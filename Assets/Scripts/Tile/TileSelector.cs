/**
 * TileSelector is responsible to select tiles and perform actions (like when it is hovered or clicked, etc).
 * **/

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Tile 
{    
    public class TileSelector : MonoBehaviour
    {
        /// <summary>
        /// When a tile is selected, the OnTileHover event will be triggered.
        /// Any functions subscribed to this event will execute.
        /// </summary>
        [HideInInspector] public UnityEvent<Tile> OnTileHover;

        /// <summary>
        /// Responsible for any operation of selecting the tiles which involves
        /// hovering over it.
        /// </summary>
        private void SelectTiles()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Tile>(out Tile tile))
                {
                    OnTileHover?.Invoke(tile);
                }
            }
        }

        private void Update()
        {
            SelectTiles();
        }
    }
}
