using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFlatActivity : FlatActivity
{
    public EmptyFlatActivity(TileType tileType, TileBiome tileBiome) : base(tileType, tileBiome, TileObjectOptions.NONE)
    {
    }
}
