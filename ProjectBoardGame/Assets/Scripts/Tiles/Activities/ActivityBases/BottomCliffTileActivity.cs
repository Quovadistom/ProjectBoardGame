public class BottomCliffTileActivity : TileActivity
{
    public BottomCliffTileActivity(
        SpawnCollection spawnCollection,
        TileType tileType,
        TileBiome tileBiome,
        ActivityObjectOptions activityObjectOptions) : base(
            spawnCollection,
            tileType,
            tileBiome,
            TileObjectOptions.CLIFFBOTTOM,
            activityObjectOptions)
    {
    }
}

