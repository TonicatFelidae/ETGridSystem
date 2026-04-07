using ET.GridSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
    public class GridSPLinq : MonoBehaviour
    {
        public static Dictionary<string, List<Vector3Int>> GroupByTileDataID(Dictionary<Vector3Int, GTileMapData> tileData)
        {
            return tileData
                .GroupBy(kv => kv.Value.ID)
                .ToDictionary(g => g.Key, g => g.Select(kv => kv.Key).ToList());
        }

        public static T[] MergeTwoArray<T>(T[] array0, T[] array1)
        {
            return array0.Concat(array1).ToArray();
        }
    }
}