using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState { PlayerTurn, EnemyTurn, Waiting }
        public GameState State;

        [HideInInspector] public UnityEvent OnPlayerTurn;
        [HideInInspector] public UnityEvent OnEnemyTurn;

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
            State = GameState.PlayerTurn;
        }

        private void Update()
        {
            if (State == GameState.PlayerTurn)
            {
                OnPlayerTurn?.Invoke();
            }
            else if (State == GameState.EnemyTurn)
            {
                StartCoroutine(EnemyTurn());
            }
        }

        public void EndPlayerTurn()
        {
            State = GameState.EnemyTurn;
        }

        private IEnumerator EnemyTurn()
        {
            State = GameState.Waiting;
            yield return new WaitForSeconds(1f);
            OnEnemyTurn?.Invoke();
            State = GameState.PlayerTurn;
        }
    }
}
