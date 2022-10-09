public class EmptyTopCliffTileActivity : TopCliffTileActivity
{
    public EmptyTopCliffTileActivity(
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
