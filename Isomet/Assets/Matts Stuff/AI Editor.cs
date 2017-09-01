using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AI))]
public class AIEditor : Editor {

    bool viewFullInspector = false;

    public override void OnInspectorGUI()
    {
        AI myTarget = (AI)target;
        

        GUILayout.BeginVertical("box");
        myTarget.m_range = EditorGUILayout.FloatField("Attck Range : ", myTarget.m_range);
        myTarget.m_speed = EditorGUILayout.FloatField("Move Speed : ", myTarget.m_speed);
        myTarget.m_attackSpeed = EditorGUILayout.FloatField("Attack Speed : ", myTarget.m_attackSpeed);
        myTarget.m_rotationSpeed = EditorGUILayout.FloatField("Rotation Speed : ", myTarget.m_rotationSpeed);
        myTarget.m_isRanged = EditorGUILayout.Toggle("Is Ranged? ", myTarget.m_isRanged);
        viewFullInspector = EditorGUILayout.Toggle("View Inspector", viewFullInspector);
        GUILayout.EndVertical();

        if (viewFullInspector) base.DrawDefaultInspector();

    }
}
