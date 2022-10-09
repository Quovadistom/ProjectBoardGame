using System;
using System.Collections.Generic;
using System.Reflection;

public class ActivityService : GenericSingleton<ActivityService>
{
    private List<Type> m_defaultActivityTypes = new List<Type>();
    private List<Type> m_flatActivityTypes = new List<Type>();
    private List<Type> m_bottomCliffActivityTypes = new List<Type>();
    private List<Type> m_topCliffActivityTypes = new List<Type>();

    public override void Awake()
    {
        base.Awake();

        var types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (Type type in types)
        {
            if (type.IsSubclassOf(typeof(FlatActivity)))
                m_defaultActivityTypes.Add(type);
            if (type.IsSubclassOf(typeof(FlatActivity)))
                m_flatActivityTypes.Add(type);
            if (type.IsSubclassOf(typeof(BottomCliffTileActivity)))
                m_bottomCliffActivityTypes.Add(type); 
            if (type.IsSubclassOf(typeof(TopCliffTileActivity)))
                m_topCliffActivityTypes.Add(type);
        }
    }

    public TileActivity GetRandomActivity(TileType tileType, TileBiome tileBiome, SpawnCollection spawnCollection)
    {
        Type type = tileType switch
        {
            TileType.FLAT => m_flatActivityTypes.GetRandomItem(),
            TileType.CLIFFBOTTOM => m_bottomCliffActivityTypes.GetRandomItem(),
            TileType.CLIFFTOP => m_topCliffActivityTypes.GetRandomItem(),
            _ => m_defaultActivityTypes.GetRandomItem(),
        };

        return (TileActivity) Activator.CreateInstance(type, spawnCollection, tileType, tileBiome);
    }
}