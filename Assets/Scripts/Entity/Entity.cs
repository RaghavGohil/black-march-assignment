using Game.InputSystem;
using Game.Tile;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Game.Entity 
{
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Visual y offset above tile
        /// </summary>
        [SerializeField] protected float _aboveTileYOffset;

        /// <summary>
        /// The duration required to move to next tile.
        /// </summary>
        protected float _moveDuration;

        protected bool _isMoving = false;

        protected TileManager _tileManager;
        protected Tile.Tile _currentTile;
        public Tile.Tile CurrentTile => _currentTile;

        private AI.Pathfinder _pathfinder;


        protected void Start()
        {
            _tileManager = FindFirstObjectByType<TileManager>();

            if (_tileManager == null)
            {
                Debug.LogError("Tile Manager is null");
                enabled = false;
            }

            _pathfinder = new AI.Pathfinder(_tileManager);


            //Entity data
            _moveDuration = 0.5f;

            //Spawn with a y offset above tile.
            transform.position = new Vector3(_currentTile.GridPosition.x, _currentTile.GridPosition.y + _aboveTileYOffset, _currentTile.GridPosition.z);
            BlockTile(_currentTile);
        }

        public void SetCurrentTile(Tile.Tile tile) 
        {
            _currentTile = tile;
        }

        protected void MoveToTile(Tile.Tile tile)
        {
            List<Tile.Tile> moves = _pathfinder.FindPath(_currentTile, tile); //Do algorithm, find path..
            if (moves != null) 
            {
                StartCoroutine(Move(moves));
            }
        }

        protected void BlockTile(Tile.Tile tile) // Blocks a tile in game
        {
            tile.TileState = TileState.Blocked;
        }

        protected void ResetTile(Tile.Tile tile)
        {
            tile.TileState = TileState.Walkable;
        }

        public virtual IEnumerator Move(List<Tile.Tile> moves) { yield return null; } // overridden by implementation
    }
}

