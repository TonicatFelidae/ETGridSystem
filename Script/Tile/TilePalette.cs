using System;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace ET.GridSystem
{
    [Serializable]
    public struct TilePalette
    {
        [Header("TILE PALETTES")]
        public TileBase defaultTileBase;
        public TileGroup<TileBase> defaultTileBases;
        public TileGroup<GTile> defaultGTiles;

    }
}
