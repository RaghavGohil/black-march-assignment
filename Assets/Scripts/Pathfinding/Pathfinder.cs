using Game.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI
{
    public class Pathfinder
    {
        private TileManager _tileManager;

        public Pathfinder(TileManager tileManager)
        {
            _tileManager = tileManager;
        }

        /// <summary>
        /// FindPath is an A* algorithm used to find the path between two points.
        /// </summary>
        /// <param name="startTile">The start tile</param>
        /// <param name="targetTile">The target tile</param>
        /// <returns>The path of tiles from the start tile to the target tile.</returns>
        public List<Tile.Tile> FindPath(Tile.Tile startTile, Tile.Tile targetTile)
        {
            PriorityQueue<Tile.Tile> openSet = new PriorityQueue<Tile.Tile>();
            Dictionary<Tile.Tile, Tile.Tile> cameFrom = new Dictionary<Tile.Tile, Tile.Tile>();
            Dictionary<Tile.Tile, float> gScore = new Dictionary<Tile.Tile, float>();
            Dictionary<Tile.Tile, float> fScore = new Dictionary<Tile.Tile, float>();

            openSet.Enqueue(startTile, 0);
            gScore[startTile] = 0;
            fScore[startTile] = Heuristic(startTile, targetTile);

            while (openSet.Count > 0)
            {
                Tile.Tile currentTile = openSet.Dequeue();

                if (currentTile == targetTile)
                {
                    return ReconstructPath(cameFrom, currentTile);
                }

                foreach (Tile.Tile neighbor in GetNeighbors(currentTile))
                {
                    float tentativeGScore = gScore[currentTile] + Distance(currentTile, neighbor);

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = currentTile;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, targetTile);

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                        }
                    }
                }
            }

            return null; // No path found
        }

        private List<Tile.Tile> ReconstructPath(Dictionary<Tile.Tile, Tile.Tile> cameFrom, Tile.Tile currentTile)
        {
            List<Tile.Tile> path = new List<Tile.Tile>();

            while (cameFrom.ContainsKey(currentTile))
            {
                path.Add(currentTile);
                currentTile = cameFrom[currentTile];
            }

            path.Reverse();
            return path;
        }

        private IEnumerable<Tile.Tile> GetNeighbors(Tile.Tile tile)
        {
            List<Tile.Tile> neighbors = new List<Tile.Tile>();
            Vector2Int[] directions = new Vector2Int[]
            {
                new Vector2Int(0, 1),
                new Vector2Int(1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(-1, 0)
            };

            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighborPos = new Vector2Int(tile.GridPosition.x, tile.GridPosition.z) + direction;
                Tile.Tile neighborTile = _tileManager.GetTileAtPosition(neighborPos);

                if (neighborTile != null && neighborTile.TileState == TileState.Walkable)
                {
                    neighbors.Add(neighborTile);
                }
            }

            return neighbors;
        }

        private float Heuristic(Tile.Tile a, Tile.Tile b)
        {
            // Use Manhattan distance as the heuristic
            return Mathf.Abs(a.GridPosition.x - b.GridPosition.x) + Mathf.Abs(a.GridPosition.z - b.GridPosition.z);
        }

        private float Distance(Tile.Tile a, Tile.Tile b)
        {
            // Distance between adjacent tiles (assuming uniform grid)
            return 1.0f;
        }
    }

    public class PriorityQueue<T>
    {
        private List<KeyValuePair<T, float>> _elements = new List<KeyValuePair<T, float>>();

        public int Count => _elements.Count;

        public void Enqueue(T item, float priority)
        {
            _elements.Add(new KeyValuePair<T, float>(item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < _elements.Count; i++)
            {
                if (_elements[i].Value < _elements[bestIndex].Value)
                {
                    bestIndex = i;
                }
            }

            T bestItem = _elements[bestIndex].Key;
            _elements.RemoveAt(bestIndex);
            return bestItem;
        }

        public bool Contains(T item)
        {
            return _elements.Exists(element => EqualityComparer<T>.Default.Equals(element.Key, item));
        }
    }
}
