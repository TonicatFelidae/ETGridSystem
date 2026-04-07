using System;
using System.Collections.Generic;
using UnityEngine;
namespace ET.GridSystem
{
    public interface ITilemapAgentData
    {
        public Dictionary<Vector3Int, GTileMapData> TileData { get; set; }
        public Dictionary<string, List<Vector3Int>> TileKeysByID { get; set; }
        void Add(Vector3Int pos, string ID);
        void CreateGroupKeyByID();
    }
    [Serializable]
    public struct GTileMapData // data to indenticate
    {
        public string ID; // fucking ID, primary key use number
        public int objectID; // ID of the whole object

        public GTileMapData(string iD, int objectID)
        {
            ID = iD;
            this.objectID = objectID;
        }
    }

}
