/*
 * The Game Manager manages the entire state of the game.
 * The Game Manager can be extended using state macihnes in order to make the game more scalable.
 */
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState { PlayerTurn, EnemyTurn, Waiting }
        private GameState _state;
        public GameState State => _state;

        public static GameManager Instance;

        private void Awake() //Make a singleton instance.
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
