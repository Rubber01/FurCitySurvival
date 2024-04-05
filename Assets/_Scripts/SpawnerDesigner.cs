using UnityEngine;
using UnityEditor;
public class SpawnerDesigner : EditorWindow
{
    public GameObject objectToInstantiate;
    float x, y, z;

    [MenuItem("Tools/Prefab Spawner")]
    public static void ShowWindow()
    {
        GetWindow<SpawnerDesigner>("Prefab Spawner");
    }
    void OnGUI()
    {
        GUILayout.Label("Prefab Spawner", EditorStyles.boldLabel);

        objectToInstantiate = EditorGUILayout.ObjectField("Object to Instantiate", objectToInstantiate, typeof(GameObject), true) as GameObject;
        x = EditorGUILayout.FloatField("X Position", x);
        y = EditorGUILayout.FloatField("Y Position", y);
        z = EditorGUILayout.FloatField("Z Position", z);

        if (GUILayout.Button("Spawn Object"))
        {
            if (objectToInstantiate != null)
            {
                Instantiate(objectToInstantiate, new Vector3(x, y, z), Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Object to Instantiate is null. Please assign a GameObject.");
            }
        }
    }
}