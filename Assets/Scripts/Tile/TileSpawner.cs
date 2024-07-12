/**
 * TileSpawner is responsible to spawn tiles in game (creates a tile grid).
 * **/

using UnityEngine;

namespace Game.Tile 
{
    [RequireComponent(typeof(TileManager))]
    public class TileSpawner : MonoBehaviour
    {
        private TileManager _tileManager;
        [Tooltip("Insert the tile prefab here!")] public GameObject tilePrefab;

        void Awake()
        {
            _tileManager = GetComponent<TileManager>();
            GenerateGrid();
        }

        /// <summary>
        /// Responsible to generate the grid by specified gridSize with the z as 0.
        /// </summary>
        void GenerateGrid()
        {
            for (int x = 0; x < _tileManager.GridSize; x++)
            {
                for (int y = 0; y < _tileManager.GridSize; y++)
                {
                    Vector3Int position = new Vector3Int(x, 0, y);
                    GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                    tile.GetComponent<Tile>().SetPosition(position);
                }
            }
        }
    }
}

