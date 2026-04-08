using UnityEngine;
namespace ET.GridSystem
{
    public abstract class MapDataMapper : ScriptableObject
    {
        public abstract void ImportMapData();
        public abstract void ExportMapData(TilemapAgentData mapData);
    }
}