using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SpawnPointSize
{
    SMALL,
    MEDIUM,
    LARGE,
    TILEPART,
    TILE
}

public class SpawnPoint
{
    public Vector3 Position { get; }
    public Quaternion Rotation { get; }
    public Vector3 Scale { get; }
    public SpawnPointSize SpawnPointSize { get; }
    public bool IsOccupied { get; set; } = false;

    public SpawnPoint(Vector3 position, Quaternion rotation, Vector3 scale, SpawnPointSize spawnPointSize)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
        SpawnPointSize = spawnPointSize;
    }

    public SpawnPoint GetDerivedSpawnTransform(Vector3 location, Quaternion rotation, Vector3 scale, SpawnPointSize spawnPointSize)
    {
        return new SpawnPoint(Position + location, Rotation * rotation, scale, spawnPointSize);
    }
}

public class TileFiller
{
    private TileObjectOptions m_tileNecessities;
    private ActivityObjectOptions m_activityObjectOptions;
    private TileType m_tileType;
    private TileBiome m_tileBiome;
    private SpawnCollection m_spawnCollection;
    private TileAssets m_tileAssets;

    private List<SpawnPoint> m_spawnPoints = new List<SpawnPoint>();

    public TileFiller(TileObjectOptions tileNecessities, ActivityObjectOptions activityObjectOptions, TileType tileType, TileBiome tileBiome, SpawnCollection spawnCollection)
    {
        m_tileNecessities = tileNecessities;
        m_activityObjectOptions = activityObjectOptions;
        m_tileType = tileType;
        m_tileBiome = tileBiome;
        m_tileBiome = tileBiome;
        m_spawnCollection = spawnCollection;

        m_tileAssets = ScriptableObjectService.Instance.GetScriptableObject<TileAssets>();

        Fill();
    }

    private void Fill()
    {
        AddSpawnPoints(m_tileAssets.BaseTile.SpawnLocations);

        foreach (TileObjectOptions value in Enum.GetValues(typeof(TileObjectOptions)))
        {
            if (value != TileObjectOptions.NONE && m_tileNecessities.HasFlag(value))
            {
                AddStaticObject(value.ToString());
            }
        }

        var emptyTilePartList = new List<SpawnPoint>(m_spawnPoints.Where(spawnPoint => spawnPoint.SpawnPointSize == SpawnPointSize.TILEPART && !spawnPoint.IsOccupied));
        foreach (var spawnPoints in emptyTilePartList)
        {
            AddStaticObject(TileObjectOptions.FLAT.ToString());
        }

        /*
        foreach (ActivityObjectOptions value in Enum.GetValues(typeof(ActivityObjectOptions)))
        {
            if (value != ActivityObjectOptions.NONE && m_activityObjectOptions.HasFlag(value))
            {
                AddStaticObject(value.ToString());
            }
        }
        */
    }

    private void AddStaticObject(string enumName)
    {
        int index = m_tileAssets.TryGetRandomStaticObject(enumName, out TileMetaData tileMetaData);
        if (index >= 0)
        {
            SpawnPoint spawnPoint = m_spawnPoints.Where(spawnPoint => spawnPoint.SpawnPointSize == tileMetaData.SpawnPointSize && !spawnPoint.IsOccupied).ToList().GetRandomItem();
            m_spawnCollection.SpawnObjects.Add(
                new SpawnObject(spawnPoint.Position,
                spawnPoint.Rotation,
                spawnPoint.Scale,
                enumName,
                index));

            AddDerivedSpawnPoints(spawnPoint, tileMetaData.SpawnLocations);

            spawnPoint.IsOccupied = true;
        }
    }

    private void AddSpawnPoints(List<SpawnPointMetaData> spawnPointMetaDataList)
    {
        foreach (SpawnPointMetaData spawnPointMetaData in spawnPointMetaDataList)
        {
            SpawnPoint spawnTransform = new SpawnPoint(spawnPointMetaData.Transform.position,
                spawnPointMetaData.Transform.rotation,
                spawnPointMetaData.Transform.localScale,
                spawnPointMetaData.SpawnPointSize);

            m_spawnPoints.Add(spawnTransform);
        }
    }

    private void AddDerivedSpawnPoints(SpawnPoint parentSpawnPoint, List<SpawnPointMetaData> spawnPointMetaDataList)
    {
        foreach (SpawnPointMetaData spawnPointMetaData in spawnPointMetaDataList)
        {
            SpawnPoint spawnTransform = parentSpawnPoint.GetDerivedSpawnTransform(spawnPointMetaData.Transform.position,
                spawnPointMetaData.Transform.rotation,
                spawnPointMetaData.Transform.localScale,
                spawnPointMetaData.SpawnPointSize);

            m_spawnPoints.Add(spawnTransform);
        }
    }
}
