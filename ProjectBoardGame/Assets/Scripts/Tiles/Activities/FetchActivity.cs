using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchActivity : FlatActivity
{
    public override Vector3 PlayerSpawnLocation { get => base.PlayerSpawnLocation; set => base.PlayerSpawnLocation = value; }

    public FetchActivity(TileType tileType, TileBiome tileBiome) : base(tileType, tileBiome, TileObjectOptions.HOUSE | TileObjectOptions.NPC | TileObjectOptions.COLLECTIBLE)
    {
    }

    public override void StartTileActivity()
    {
        base.StartTileActivity();
    }

    public void OnTileActivityFailed() => TileActivityFailed.Invoke();
    public void OnTileActivityCompleted() => TileActivityCompleted.Invoke();
}
