using System;
using System.Collections.Generic;
using UnityEngine;
namespace ET.GridSystem
{
    public interface ITilemapAgentData
    {
        Dictionary<Vector3Int, GTileMapData> TileData { get; set; }
        Dictionary<string, List<Vector3Int>> TileKeysByID { get; set; }
        void ForceAdd(Vector3Int pos, string ID, string objectID = null);
    }
    [Serializable]
    public struct GTileMapData // data to indenticate
    {
        public string ID; // fucking ID, primary key use number
        public string objectID; // ID of the whole object

        public GTileMapData(string iD, string objectID)
        {
            ID = iD;
            this.objectID = objectID;
        }
    }

}
