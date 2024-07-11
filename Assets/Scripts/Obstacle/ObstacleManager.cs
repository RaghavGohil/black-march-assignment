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
                enabled = false; //Fail early if tile manager is not found.
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

            if (_obstacleDataSO == null) 
            {
                Debug.LogError("Obstacle data is null!");
                return;
            }

            if (_obstaclePrefab == null)
            {
                Debug.LogError("Obstacle prefab is null!");
                return;
            }

            var obstacleGridSize = (int) Mathf.Sqrt(_obstacleDataSO.obstacles.Length);

            // Generate visual representation of the grid
            for (int x = 0; x < obstacleGridSize; x++)
            {
                for (int y = 0; y < obstacleGridSize; y++)
                {
                    Vector3 spawnPos = new Vector3(x, _obstacleVerticalOffset , y);

                    Debug.Log($"{y} {x}");

                    // Instantiate an obstacle prefab (red sphere) for each obstacle
                    if (_obstacleDataSO.obstacles[y * obstacleGridSize + x])
                    {
                        GameObject obstacle = Instantiate(_obstaclePrefab, spawnPos, Quaternion.identity,transform);
                        _tileManager.TileList[y * obstacleGridSize + x].TileState = TileState.Blocked; // block the tiles
                    }
                }
            }
        }
    }
}


