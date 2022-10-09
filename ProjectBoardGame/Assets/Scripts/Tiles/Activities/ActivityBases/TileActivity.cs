using System;
using UnityEngine;

/// <summary>
/// Only three of these are allowed on one tile
/// </summary>
[Flags]
public enum TileObjectOptions
{
    NONE = 0,
    CLIFFBOTTOM = 2,
    CLIFFTOP = 4,
    FLAT
}

[Flags]
public enum ActivityObjectOptions
{
    NONE = 0,
    HOUSE = 1,
    NPC = 2,
    COLLECTIBLE = 4
}

public enum CollectibleObject
{
    NONE,
    FRUIT,
    NAILS
}

public class TileActivity : ITileActivity
{
    public TileFiller TileFiller { get; }

    public virtual Vector3 PlayerSpawnLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action TileActivityFailed;
    public Action TileActivityCompleted;

    public TileActivity(SpawnCollection spawnCollection, 
        TileType tileType, 
        TileBiome tileBiome, 
        TileObjectOptions tileObjectOptions, 
        ActivityObjectOptions activityObjectOptions)
    {
        TileFiller = new TileFiller(tileObjectOptions, activityObjectOptions, tileType, tileBiome, spawnCollection);
    }

    public virtual ActivityInfo GetTileActivityInfo()
    {
        return new ActivityInfo(CollectibleObject.NONE, 0);
    }

    public virtual void StartTileActivity()
    {
        throw new NotImplementedException();
    }
}
