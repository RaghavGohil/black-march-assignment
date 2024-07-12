/*
 * Inherited from the Entity abstract class.
 * Handles the enemy behavior.
 */

using Game.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entity 
{
    public class Enemy : Entity, IAI
    {

        private Player _player;

        private new void Start()
        {
            base.Start();

            _player = FindFirstObjectByType<Player>();

            if (_player == null) 
            {
                Debug.LogError("Player is null. Add a type 'Player' to the scene.");
            }
        }

        /// <summary>
        /// Handles the turn internally.
        /// </summary>
        private void Update()
        {
            if (GameManager.Instance.State == GameManager.GameState.EnemyTurn && !_isMoving) 
            {
                MoveToTile(_player.CurrentTile);
            }   
        }

        /// <summary>
        /// An override method called by MoveToTile when there is a turn.
        /// The function checks if there less than or equal to one move.
        /// If enemy reaches the player in the next cross position, the enemy will just not move.
        /// The enemy tile is then reset so it frees the tile to move.
        /// We move the enemy one step to the desired tile.
        /// The enemy current tile is then set to blocked.
        /// </summary>
        /// <param name="tile">gets the tile from the MoveToTile function</param>
        /// <param name="moves"></param>
        /// <returns></returns>
        public override IEnumerator Move(List<Tile.Tile> moves)
        {
            if (moves.Count <= 1)
            {
                GameManager.Instance.EndEnemyTurn();
                yield break;
            }

            ResetTile(_currentTile);

            _isMoving = true;


            float elapsedTime = 0;
            while (elapsedTime < _moveDuration && _currentTile != moves[moves.Count - 2])
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(moves[0].GridPosition.x, moves[0].GridPosition.y + _aboveTileYOffset, moves[0].GridPosition.z),
                    elapsedTime / _moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait until the next frame
            }

            _currentTile = moves[0];

            BlockTile(_currentTile);

            _isMoving = false;
            
            GameManager.Instance.EndEnemyTurn();
        }
    }
}