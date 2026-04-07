using System;
using System.Collections.Generic;
using ET.SupportKit.Collection;
using UnityEngine;
namespace ET.GridSystem
{
    [Serializable]
    public class TilemapAgentData : ITilemapAgentData
    {
        public Dictionary<Vector3Int, GTileMapData> tileData = new(); // Immutatble 
        public GridLayer gridLayerID;
        public GridSize gridSize;
        private Dictionary<string, List<Vector3Int>> _tileKeysByID;
        public Dictionary<string, List<Vector3Int>> TileKeysByID
        {
            get
            {
                if (_tileKeysByID == null)
                {
                    _tileKeysByID = GridSPLinq.GroupByTileDataID(TileData);
                }
                return _tileKeysByID;
            }
            set { _tileKeysByID = value; }
        }
        public Dictionary<Vector3Int, GTileMapData> TileData { get => tileData; set => tileData = value; }

        public void ForceAdd(Vector3Int pos, string ID) => TileData.ForceAdd(pos, new GTileMapData(ID, 0));

    }
}

