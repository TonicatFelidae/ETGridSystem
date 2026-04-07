using System;
using System.Collections.Generic;
using UnityEngine;
namespace ET.GridSystem
{
    public interface ITilemapAgentData
    {
        Dictionary<Vector3Int, string> TileData { get; set; }
        Dictionary<string, List<Vector3Int>> TileKeysByID { get; set; }
        void ForceAdd(Vector3Int pos, string ID);
    }

}
