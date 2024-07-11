using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.InputSystem;
using Game.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Game.Entity 
{
    public class Player : Entity, IAI
    {

        private TileInput _tileInput;

        private new void Start()
        {
            base.Start();

            _tileInput = FindFirstObjectByType<TileInput>();

            if (_tileInput == null)
            {
                Debug.LogError("Tile Input is null!");
                enabled = false; // Fail early.
            }

            _tileInput.OnTileClick.AddListener(MoveToTile);
        }

        public override IEnumerator Move(Tile.Tile tile, List<Tile.Tile> moves) 
        {
            foreach (var move in moves)
            {
                float elapsedTime = 0;
                while (elapsedTime < _moveDuration)
                {
                    transform.position = Vector3.Lerp(
                        _currentTile.GridPosition,
                        new Vector3(move.GridPosition.x, transform.position.y, move.GridPosition.z),
                        elapsedTime / _moveDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null; // Wait until the next frame
                }
                _currentTile = move;
            }

            GameManager.Instance.State = GameManager.GameState.EnemyTurn;

        }

        private void OnDestroy()
        {
            _tileInput.OnTileClick.RemoveListener(MoveToTile);
        }
    }
}

