using System.Collections.Generic;
using UnityEngine;
namespace ET.GridSystem
{
    [CreateAssetMenu(fileName = "MapDataPalette", menuName = "ET/GridSystem/MapDataPalette")]
    public class MapDataPalette : ScriptableObject
    {
        public List<GTile> tileList;
        public GTile GetGTileByID(string id)
        {
            return tileList.Find(tile => tile.ID == id);
        }
    }
}
