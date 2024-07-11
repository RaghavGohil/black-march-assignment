using Game.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.InputSystem;

namespace Game.Entity 
{
    public class Player : Entity
    {

        private TileInput _tileSelector;
        private TileManager _tileManager;
        private Tile.Tile _currentTile;

        private void Start()
        {
            if (_tileSelector == null) 
            {
                Debug.LogError("Tile selector is null!");
                enabled = false; // Fail early.
            }            
        }

        private void MoveToTile(Tile.Tile tile)
        {
            Pathfinding.Pathfinder pathfinder = new Pathfinding.Pathfinder(_tileManager);
            List<Tile.Tile> moves = pathfinder.FindPath(_currentTile,tile);
            
        }

        private void Update()
        {
            
        }
    }
}

