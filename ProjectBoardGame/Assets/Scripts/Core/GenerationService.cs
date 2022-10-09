using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerationService : GenericSingleton<GenerationService>
{
    public Action GenerationFinished;

    private Dictionary<Tile, ActivityInfo> TileActivityInfoList = new Dictionary<Tile, ActivityInfo>();

    public void StartGeneration(IEnumerable<Tile> tiles)
    {
        // Choose first and last tile
        // Generate path(s) between first and last tile
        // Save tiles for path in list and create specific activities?

        foreach (Tile tile in tiles)
        {
            TileActivityInfoList.Add(tile, tile.CreateActivity());

            Debug.Log($"Amount: {TileActivityInfoList.Last().Value.AmountOfNeededCollectibles}, type: {TileActivityInfoList.Last().Value.CollectibleObject}");
        }

        foreach (var item in TileActivityInfoList.Where(x => x.Value.CollectibleObject != CollectibleObject.NONE))
        {
            int countPerNeighboringTile = (int)Mathf.Ceil((item.Value.AmountOfNeededCollectibles / (item.Key.NeighboringTiles.Length + 1)));

            // item.Key.AddCollectibles(x.Value.CollectibleObject, countPerNeighboringTile);

            foreach (Tile tile in item.Key.NeighboringTiles)
            {
                // tile.AddCollectibles(x.Value.CollectibleObject, countPerNeighboringTile);
            }
        }

        foreach (var item in TileActivityInfoList)
        {
            // tile.Finalize();
        }

        GenerationFinished?.Invoke();
    }
}
