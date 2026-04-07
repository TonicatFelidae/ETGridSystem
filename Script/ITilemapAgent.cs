using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
    public interface ITilemapAgent
    {
        GridLayer GridLayerID { get; }
        GridSize GridSize { get; }
        List<Vector3Int> Keys { get; }
        Transform Transform { get; }
        SquareRange2DInt MapSize { get; }
        TilemapRenderer TilemapRenderer { get; }
        LayerRenderer LayerRenderer { get; }
        MapExporterBase MapExporter { get; }
        TilemapAgentData MapData { get; set; }
        Tilemap Tilemap { get; }
        Grid Grid { get; }
        Vector3 CellToWorld(Vector3Int cellPosition);
        void CleanAllTiles();
        void FillMapWith(List<Vector3Int> posToFill);
        void FillMapWithDefaultMTile();
        void FillMapWithDefaultMTileGroup();
        void FillTile(Vector3Int loc, int x = 1, int y = 1);
        void FillTile(Vector3Int loc, int index, int x, int y);
        GTile GetGTile(Vector3Int loc);
        Vector2 GetRandomPositionInRandomTile_All();
        Vector2 GetRandomPositionInRandomTile_ID(string tileID);
        int GetTileIndex(Vector3Int loc);
        bool HaveDefaultTile(Vector3Int loc);
        bool HaveTile(Vector3Int loc);
        void LogBuildMap();
        void ReadAllTileIDs();
        void RemoveTile(Vector3Int loc);
        void RemoveTiles(List<Vector3Int> bodyTiles, bool updateGeometry = false);
        void RemoveTileWithoutUpdateGeometry(Vector3Int loc);
        void SetTile(Vector3Int loc);
        void SetTile(Vector3Int loc, int index);
        void SetTile(Vector3Int loc, TileBase tileBase, int rotationAngle = 0);
        void SetTiles(List<Vector3Int> bodyTiles);
        void SetTiles(List<Vector3Int> bodyTiles, int index);
        void SetTiles(List<Vector3Int> bodyTiles, TileBase tileBase, bool updateGeometry = false);
        void Show(bool enable);
        void UpdateGeometry();
        void ExportMapData();
        UnityAction<Vector3Int, string> ConstructTile { get; set; }


    }
}