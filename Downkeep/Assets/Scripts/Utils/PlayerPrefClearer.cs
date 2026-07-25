using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerPrefClearer : MonoBehaviour
{
#if UNITY_EDITOR
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerPrefClearer))]
public class PlayerPrefClearerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerPrefClearer tg = (PlayerPrefClearer) target;

        DrawDefaultInspector();

        if (GUILayout.Button("Clear Prefs"))
            tg.Clear();
    }
}
#endif