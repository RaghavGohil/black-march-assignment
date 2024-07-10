/**
 * TileManager is responsible to manage tiles.
 * **/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Tile 
{
    public class TileManager : MonoBehaviour
    {
        private readonly int _gridSize = 10;
        public int GridSize => _gridSize;

        private List<Tile>_tileList;
        public List<Tile> TileList => _tileList;

        public UnityEvent OnTileManagerInitialized;

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
    }
}


