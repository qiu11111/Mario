using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BrickCreate))]
public class BricksEditor : Editor
{
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

        BrickCreate tileBricks = (BrickCreate)target;
            if (GUILayout.Button("Tile Bricks"))
            {
                tileBricks.tileInEditor();
            }
        }
    
}
