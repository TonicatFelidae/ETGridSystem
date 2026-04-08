using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ETEditor;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace ET.GridSystem
{
    public class TilemapDrawer : MonoBehaviour
    {
        public Tilemap Tilemap
        {
            get
            {
                if (_tilemap == null)
                    _tilemap = GetComponent<Tilemap>();
                return _tilemap;
            }
        }
        private Tilemap _tilemap;

        [Header("Auto Load Tiles (Resources/Tiles)")]
        [SerializeField] private bool _autoLoadFromResources = true;

        [Header("Manual Tile List (optional)")]
        [SerializeField] private List<GTile> _tiles;

        // Runtime lookup: ID → GTile
        private Dictionary<string, GTile> _tileLookup;

        // Runtime data: position → ID
        public Dictionary<Vector3Int, string> TileData { get; private set; } = new();

        private void Awake()
        {
            BuildLookup();
        }

        private void BuildLookup()
        {
            _tileLookup = new Dictionary<string, GTile>();

            IEnumerable<GTile> sourceTiles;

            if (_autoLoadFromResources)
            {
                // Load all tiles from Resources/Tiles
                sourceTiles = Resources.LoadAll<GTile>("Tiles");
            }
            else
            {
                sourceTiles = _tiles;
            }

            foreach (var tile in sourceTiles)
            {
                if (tile == null) continue;

                if (!_tileLookup.ContainsKey(tile.ID))
                {
                    _tileLookup.Add(tile.ID, tile);
                }
                else
                {
                    Debug.LogWarning($"Duplicate tile ID detected: {tile.ID}");
                }
            }
        }

        // =========================
        // DRAW (ID → Tilemap)
        // =========================
        public void DrawTilemap()
        {
            if (_tileLookup == null || TileData == null) return;

            _tilemap.ClearAllTiles();

            foreach (var pair in TileData)
            {
                Vector3Int position = pair.Key;
                string tileID = pair.Value;

                if (_tileLookup.TryGetValue(tileID, out GTile tile))
                {
                    _tilemap.SetTile(position, tile);
                }
                else
                {
                    Debug.LogWarning($"Missing tile for ID: {tileID}");
                }
            }
        }

        // =========================
        // EXTRACT (Tilemap → ID)
        // =========================
        public void ExtractFromTilemap()
        {
            TileData.Clear();

            var bounds = _tilemap.cellBounds;

            foreach (var pos in bounds.allPositionsWithin)
            {
                var tile = _tilemap.GetTile(pos) as GTile;
                if (tile == null) continue;

                TileData[pos] = tile.ID;
            }
        }

        // =========================
        // SINGLE TILE OPS
        // =========================
        public void SetTile(Vector3Int pos, string id)
        {
            if (_tileLookup.TryGetValue(id, out GTile tile))
            {
                _tilemap.SetTile(pos, tile);
                TileData[pos] = id;
            }
            else
            {
                Debug.LogWarning($"Tile ID not found: {id}");
            }
        }

        public void ClearTile(Vector3Int pos)
        {
            _tilemap.SetTile(pos, null);
            TileData.Remove(pos);
        }

        public string GetTileID(Vector3Int pos)
        {
            return TileData.TryGetValue(pos, out var id) ? id : null;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TilemapDrawer))]
    public class TilemapDrawerEditor : Editor
    {
        private EditorFolderPicker _folderPicker;

        private void OnEnable()
        {
            _folderPicker = new EditorFolderPicker(
                prefKey: "TilemapDrawerEditor_FolderPath",
                defaultPath: Application.dataPath + "/Resources/Tiles",
                panelTitle: "Select Tile Directory"
            );
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TilemapDrawer drawer = (TilemapDrawer)target;

            EditorGUILayout.Space(12);
            EditorGUILayout.LabelField("── Editor Tools ──", EditorStyles.boldLabel);
            EditorGUILayout.Space(4);

            // ── Select folder (direction) ──
            _folderPicker.DrawLayout("Tile Folder (Direction)");

            EditorGUILayout.Space(6);

            // ── Add All GTiles from folder ──
            if (GUILayout.Button("Add All GTiles from Direction"))
            {
                if (string.IsNullOrEmpty(_folderPicker.SelectedPath))
                {
                    Debug.LogWarning("[TilemapDrawer] No folder selected. Click 'Select Tile Directory' first.");
                    return;
                }

                if (!_folderPicker.IsValidAssetFolder)
                {
                    Debug.LogWarning($"[TilemapDrawer] Folder not found in project: {_folderPicker.AssetFolderPath}");
                    return;
                }

                GTile[] loaded = _folderPicker.LoadAll<GTile>();
                if (loaded.Length == 0)
                {
                    Debug.LogWarning($"[TilemapDrawer] No GTiles found in {_folderPicker.AssetFolderPath}");
                    return;
                }

                SerializedProperty tilesProp = serializedObject.FindProperty("_tiles");
                SerializedProperty autoLoad = serializedObject.FindProperty("_autoLoadFromResources");

                autoLoad.boolValue = false;

                var existingSet = new System.Collections.Generic.HashSet<GTile>();
                for (int i = 0; i < tilesProp.arraySize; i++)
                {
                    var existing = tilesProp.GetArrayElementAtIndex(i).objectReferenceValue as GTile;
                    if (existing != null) existingSet.Add(existing);
                }

                int added = 0;
                foreach (var tile in loaded)
                {
                    if (existingSet.Contains(tile)) continue;
                    tilesProp.arraySize++;
                    tilesProp.GetArrayElementAtIndex(tilesProp.arraySize - 1).objectReferenceValue = tile;
                    existingSet.Add(tile);
                    added++;
                }

                serializedObject.ApplyModifiedProperties();
                Debug.Log($"[TilemapDrawer] Added {added} GTile(s) from {_folderPicker.AssetFolderPath}");
            }

            EditorGUILayout.Space(6);

            // ── Draw Tilemap ──
            if (GUILayout.Button("Draw Tilemap"))
            {
                drawer.DrawTilemap();
            }

            EditorGUILayout.Space(4);
        }
    }
#endif
}