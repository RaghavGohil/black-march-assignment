using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.InputSystem;
using Game.AI;

namespace Game.Entity 
{
    public class Player : Entity, IAI
    {

        private TileInput _tileInput;

        /// <summary>
        /// The next tile is set when the player clicks on a tile.
        /// </summary>
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


        /// <summary>
        /// The player moves to a tile on turn when the clicked tile is not the current tile.
        /// The next tile is immediately set to null because when player moves, the next tile is not null
        /// and the currentTile is not equal to next tile as the Move function updates the current tile on each move.
        /// </summary>
        private void Update()
        {
            if (GameManager.Instance.State == GameManager.GameState.PlayerTurn)
            {
                if (_nextTile != null && _currentTile != _nextTile) 
                {
                    MoveToTile(_nextTile);
                    _nextTile = null;
                }
            }
        }

        /// <summary>
        /// Is attached as a listener to the event in TileInput
        /// also checks if player is moving.
        /// </summary>
        /// <param name="tile">gets the current tile from TileInput as it's a listener</param>
        private void GetTileOnClick(Tile.Tile tile) 
        {
            if(!_isMoving)
                _nextTile = tile;   
        }


        /// <summary>
        /// An override method called by MoveToTile when there is a turn.
        /// The player tile reset so it frees the tile to move.
        /// We move the player entirely to the desired tile, checking each tile position.
        /// The player current tile is then set to blocked.
        /// </summary>
        /// <param name="moves">Gets the moves from the pathfinder ai from MoveToTile</param>
        /// <returns></returns>
        public override IEnumerator Move(List<Tile.Tile> moves) 
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
                        new Vector3(move.GridPosition.x, move.GridPosition.y + _aboveTileYOffset, move.GridPosition.z),
                        elapsedTime / _moveDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null; // Wait until the next frame
                }
                currentPosition = new Vector3(move.GridPosition.x, move.GridPosition.y + _aboveTileYOffset, move.GridPosition.z);
                _currentTile = move;
            }

            _isMoving = false;

            GameManager.Instance.EndPlayerTurn();
        }

        private void OnDestroy()
        {
            _tileInput.OnTileClick.RemoveListener(GetTileOnClick);
        }
    }
}

