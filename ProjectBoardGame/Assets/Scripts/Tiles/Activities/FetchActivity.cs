using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchActivity : FlatActivity
{
    public override Vector3 PlayerSpawnLocation { get => base.PlayerSpawnLocation; set => base.PlayerSpawnLocation = value; }

    private int AmountOfRequiredItems = 0;

    public FetchActivity(
        SpawnCollection spawnCollection,
        TileType tileType,
        TileBiome tileBiome) : base(
            spawnCollection,
            tileType,
            tileBiome,
            ActivityObjectOptions.HOUSE | ActivityObjectOptions.NPC | ActivityObjectOptions.COLLECTIBLE)
    {
        AmountOfRequiredItems = UnityEngine.Random.Range(5, 31);
    }

    public override ActivityInfo GetTileActivityInfo()
    {
        Array values = Enum.GetValues(typeof(CollectibleObject));
        int randomInt = UnityEngine.Random.Range(1, values.Length);
        CollectibleObject randomCollectible = (CollectibleObject)values.GetValue(randomInt);

        return new ActivityInfo(randomCollectible, AmountOfRequiredItems);
    }

    public override void StartTileActivity()
    {
        base.StartTileActivity();
    }

    public void OnTileActivityFailed() => TileActivityFailed.Invoke();
    public void OnTileActivityCompleted() => TileActivityCompleted.Invoke();
}
