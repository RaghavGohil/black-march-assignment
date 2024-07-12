/**
 * Tile is responsible to store information on in game tile units.
 * **/

using UnityEngine;
using UnityEngine.Events;

namespace Game.Tile 
{

    /// <summary>
    /// A tile can be blocked so entity will not move on that tile.
    /// </summary>
    public enum TileState 
    {
        Walkable,
        Blocked
    }

    public class Tile : MonoBehaviour
    {
        private int _tileId;

        private Vector3Int _gridPosition;
        [HideInInspector] public Vector3Int GridPosition => _gridPosition;

        [HideInInspector]public UnityEvent OnTileStateChange;
        public TileState _tileState;

        /// <summary>
        /// If the tile state is changed to anything, subscribed functions will reflect.
        /// </summary>
        public TileState TileState { 
            get { return _tileState; }
            set {
                OnTileStateChange?.Invoke();
                _tileState = value;
            }
        }

        private void Awake()
        {
            _tileState = TileState.Walkable;
        }

        /// <summary>
        /// Sets the position of the tile when it's spawned.
        /// </summary>
        /// <param name="position">The position where the tile is spawned.</param>
        public void SetPosition(Vector3Int position)
        {
            _gridPosition = position;
        }
    }
}

