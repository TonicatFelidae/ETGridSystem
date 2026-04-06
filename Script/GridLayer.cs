using UnityEngine;
namespace ET.GridSystem
{
    /// <summary>
    /// Layer of grid, used to separate different type of tilemap, for example, underground resource, resource, ground, floor, onfloor, normal geo
    /// </summary>
    public enum GridLayer
    {
        Base, // lowest level
        UndergroundResource,
        Underground,
        Resource,
        Ground,
        Floor,
        OnFloor,
        NormalGeo = 99,
        Normal = 100, // normal building + void space
        OnNormal,
        Shadow,
        Roof,
        OnRoof,
        //UI
        Planning, // 
        PathFinding, // path collider
        Masking, // action mask
        Water = 500,
        // Indicator
        Light = 1000,
        FogOfWar = 1100,
        FogOfWarBorder = 1200,
        //Damage
        NormalDamage = 2000,
        RoofDamage = 2001,
        GridRange = 2010,
        GridRangeH = 2011,

        DarkInfoLayer = 2100,

        Event = 9999,
    }
    public enum TileColliderType
    {
        None = 0,
        Collider = 1,
        PlayerDoor = 2,
        AllieDoor = 3,
        EnemyDoor = 4,
        VoidCollider = 5,
        SlowZone = 6,
    }
}