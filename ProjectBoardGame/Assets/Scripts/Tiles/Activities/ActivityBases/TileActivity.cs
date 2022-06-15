using System;
using UnityEngine;

public class TileActivity : ITileActivity
{
    protected TileFiller TileFiller;

    public virtual Vector3 PlayerSpawnLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Action TileActivityFailed;
    public Action TileActivityCompleted;

    public TileActivity(TileType tileType, TileBiome tileBiome, TileObjectOptions tileObjectOptions)
    {
        TileFiller = new TileFiller(tileObjectOptions, tileType, tileBiome);
    }

    public virtual void StartTileActivity()
    {
        throw new NotImplementedException();
    }
}
