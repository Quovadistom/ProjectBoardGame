using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFiller
{
    private TileObjectOptions m_tileNecessities;
    private TileType m_tileType;
    private TileBiome m_tileBiome;

    public TileFiller(TileObjectOptions tileNecessities, TileType tileType, TileBiome tileBiome)
    {
        m_tileNecessities = tileNecessities;
        m_tileType = tileType;
        m_tileBiome = tileBiome;

        Fill();
    }

    private void Fill()
    {
        string tileNecessities = "-";
        foreach (TileObjectOptions value in Enum.GetValues(typeof(TileObjectOptions)))
        {
            if (value != TileObjectOptions.NONE && m_tileNecessities.HasFlag(value))
            {
                TileObject tileObject = (TileObject)value;

                TileAssets m_tileAssets = ScriptableObjectService.Instance.GetScriptableObject<TileAssets>();
                TileMetaData tileMetaData = GameObject.Instantiate(m_tileAssets.BaseTile);

                if (tileObject == TileObject.CLIFFBOTTOM || tileObject == TileObject.CLIFFTOP || tileObject == TileObject.HOUSE)
                {
                    TileMetaData tileMetaData1 = GameObject.Instantiate(m_tileAssets.SingleSpawn, tileMetaData.transform);
                    Transform transform = tileMetaData.SpawnLocations.GetRandomItem();
                    SetTransform(transform, tileMetaData1.transform);
                }

                tileNecessities += tileObject + "-";
            }
        }

        Debug.Log($"This tile is of type {m_tileType}, has biome {m_tileBiome} and needs {tileNecessities}") ;
    }

    private void SetTransform(Transform transform, Transform targetTransform)
    {
        targetTransform.localPosition = transform.localPosition;
        targetTransform.localRotation = transform.localRotation;
        //targetTransform.localScale = transform.localScale;
    }
}
