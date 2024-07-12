/*
 * Input manager handles all of the input in game..
 */

using System;
using UnityEngine;
using Game.Tile;

namespace Game.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private TileInput _tileInput;

        private void Start()
        {
            _tileInput = FindObjectOfType<TileInput>();
            if (_tileInput == null)
            {
                Debug.LogError("TileInput is null!");
                enabled = false; // Fail early.
            }
        }

        /// <summary>
        /// Handles hover and if not player's turn, there is no need for click event.
        /// </summary>
        private void Update()
        {
            _tileInput.HandleHover();

            if (GameManager.Instance.State != GameManager.GameState.PlayerTurn)
                return;
            _tileInput.HandleClick();
        }
    }
}
