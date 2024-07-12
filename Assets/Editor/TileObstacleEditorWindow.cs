/*
 * A tool for unity editor to edit tile obstacles in game.
 * 
 * Generates an ObstacleData asset for unity with given size
 * or loads an asset for editing.
 * 
 * Creates toggleable buttons to edit.
 */

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Game.Obstacle;

public class TileObstacleEditorWindow : EditorWindow
{
    private uint _gridSize = 10; // Default grid size for the tool
    private ObstacleDataSO _obstacleData;
    private bool _obstacleDataInitialized;
    private ObstacleDataSO _obstacleDataField;

    [MenuItem("Window/Tile Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<TileObstacleEditorWindow>("Tile Obstacle Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Tile Obstacle Editor", EditorStyles.boldLabel);

        if (_obstacleDataInitialized)
            DrawButtons();
        else
            DrawUninitialized();
    }

    private void DrawUninitialized()
    {
        EditorGUILayout.BeginVertical();

        _gridSize = (uint)Mathf.Clamp(EditorGUILayout.IntField("Grid Size", (int)_gridSize), 1, int.MaxValue);

        if (GUILayout.Button("Create New Obstacle Data"))
        {
            CreateNewObstacleData();
        }

        _obstacleDataField = (ObstacleDataSO)EditorGUILayout.ObjectField("Obstacle Data", _obstacleDataField, typeof(ObstacleDataSO), false);

        if (_obstacleDataField != null && GUILayout.Button("Load Data"))
        {
            LoadObstacleData(_obstacleDataField);
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawButtons()
    {
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
                if (index < obstaclesProperty.arraySize)
                {
                    obstaclesProperty.GetArrayElementAtIndex(index).boolValue = GUILayout.Toggle(obstaclesProperty.GetArrayElementAtIndex(index).boolValue, "");
                }
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

    private void LoadObstacleData(ObstacleDataSO data)
    {
        _obstacleData = data;
        _gridSize = (uint)Mathf.Sqrt(_obstacleData.obstacles.Length);
        _obstacleDataInitialized = true;
    }
}

#endif