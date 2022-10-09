using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFlatActivity : FlatActivity
{
    public EmptyFlatActivity(
        SpawnCollection spawnCollection,
        TileType tileType,
        TileBiome tileBiome) : base(
            spawnCollection,
            tileType,
            tileBiome,
            ActivityObjectOptions.NONE)
    {
    }
}
