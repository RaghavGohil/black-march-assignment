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

            if (GameManager.Instance != null)
                GameManager.Instance.OnEnemyTurn.AddListener(Turn);
        }

        private void Turn()
        {
            MoveToTile(_player.CurrentTile);
        }

 
        public override IEnumerator Move(Tile.Tile tile, List<Tile.Tile> moves)
        {
            float elapsedTime = 0;
            while (elapsedTime < _moveDuration && _currentTile != moves[moves.Count - 2])
            {
                transform.position = Vector3.Lerp(
                    _currentTile.GridPosition,
                    new Vector3(moves[0].GridPosition.x, transform.position.y, moves[0].GridPosition.z),
                    elapsedTime / _moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null; // Wait until the next frame
            }
            _currentTile = moves[0];
        }

        public void OnDestroy()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnEnemyTurn.RemoveListener(Turn);
        }
    }
}