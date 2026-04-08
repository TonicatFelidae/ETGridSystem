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
        [SerializeField] private MapDataMapper _mapExporter;
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

        // Runtime lookup: ID → GTile
        public Dictionary<string, GTile> TileLookup => _mapExporter.TileLookup;

        // Runtime data: position → ID
        private Dictionary<Vector3Int, string> TileData => _mapExporter.GetTilemapAgentData().TileData;
        public void DrawTilemap()
        {
            // Clear the existing tilemap
            Tilemap.ClearAllTiles();
            if (TileData == null || TileData.Count == 0)
            {
                Debug.LogError("[TilemapDrawer] No tile data available to draw the tilemap.");
                return;
            }
            if (TileLookup == null || TileLookup.Count == 0)
            {
                Debug.LogError("[TilemapDrawer] No tile lookup available to draw the tilemap.");
                return;
            }


            // Get the data from the map exporter
            var data = _mapExporter.GetTilemapAgentData();

            if (data == null || data.TileData == null)
            {
                Debug.LogWarning("[TilemapDrawer] No data found to draw the tilemap.");
                return;
            }

            // Iterate through the data and set tiles
            foreach (var entry in data.TileData)
            {
                Vector3Int position = entry.Key;
                string tileID = entry.Value;

                if (TileLookup.TryGetValue(tileID, out GTile tile))
                {
                    Tilemap.SetTile(position, tile);
                    TileData[position] = tileID;
                }
                else
                {
                    Debug.LogWarning($"[TilemapDrawer] Tile ID not found: {tileID}");
                }
            }
        }
        public void ExtractFromTilemap()
        {
            TileData.Clear();

            var bounds = Tilemap.cellBounds;

            foreach (var pos in bounds.allPositionsWithin)
            {
                var tile = Tilemap.GetTile(pos) as GTile;
                if (tile == null) continue;

                TileData[pos] = tile.ID;
            }
        }
        public void SetTile(Vector3Int pos, string id)
        {
            if (TileLookup.TryGetValue(id, out GTile tile))
            {
                Tilemap.SetTile(pos, tile);
                TileData[pos] = id;
            }
            else
            {
                Debug.LogWarning($"Tile ID not found: {id}");
            }
        }

        public void ClearTile(Vector3Int pos)
        {
            Tilemap.SetTile(pos, null);
            TileData.Remove(pos);
        }

        public string GetTileID(Vector3Int pos)
        {
            return TileData.TryGetValue(pos, out var id) ? id : null;
        }
    }


}