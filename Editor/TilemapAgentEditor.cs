using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ET.GridSystem;

#if UNITY_EDITOR
[CustomEditor(typeof(TilemapAgentBase), true)]
public class TilemapAgentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TilemapAgentBase myScript = (TilemapAgentBase)target;
        GUILayout.Space(5);

        //
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("CleanAllObjects", GUILayout.MaxWidth(250)))
        {
            myScript.CleanAllTiles();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("FillMapWithDefaultGTile", GUILayout.MaxWidth(250)))
        {
            myScript.FillMapWithDefaultMTile();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("FillMapWithDefaultGTileGroup", GUILayout.MaxWidth(250)))
        {
            myScript.FillMapWithDefaultMTileGroup();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        //
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("LogBuildMap", GUILayout.MaxWidth(250)))
        {
            myScript.LogBuildMap();
        }
        if (GUILayout.Button("ReadBuildMap", GUILayout.MaxWidth(250)))
        {
            myScript.ReadAllTileIDs();
        }
        if (GUILayout.Button("Export Map Data", GUILayout.MaxWidth(250)))
        {
            myScript.ExportMapData();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(12);
        EditorGUILayout.LabelField("── Editor Tools ──", EditorStyles.boldLabel);
        EditorGUILayout.Space(4);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Draw From Data", GUILayout.MaxWidth(250)))
        {
            myScript.TilemapDrawer.DrawTilemap();
        }
        if (GUILayout.Button("Export Data", GUILayout.MaxWidth(250)))
        {
            myScript.ExportMapData();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);


    }
}
#endif