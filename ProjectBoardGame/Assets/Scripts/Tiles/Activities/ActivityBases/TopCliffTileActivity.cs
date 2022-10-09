public class TopCliffTileActivity : TileActivity
{
    public TopCliffTileActivity(
        SpawnCollection spawnCollection,
        TileType tileType,
        TileBiome tileBiome,
        ActivityObjectOptions activityObjectOptions) : base(
            spawnCollection,
            tileType,
            tileBiome,
            TileObjectOptions.CLIFFTOP,
            activityObjectOptions)
    {
    }
}
