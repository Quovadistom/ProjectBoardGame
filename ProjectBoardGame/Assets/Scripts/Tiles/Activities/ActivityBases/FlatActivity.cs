using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatActivity : TileActivity
{
    public FlatActivity(
        SpawnCollection spawnCollection,
        TileType tileType,
        TileBiome tileBiome,
        ActivityObjectOptions activityObjectOptions) : base(
            spawnCollection,
            tileType,
            tileBiome,
            TileObjectOptions.FLAT,
            activityObjectOptions)
    {
    }
}
