/**
 * TileInput is responsible to select tiles and perform actions (like when it is hovered or clicked, etc).
 * **/

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
        [HideInInspector] public UnityEvent<Tile.Tile> OnTileClick;
        private Ray _ray;
        private RaycastHit _raycastHit;
        private bool _hit;
        private Tile.Tile _tile;


        /// <summary>
        /// Responsible for any operation of selecting the tiles which involves
        /// hovering over it.
        /// </summary>

        private void Update()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _hit = Physics.Raycast(_ray, out RaycastHit _raycastHit);
            if (_hit)
                _tile = _raycastHit.collider.GetComponent<Tile.Tile>();
        }

        public void HandleHover()
        {
            if (_tile != null)
            {
                OnTileHover?.Invoke(_tile);
            }
        }

        public void HandleClick() 
        {
            if (_tile != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnTileClick?.Invoke(_tile);
                }
            }
        }
    }
}
