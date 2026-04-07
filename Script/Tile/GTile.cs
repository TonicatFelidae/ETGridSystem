using ET;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ET.GridSystem
{
    /// <summary>
    /// Use to draw the map, as ID of building
    /// </summary>
    [CreateAssetMenu(fileName = "GTile", menuName = "ET/Tiles/GTile")]
    public class GTile : Tile, IIDItem
    {
        [Space]
        [Header("GData")]
        [SerializeField] private string _id;
        public string ID => _id; // NEVER use enum for too dynamic ID
        [Header("Data as related tile")]
        public GridLayer gridLayer;
        public TileColliderType tileColliderType;
        [Header("Optional")]
        [SerializeField] public string namex = "N/A";
        public void SetID(string setID)
        {
            _id = setID;
        }
    }
}