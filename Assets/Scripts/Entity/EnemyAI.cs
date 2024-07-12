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
    public class EnemyAI : Entity, IAI
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

        public override void HandleNoMovesLeft()
        {
            base.HandleNoMovesLeft();
            GameManager.Instance.EndEnemyTurn();
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
        public override IEnumerator MoveAI(List<Tile.Tile> moves)
        {
            if (moves.Count <= 1)
            {
                GameManager.Instance.EndEnemyTurn();
                yield break;
            }

            _isMoving = true;

            ResetTile(_currentTile);

            Vector3 currentPosition = transform.position;

            foreach (var move in moves)
            {

                if(move == moves[moves.Count - 1]) { break; } // stop right beside player.

                // Calculate target rotation towards the next tile
                Vector3 direction = new Vector3(move.GridPosition.x, 0, move.GridPosition.z) - new Vector3(transform.position.x, 0, transform.position.z);
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

                float elapsedTime = 0;

                /********* Rotate the player to the path *********/
                if (targetRotation != transform.rotation)
                {
                    while (elapsedTime < _rotationDuration) // Also rotate the player
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsedTime / _rotationDuration);
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    transform.rotation = targetRotation; // Ensure exact rotation at the end
                }

                elapsedTime = 0;

                /********* Move the player to the path *********/
                while (elapsedTime < _moveDuration)
                {
                    transform.position = Vector3.Lerp(
                        currentPosition,
                        new Vector3(move.GridPosition.x, move.GridPosition.y + _aboveTileYOffset, move.GridPosition.z),
                        elapsedTime / _moveDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null; // Wait until the next frame
                }

                // Ensure exact position at the end of the movement
                transform.position = new Vector3(move.GridPosition.x, move.GridPosition.y + _aboveTileYOffset, move.GridPosition.z);
                currentPosition = transform.position;
                _currentTile = move;
            }

            BlockTile(_currentTile);

            _isMoving = false;

            GameManager.Instance.EndEnemyTurn();
        }
    }
}