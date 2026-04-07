using UnityEngine;
namespace ET.GridSystem
{
    public abstract class MapExporterBase : ScriptableObject
    {
        public abstract void ExportMapData(TilemapAgentData mapData);
    }
}