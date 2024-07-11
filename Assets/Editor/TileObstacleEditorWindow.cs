#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Game.Obstacle;

public class TileObstacleEditorWindow : EditorWindow
{
    private uint _gridSize;
    private ObstacleDataSO _obstacleData;
    private bool _obstacleDataInitialized;

    [MenuItem("Window/Tile Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<TileObstacleEditorWindow>("Tile Obstacle Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Tile Obstacle Editor", EditorStyles.boldLabel);

        if (_obstacleData == null)
        {
            EditorGUILayout.BeginVertical();
            _gridSize = (uint)Mathf.Clamp(EditorGUILayout.IntField("Grid Size", (int)_gridSize), 1, int.MaxValue);

            if (GUILayout.Button("Create New Obstacle Data"))
            {
                CreateNewObstacleData();
            }

            EditorGUILayout.EndVertical();
            return;
        }

        if (!_obstacleDataInitialized)
        {
            _gridSize = (uint)Mathf.Sqrt(_obstacleData.obstacles.Length);
            _obstacleDataInitialized = true;
        }

        if (GUILayout.Button("Save Obstacle Data"))
        {
            EditorUtility.SetDirty(_obstacleData);
            AssetDatabase.SaveAssets();
        }

        SerializedObject serializedObject = new SerializedObject(_obstacleData);
        SerializedProperty obstaclesProperty = serializedObject.FindProperty("obstacles");

        serializedObject.Update();

        for (int y = 0; y < _gridSize; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < _gridSize; x++)
            {
                int index = y * (int)_gridSize + x;
                obstaclesProperty.GetArrayElementAtIndex(index).boolValue = GUILayout.Toggle(obstaclesProperty.GetArrayElementAtIndex(index).boolValue, "");
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateNewObstacleData()
    {
        ObstacleDataSO newObstacleData = CreateInstance<ObstacleDataSO>();
        newObstacleData.obstacles = new bool[_gridSize * _gridSize];
        AssetDatabase.CreateAsset(newObstacleData, "Assets/ObstacleData.asset");
        AssetDatabase.SaveAssets();

        _obstacleData = newObstacleData;
        _obstacleDataInitialized = true;
    }
}

#endif