using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.InputSystem;

namespace Game.Tile 
{
    public class TileCursor : MonoBehaviour
    {
        [SerializeField] private TileInput _tileInput;
        [SerializeField] private GameObject _cursorPrefab;
        private GameObject _cursor;
        private void Start()
        {
            if (_tileInput == null) 
            {
                Debug.LogError("Tile Input is null!");
                enabled = false;
            }

            if (_cursorPrefab != null)
                _cursor = Instantiate(_cursorPrefab, new Vector3(), Quaternion.identity);
            else
                Debug.LogWarning("Unable to render tile cursor. Cursor prefab is null.");

            _tileInput.OnTileHover.AddListener(RenderCursor);
        }

        private void RenderCursor(Tile tile)
        {
            if (_cursor != null)
                _cursor.transform.position = tile.GridPosition;
        }

        private void OnDestroy()
        {
            _tileInput.OnTileHover.RemoveListener(RenderCursor);
        }
    }  
}


