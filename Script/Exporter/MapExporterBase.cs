using UnityEngine;
namespace ET.GridSystem
{
    public abstract class MapExporterBase : MonoBehaviour
    {
        public abstract void ExportMapData(TilemapAgentData mapData);
    }
}