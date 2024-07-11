/**
 * TileManager is responsible to manage tiles.
 * **/

using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace Game.Tile 
{
    public class TileManager : MonoBehaviour
    {
        private readonly uint _gridSize = 10;
        public uint GridSize => _gridSize;

        private List<Tile>_tileList;
        public List<Tile> TileList => _tileList;

        [HideInInspector] public UnityEvent OnTileManagerInitialized;

        private void Start()
        {
            SetTileList();
            OnTileManagerInitialized?.Invoke();
        }

        /// <summary>
        /// This function sets tiles to a list from the hierarchy 
        /// once they are spawned to access them conveniently. 
        /// </summary>
        private void SetTileList() 
        {
            _tileList = new List<Tile>();
            var tileComponentsInHierarchy = GetComponentsInChildren<Tile>();
            if (tileComponentsInHierarchy != null) 
            {
                _tileList.AddRange(tileComponentsInHierarchy);
            }
        }

        /// <summary>
        /// Gets tile at position in the grid.
        /// </summary>
        /// <param name="position">Vector2 position is needed. The Y position is not necessary</param>
        /// <returns>Type 'Tile' at position</returns>
        public Tile GetTileAtPosition(Vector2Int position) 
        {
            return _tileList
                    .Where(tile => tile.transform.position.x == position.x
                                && tile.transform.position.z == position.y)
                    .FirstOrDefault();
        }

    }
}


