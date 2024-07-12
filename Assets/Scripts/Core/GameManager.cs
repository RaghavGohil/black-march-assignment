using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState { PlayerTurn, EnemyTurn, Waiting }
        private GameState _state;
        public GameState State => _state;

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _state = GameState.PlayerTurn;
        }

        public void EndPlayerTurn()
        {
            _state = GameState.EnemyTurn;
        }

        public void EndEnemyTurn()
        {
            _state = GameState.PlayerTurn;
        }
    }
}
