using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GTile))]
    public class CustomGTileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector elements
            DrawDefaultInspector();
            GUILayout.Space(15);
            // Reference to the target object
            GTile gTile = (GTile)target;

            // Add a button to set the ID
            if (GUILayout.Button("Set ID to File Name"))
            {
                // Get the file name of the asset
                string path = AssetDatabase.GetAssetPath(gTile);
                string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

                // Set the ID to the file name
                gTile.SetID(fileName);

                // Mark the object as dirty to save changes
                EditorUtility.SetDirty(gTile);

                Debug.Log($"GTile ID set to file name: {fileName}");
            }
        }
    }
#endif
}