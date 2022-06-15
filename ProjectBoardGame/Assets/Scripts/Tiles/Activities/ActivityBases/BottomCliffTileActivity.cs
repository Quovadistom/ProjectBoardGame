public class BottomCliffTileActivity : TileActivity
{
    public BottomCliffTileActivity(TileType tileType, TileBiome tileBiome, TileObjectOptions tileObjectOptions) : base(tileType, tileBiome, tileObjectOptions | TileObjectOptions.CLIFFBOTTOM)
    {
    }
}

