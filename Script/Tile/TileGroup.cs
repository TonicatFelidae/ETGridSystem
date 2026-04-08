using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ET.SupportKit.Collection;
using ET.PowerStruct;

namespace ET.GridSystem
{
    // MTile mapping tile: use to map object
    /// <summary>
    /// ET tilemap, group
    /// - enable easy control over tile group for project
    /// - get tile by ID
    /// - get tile random with setting
    /// </summary>
    [CreateAssetMenu(fileName = "TileGroup", menuName = "ET/Tiles/TileGroup")]
    public class TileGroup : ScriptableObject
    {
        [SerializeField] private SimpleChanceGroup<TileBase> _tiles;
        public SimpleChanceGroup<TileBase> Tiles
        {
            get
            {
                return _tiles;
            }
        }
        private Dictionary<Vector3Int, int> _indexMap = new(); // cache for added tile
        public TileBase GetTile()
        {
            return Tiles.Get(0);
        }
        public TileBase GetTile(int order)
        {
            return Tiles.Get(order);
        }
        public TileBase GetRandomTile()
        {
            return Tiles.GetRandom();
        }
        public void SetIndexAt(Vector3Int loc, int index)
        {
            _indexMap.ForceAdd(loc, index);
        }
        public int GetIndexAt(Vector3Int loc)
        {
            if (_indexMap.ContainsKey(loc)) return _indexMap[loc];
            else return -1;

        }
        public struct TileGroupTilebase
        {
            public TileBase tileBase;


        }
    }

}


