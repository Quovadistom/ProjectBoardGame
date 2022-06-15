public class TopCliffTileActivity : TileActivity
{
    public TopCliffTileActivity(TileType tileType, TileBiome tileBiome, TileObjectOptions tileObjectOptions) : base(tileType, tileBiome, tileObjectOptions | TileObjectOptions.CLIFFTOP)
    {
    }
}
