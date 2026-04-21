#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using ETEditor;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
    [CustomEditor(typeof(GTileGroup))]
    public class GTileGroupEditor : Editor
    {
        private EditorFolderPicker _folderPicker;
        private GTile[] _foundTiles;

        private void OnEnable()
        {
            if (target != null)
            {
                _folderPicker = new EditorFolderPicker(
                    $"GTileGroupEditor_TileFolder_{target.name}",
                    "Assets",
                    "Select GTile Folder"
                );
                if (_folderPicker.IsValidAssetFolder)
                {
                    _foundTiles = _folderPicker.LoadAll<GTile>();
                }
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GTileGroup myScript = (GTileGroup)target;

            _folderPicker ??= new EditorFolderPicker(
                $"GTileGroupEditor_TileFolder_{target.name}",
                "Assets",
                "Select GTile Folder"
            );

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("GTile Folder", EditorStyles.boldLabel);

            bool changed = _folderPicker.DrawLayout("GTile Folder");
            if (changed)
            {
                _foundTiles = _folderPicker.LoadAll<GTile>();
            }

            if (_folderPicker.IsValidAssetFolder)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Find All GTiles"))
                {
                    _foundTiles = _folderPicker.LoadAll<GTile>();
                }
                if (_foundTiles != null && GUILayout.Button("Refresh"))
                {
                    _foundTiles = _folderPicker.LoadAll<GTile>();
                }
                EditorGUILayout.EndHorizontal();
            }

            if (_foundTiles != null && _foundTiles.Length > 0)
            {
                EditorGUILayout.LabelField($"Found {_foundTiles.Length} GTile(s):", EditorStyles.miniLabel);
                foreach (GTile tile in _foundTiles)
                {
                    EditorGUILayout.ObjectField(tile, typeof(GTile), false);
                }

                if (GUILayout.Button("Apply to Tiles"))
                {
                    Undo.RecordObject(myScript, "Apply GTiles to TileGroup");
                    int[] chances = new int[_foundTiles.Length];
                    for (int i = 0; i < chances.Length; i++) chances[i] = 1;
                    _ = myScript.Tiles;
                    myScript.Tiles.SetUpItems(_foundTiles, chances);
                    EditorUtility.SetDirty(myScript);
                }
            }
        }
    }
}
#endif