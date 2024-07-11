/*
 * This script will spawn entities in the game 
 * on tiles - be it player or an enemy in the stated position in inspector.
 */
using Game.Tile;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Entity 
{
    public class EntitySpawner : MonoBehaviour
    {
        /// <summary>
        /// We need tile manager to entities on tiles.
        /// </summary>
        [SerializeField] private TileManager _tileManager;
        
        [System.Serializable]
        [Tooltip("Spawn data holds entity to spawn and grid position as X and Z.")]
        private struct SpawnData
        {
            public GameObject EntityGO;
            public Vector2Int GridPosition;
        }

        [SerializeField] private SpawnData[] _spawnData;


        private void Awake()
        {
            if (_tileManager == null) 
            {
                Debug.LogError("Tile manager is null!");
                enabled = false;
            }
            _tileManager.OnTileManagerInitialized.AddListener(InstantiateEntities);
        }

        /// <summary>
        /// checks if entities are outside boundaries
        /// and spawns entities in grid position above tiles.
        /// </summary>
        void InstantiateEntities() 
        {
            foreach(var data in _spawnData) 
            {
                if (data.GridPosition.x > _tileManager.GridSize || data.GridPosition.y > _tileManager.GridSize)
                { 
                    Debug.LogWarning("Spawned objects beyond tile boundaries.");
                    return;
                }
                Tile.Tile tile = _tileManager.GetTileAtPosition(data.GridPosition);
                GameObject go = Instantiate(data.EntityGO,new Vector3(data.GridPosition.x, tile.GridPosition.y, data.GridPosition.y),Quaternion.identity);
                if (go.TryGetComponent<Entity>(out Entity e)) 
                {
                    e.SetCurrentTile(tile);
                }
            }
        }

    } 
}


