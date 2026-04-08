using System.Collections.Generic;
using UnityEngine;
namespace ET.GridSystem
{
    public abstract class MapDataMapper : ScriptableObject
    {
        public abstract TilemapAgentData GetTilemapAgentData();
        public abstract void ExportMapData(TilemapAgentData mapData);
        public Dictionary<string, GTile> TileLookup { get; set; }
    }
}