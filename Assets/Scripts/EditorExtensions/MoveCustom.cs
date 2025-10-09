using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

[CustomEditor(typeof(EnemyCoreMove))]
public class MoveCustom : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var coremove = (EnemyCoreMove)target;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("コアの最大半径：" + coremove.Core.maxr, EditorStyles.boldLabel);

    }
}
