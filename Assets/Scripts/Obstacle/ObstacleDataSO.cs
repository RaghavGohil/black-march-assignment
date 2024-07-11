/**
 * ObstacleDataSO is responsible for creating an 'ObstacleData' asset which is read by the 
 * ObstacleManager to render the obstacle.
 * **/

using UnityEngine;

namespace Game.Obstacle 
{
    [CreateAssetMenu(fileName = "ObstacleDataSO", menuName = "ScriptableObjects/ObstacleData")]
    public class ObstacleDataSO : ScriptableObject
    {
        public bool[] obstacles;
    }
}


