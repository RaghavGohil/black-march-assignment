/**
 * Obstacle Manager takes care of obstacles rendered
 * on top of tiles. Obstacle Manager is initialized after TileManager.
 * **/

using Game.Tile;
using UnityEngine;

namespace Game.Obstacle
{
    public class ObstacleManager : MonoBehaviour
    {

        [SerializeField] private ObstacleDataSO _obstacleDataSO;
        [SerializeField] private TileManager _tileManager;
        [SerializeField] private GameObject _obstaclePrefab;

        private float _obstacleVerticalOffset = 1f;

        private void Awake()
        {
            if (_tileManager == null)
            {
                Debug.LogError("Tile Manager not found!");
            }
            else 
            {
                _tileManager.OnTileManagerInitialized.AddListener(GenerateObstacles); // Initialized after Tile Manager
            }
        }

        /// <summary>
        /// Reads the obstacle data scriptable object and generates obstacles on top of tiles.
        /// </summary>
        private void GenerateObstacles() 
        {
            // Generate visual representation of the grid
            for (int x = 0; x < _tileManager.GridSize; x++)
            {
                for (int y = 0; y < _tileManager.GridSize; y++)
                {
                    Vector3 spawnPos = new Vector3(x, _obstacleVerticalOffset , y);

                    // Instantiate an obstacle prefab (red sphere) for each obstacle
                    if (_obstacleDataSO.obstacles[y * _tileManager.GridSize + x])
                    {
                        GameObject obstacle = Instantiate(_obstaclePrefab, spawnPos, Quaternion.identity);
                        _tileManager.TileList[y * _tileManager.GridSize + x].TileState = TileState.Blocked; // block the tiles
                    }
                }
            }
        }
    }
}


