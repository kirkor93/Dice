using Localization;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Localizer))]
public class LocalizerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Localizer scriptTarget = (Localizer)target;

        string oldText = scriptTarget.Key;
        string newText = EditorGUILayout.TextField("Key", oldText);

        if (oldText != newText)
        {
            scriptTarget.Key = newText;
            scriptTarget.Localize();
        }
    }
}
