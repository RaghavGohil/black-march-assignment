/**
 * UITileInfo is responsible to render the tile unit information to the UI Canvas.
 * **/

using UnityEngine;
using TMPro;
using Game.InputSystem;

namespace Game.UI 
{
    public class UITileInfo : MonoBehaviour
    {

        [SerializeField] private TileInput _tileInput;
        [SerializeField] private TMP_Text _unitInfoText;

        private void Start()
        {
            if (_tileInput != null)
            {
                _tileInput.OnTileHover.AddListener(UpdateTileInfo);
            }
            else 
            {
                Debug.LogError("Tile selector is null!");
            }
        }

        /// <summary>
        /// Updates the UI text and displays the tile's gridPosition
        /// </summary>
        /// <param name="tile">Tile is the grid unit tile.</param>
        public void UpdateTileInfo(Tile.Tile tile)
        {
            _unitInfoText.text = $"Unit info: {tile.GridPosition}";
        }

        private void OnDestroy()
        {
            _tileInput.OnTileHover?.RemoveListener(UpdateTileInfo);
        }
    }
}


