/**
 * TileObstacleEditorWindow is responsible for creating an editor window tool which contains toggleable
 * buttons which is stored in the 'ObstacleDataSO' for generating obstacles.
 * **/

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using Game.Obstacle;

public class TileObstacleEditorWindow : EditorWindow
{
    private ObstacleDataSO obstacleData;

    [MenuItem("Window/Tile Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<TileObstacleEditorWindow>("Tile Obstacle Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Obstacle Grid Editor", EditorStyles.boldLabel);

        if (obstacleData == null)
        {
            if (GUILayout.Button("Create New Obstacle Data"))
            {
                CreateNewObstacleData();
            }
            obstacleData = (ObstacleDataSO)EditorGUILayout.ObjectField("Obstacle Data", obstacleData, typeof(ObstacleDataSO), false);
            return;
        }

        if (GUILayout.Button("Save Obstacle Data"))
        {
            EditorUtility.SetDirty(obstacleData);
            AssetDatabase.SaveAssets();
        }

        SerializedObject serializedObject = new SerializedObject(obstacleData);
        SerializedProperty obstaclesProperty = serializedObject.FindProperty("obstacles");

        serializedObject.Update();

        var gridSize = Mathf.Sqrt(obstaclesProperty.arraySize);

        for (int y = 0; y < gridSize; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < gridSize; x++)
            {
                int index = y * (int)gridSize + x;
                obstaclesProperty.GetArrayElementAtIndex(index).boolValue = GUILayout.Toggle(obstaclesProperty.GetArrayElementAtIndex(index).boolValue, "");
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateNewObstacleData()
    {
        ObstacleDataSO newObstacleData = CreateInstance<ObstacleDataSO>();
        AssetDatabase.CreateAsset(newObstacleData, "Assets/ObstacleData.asset");
        AssetDatabase.SaveAssets();

        obstacleData = newObstacleData;
    }
}

#endif