/**
 * TileSelector is responsible to select tiles and perform actions (like when it is hovered or clicked, etc).
 * **/

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game.InputSystem
{

    public class TileInput : MonoBehaviour
    {
        /// <summary>
        /// When a tile is selected, the OnTileHover event will be triggered.
        /// Any functions subscribed to this event will execute.
        /// </summary>
        [HideInInspector] public UnityEvent<Tile.Tile> OnTileHover;

        /// <summary>
        /// Responsible for any operation of selecting the tiles which involves
        /// hovering over it.
        /// </summary>
        private void SelectTiles()
        {
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Tile.Tile>(out Tile.Tile tile))
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
