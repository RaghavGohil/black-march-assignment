using System;
using UnityEngine;
using Game.Tile;

namespace Game.InputSystem
{
    public enum InputState
    {
        Game,
        UI,
        Menu, //There can be multiple input states depending on the need..
    }

    public class InputManager : MonoBehaviour
    {
        private InputState _inputState;

        private void Start()
        {
            _inputState = InputState.Game; // We will start with the game state.
        }

        void Update()
        {
            switch (_inputState) // depending upon the state, do the following actions
            {
                case InputState.Game:
                    break;
                case InputState.UI:
                    break;
                case InputState.Menu:
                    break;
            }
        }

        /// <summary>
        /// Switches the input system state.
        /// </summary>
        /// <param name="inputState">Can be game, ui, etc.</param>
        private void SwitchGameState(InputState inputState) 
        {
            _inputState = inputState;
        }


        private void HandleMouseInput()
        {
            
        }

        private void HandleKeyboardInput()
        {
            
        }
    }
}
