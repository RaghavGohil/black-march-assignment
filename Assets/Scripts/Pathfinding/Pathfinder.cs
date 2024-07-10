using Game.Tile;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pathfinding 
{
    public class Pathfinder
    {

        private TileManager _tileManager;

        public Pathfinder(TileManager tileManager)
        {
            _tileManager = tileManager;
        }

        /// <summary>
        /// FindPath is a BFS algorithm which is used to find path between two points.
        /// </summary>
        /// <param name="startTile">The start tile</param>
        /// <param name="targetTile">The target tile</param>
        /// <returns>The path of tiles from start tile to target tile.</returns>
        public List<Tile.Tile> FindPath(Tile.Tile startTile, Tile.Tile targetTile)
        {
            Queue<Tile.Tile> queue = new Queue<Tile.Tile>();
            Dictionary<Tile.Tile, Tile.Tile> cameFrom = new Dictionary<Tile.Tile, Tile.Tile>();
            HashSet<Tile.Tile> visited = new HashSet<Tile.Tile>();

            queue.Enqueue(startTile);
            visited.Add(startTile);

            while (queue.Count > 0)
            {
                Tile.Tile currentTile = queue.Dequeue();

                if (currentTile == targetTile)
                {
                    return ReconstructPath(cameFrom, currentTile);
                }

                foreach (Tile.Tile neighbor in GetNeighbors(currentTile))
                {
                    if (visited.Contains(neighbor) || neighbor.TileState != TileState.Walkable)
                    {
                        continue;
                    }

                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    cameFrom[neighbor] = currentTile;
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
            new Vector2Int(0, 1), // Up
            new Vector2Int(1, 0), // Right
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0) // Left
            };

            foreach (Vector2Int direction in directions)
            {
                Vector2Int neighborPos = ((Vector2Int)tile.GridPosition) + direction;
                Tile.Tile neighborTile = _tileManager.GetTileAtPosition((Vector3Int)neighborPos);

                if (neighborTile != null)
                {
                    neighbors.Add(neighborTile);
                }
            }

            return neighbors;
        }

    }
}

