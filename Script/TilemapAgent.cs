using System.Collections.Generic;
using ET.SupportKit;
using ET.SupportKit.Collection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
    public class TilemapAgentBase : MonoBehaviour, ITilemapAgent
    {
        public Transform Transform => transform;
        public SquareRange2DInt MapSize => _mapSize;
        private SquareRange2DInt _mapSize;
        public List<Vector3Int> Keys { get; }
        public void Show(bool enable) => gameObject.SetActive(enable);
        #region Map Builder
        public void CleanAllTiles() => Tilemap.ClearAllTiles();

        public void FillMapWith(List<Vector3Int> posToFill)
        {
            CleanAllTiles();
            foreach (var item in posToFill)
            {
                Tilemap.SetTile(item, defaultTiles.GetTile());
            }
        }
        public void FillMapWithDefaultMTile()
        {
            if (defaultTile == null)
            {
                Debug.LogError("[TilemapAgent] defaultTile not set");
                return;
            }
            CleanAllTiles();
            for (int i = MapSize.minX; i < MapSize.maxX; i++)
            {
                for (int j = MapSize.minY; j < MapSize.maxY; j++)
                {
                    Vector2Int pos = new Vector2Int(i, j);
                    Tilemap.SetTile(pos.ToVector3Int(), defaultTile);
                }
            }
        }
        public void FillMapWithDefaultMTileGroup()
        {
            if (defaultTile == null)
            {
                Debug.LogError("[TilemapAgent] defaultTiles not set");
                return;
            }
            CleanAllTiles();
            for (int i = MapSize.minX; i < MapSize.maxX; i++)
            {
                for (int j = MapSize.minY; j < MapSize.maxY; j++)
                {
                    Vector2Int pos = new Vector2Int(i, j);
                    Tilemap.SetTile(pos.ToVector3Int(), defaultTiles.GetTile());
                }
            }
        }

        /// <summary>
        /// Fill all tiles in a rectangle with X x Y demention with default tile.
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void FillTile(Vector3Int loc, int x = 1, int y = 1)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Vector3Int curLoc = new Vector3Int(loc.x + i, j + loc.y);
                    Tilemap.SetTile(curLoc, defaultTiles.GetTile());
                }
            }
        }
        /// <summary>
        /// Fill all tiles in a rectangle with X x Y demention with a tile in tile group
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void FillTile(Vector3Int loc, int index, int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Vector3Int curLoc = new Vector3Int(loc.x + i, j + loc.y);
                    Tilemap.SetTile(curLoc, defaultTiles.GetTile(index));
                    defaultTiles.SetIndexAt(curLoc, index);
                    CheckAndAddTileData(curLoc);
                }
            }
        }
        public void LogBuildMap()
        {
            if (transform.childCount > 0)
            {
                for (int i = MapSize.minX; i < MapSize.maxX; i++)
                {
                    for (int j = MapSize.minY; j < MapSize.maxY; j++)
                    {
                        Vector2Int pos = new Vector2Int(i, j);
                        GameObject go = Tilemap.GetInstantiatedObject(pos.ToVector3Int());
                    }
                }
            }
            else
            {
                D.LogWarning("[TILEMAP] Tilemap not empty, ReadMapWithDefaultGTile failed");
            }
        }
        #endregion
        #region Tile Builder

        public void SetTile(Vector3Int loc) => SetTile(loc, defaultTile);
        public void SetTile(Vector3Int loc, int index) => SetTile(loc, defaultTiles.GetTile(index));
        public void SetTile(Vector3Int loc, TileBase tileBase, int rotationAngle = 0)
        {
            Tilemap.SetTile(loc, null);
            Tilemap.SetTile(loc, tileBase);
        }
        public void RemoveTile(Vector3Int loc)
        {
            Tilemap.SetTile(loc, null);
            mapData.TileData.Remove(loc);
        }

        public void SetTiles(List<Vector3Int> bodyTiles) => SetTiles(bodyTiles, defaultTile);
        public void SetTiles(List<Vector3Int> bodyTiles, int index) => SetTiles(bodyTiles, defaultTiles.GetTile(index));
        public void SetTiles(List<Vector3Int> bodyTiles, TileBase tileBase, bool updateGeometry = false)
        {
            foreach (var loc in bodyTiles)
            {
                SetTile(loc, tileBase);
            }
            if (updateGeometry) UpdateGeometry();
        }
        public void RemoveTiles(List<Vector3Int> bodyTiles, bool updateGeometry = false)
        {
            foreach (var loc in bodyTiles)
            {
                Tilemap.SetTile(loc, null);
                mapData.TileData.Remove(loc);
            }
            if (updateGeometry) UpdateGeometry();
        }

        public void RemoveTileWithoutUpdateGeometry(Vector3Int loc)
        {
            Tilemap.SetTile(loc, null);
            mapData.TileData.Remove(loc);
        }


        public void UpdateGeometry()
        {
            if (compositeCollider2D != null)
            {
                Tilemap.GetComponent<TilemapCollider2D>().ProcessTilemapChanges();
                compositeCollider2D.GenerateGeometry();
            }
        }
        #endregion
        #region Map Data

        public Vector2 GetRandomPositionInRandomTile_All()
        {
            var item = mapData.TileData.RandomElement(); // posible bug is not read tileData
            return _grid.GetCellCenterWorld(item.Key);
        }
        public Vector2 GetRandomPositionInRandomTile_ID(string tileID)
        {
            var item = mapData.TileKeysByID[tileID].RandomElement(); // posible bug is not read tileData, or not group the keys
            return _grid.GetCellCenterWorld(item);
        }
        /// <summary>
        /// Get index from tile group
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetTileIndex(Vector3Int loc) => defaultTiles.GetIndexAt(loc);
        public bool HaveDefaultTile(Vector3Int loc)
        {
            if (Tilemap.GetTile(loc) && Tilemap.GetTile(loc) == defaultTiles.GetTile()) return true;
            return false;
        }

        public bool HaveTile(Vector3Int loc) => Tilemap.GetTile(loc);
        public GTile GetGTile(Vector3Int loc) => (GTile)Tilemap.GetTile(loc);


        public void ReadAllTileIDs()
        {
            mapData.TileData = new Dictionary<Vector3Int, GTileMapData>();
            for (int i = 0; i < Keys.Count; i++)
            {
                TileBase tile = Tilemap.GetTile(Keys[i]);
                if (tile is IIDItem tileIDItem)
                {
                    mapData.Add(Keys[i], tileIDItem.ID);
                }
            }
        }
        #endregion
        #region Support Function
        public Vector3 CellToWorld(Vector3Int cellPosition) => _grid.CellToWorld(cellPosition);
        #endregion


        public GridLayer gridLayerID;
        private UnityEngine.Grid _grid;
        private Tilemap Tilemap
        {
            get
            {
                if (tilemap == null)
                    tilemap = GetComponent<Tilemap>();
                return tilemap;
            }
        }

        BoundsInt Bounds => Tilemap.cellBounds;

        public TileBase defaultTile;
        public TileGroup defaultTiles;
        public ITilemapAgentData mapData;
        public bool GenerateTileNameInDebug = false;
        public CompositeCollider2D compositeCollider2D;
        public TilemapRenderer TilemapRenderer
        {
            get
            {
                if (tilemapRenderer == null)
                    tilemapRenderer = GetComponent<TilemapRenderer>();
                return tilemapRenderer;
            }

        }
        public TilemapRenderer tilemapRenderer;

        public LayerRenderer LayerRenderer
        {
            get
            {
                if (layerRenderer == null)
                    layerRenderer = GetComponent<LayerRenderer>();
                return layerRenderer;
            }

        }

        public UnityAction<Vector3Int, GTileMapData> ConstructTile { get; set; }

        public LayerRenderer layerRenderer;


        public Tilemap tilemap;

        /// <summary>
        /// Read map data from ATile, use for alpha ver tion only, read data ridectly from map. After saveload system ok the new flow will be:  read data ridectly from map +> save file => load file to render map
        /// </summary>
        public void ReadPrebuildMap(bool groupDataKeysByID = false,
            bool generateCollider = false,
            bool addLayerItem = false,
            LayerSystemLayerType layerSystemLayerType = LayerSystemLayerType.Normal) // 
        {
            //Profiler.BeginSample("ReadAllTileIDs");
            ReadAllTileIDs();
            //Profiler.EndSample();   
            //Profiler.BeginSample("ConstructBuildingFromTileID");

            foreach (var item in mapData.TileData)
            {
                ConstructTile?.Invoke(item.Key, item.Value);
                //TODO construct from object ID
            }
            //Profiler.EndSample();
            //Profiler.BeginSample("CreateGroupKeyByID");
            if (groupDataKeysByID)
            {
                mapData.CreateGroupKeyByID();
            }
            //Profiler.EndSample();
            //Profiler.BeginSample("GenerateGeometry");
            if (generateCollider)
            {
                compositeCollider2D?.GenerateGeometry();
                //GridSPCompositeColliderShrinker.ShrinkCollider(compositeCollider2D, 0.4f);
            }
            //Profiler.EndSample();
            if (addLayerItem) AddLayerItem(layerSystemLayerType);
        }
        public void AddLayerItem(LayerSystemLayerType layerSystemLayerType = LayerSystemLayerType.Normal)
        {
            LayerRenderer.Add(this.gameObject, layerSystemLayerType);
        }
        public void UpdateMap(
            bool generateCollider = false,
            bool addLayerItem = false,
            LayerSystemLayerType layerSystemLayerType = LayerSystemLayerType.Normal) // 
        {
            if (generateCollider)
            {
                compositeCollider2D?.GenerateGeometry();
                //GridSPCompositeColliderShrinker.ShrinkCollider(compositeCollider2D, 0.4f);
            }
            if (addLayerItem)
            {
                LayerRenderer.Add(this.gameObject, layerSystemLayerType);
            }
        }
        void CheckAndAddTileData(Vector3Int loc)
        {
            if (!mapData.TileData.ContainsKey(loc))
            {
                mapData.TileData.Add(loc, new GTileMapData());
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (mapData.TileData == null || !GenerateTileNameInDebug)
                return;

            foreach (var tile in mapData.TileData)
            {
                Vector3Int cellPosition = tile.Key;
                Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, cellPosition.z);
                // Draw the label
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.red;
                UnityEditor.Handles.Label(worldPosition, $"({cellPosition.x}, {cellPosition.y})", style);
            }
        }

        public void CreateJsonFileFromMap()
        {
            throw new System.NotImplementedException();
        }

#endif
    }
}
