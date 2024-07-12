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
        private Tile.Tile _nextTile;

        private new void Start()
        {
            base.Start();

            _tileInput = FindFirstObjectByType<TileInput>();

            if (_tileInput == null)
            {
                Debug.LogError("Tile Input is null!");
                enabled = false; // Fail early.
            }

            _tileInput.OnTileClick.AddListener(GetTileOnClick);
            _nextTile = null;
        }

        private void Update()
        {
            if (GameManager.Instance.State == GameManager.GameState.PlayerTurn && !_isMoving)
            {
                if (_nextTile != null && _currentTile != _nextTile) 
                {
                    MoveToTile(_nextTile);
                    _nextTile = null;
                }
            }
        }

        private void GetTileOnClick(Tile.Tile tile) 
        {
            _nextTile = tile;   
        }

        public override IEnumerator Move(Tile.Tile tile, List<Tile.Tile> moves) 
        {

            _isMoving = true;

            Vector3 currentPosition = transform.position;

            foreach (var move in moves)
            {
                float elapsedTime = 0;
                while (elapsedTime < _moveDuration)
                {
                    transform.position = Vector3.Lerp(
                        currentPosition,
                        new Vector3(move.GridPosition.x, move.GridPosition.y + _transformYOffset, move.GridPosition.z),
                        elapsedTime / _moveDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null; // Wait until the next frame
                }
                currentPosition = new Vector3(move.GridPosition.x, move.GridPosition.y + _transformYOffset, move.GridPosition.z);
                _currentTile = move;
            }

            _isMoving = false;

            GameManager.Instance.EndPlayerTurn();
        }

        private void OnDestroy()
        {
            _tileInput.OnTileClick.RemoveListener(MoveToTile);
        }
    }
}

