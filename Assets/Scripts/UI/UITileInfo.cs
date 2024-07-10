/**
 * UITileInfo is responsible to render the tile unit information to the UI Canvas.
 * **/

using UnityEngine;
using TMPro;
using Game.Tile;

namespace Game.UI 
{
    public class UITileInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _unitInfoText;

        [SerializeField] private TileSelector _tileSelector;

        private void Start()
        {
            if (_tileSelector != null)
            {
                _tileSelector.OnTileHover.AddListener(UpdateTileInfo);
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
            _tileSelector.OnTileHover?.RemoveListener(UpdateTileInfo);
        }
    }
}


