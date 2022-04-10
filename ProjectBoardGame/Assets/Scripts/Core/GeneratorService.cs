using System;
using System.Collections.Generic;
using UnityEngine;
using static ResourcesSettings;

public class GeneratorService : GenericSingleton<GeneratorService>
{
    public Dictionary<Vector3, RoomObject> DebugObjects = new Dictionary<Vector3, RoomObject>();
    public Dictionary<Vector3, SpawnObject> SpawnObjects = new Dictionary<Vector3, SpawnObject>();
    private int m_finishedGenerators = 0;
    private List<GeneratedCollection> m_roomGenerators = new List<GeneratedCollection>();
    private ResourcesSettings m_resourcesSettings;
    private DebugSettings m_debugSettings;

    public GeneratorService()
    {
        m_resourcesSettings = ScriptableObjectService.Instance.GetScriptableObject<ResourcesSettings>();
        m_debugSettings = ScriptableObjectService.Instance.GetScriptableObject<DebugSettings>();
    }

    public void AddGeneratedCollection(GeneratedCollection roomGenerator)
    {
        m_roomGenerators.Add(roomGenerator);
        roomGenerator.OnGeneratingDone += GenerationDone;
    }

    public void AddDebugObjects(IEnumerable<RoomObject> debugObjects)
    {
        foreach (var debugObject in debugObjects)
        {
            if (DebugObjects.ContainsKey(debugObject.Position)) { return; }

            DebugObjects.Add(debugObject.Position, debugObject);
        }
    }

    public void AddSpawnObject(SpawnObject spawnObject)
    {
        if (SpawnObjects.ContainsKey(spawnObject.Position)) { return; }

        SpawnObjects.Add(spawnObject.Position, spawnObject);
    }

    public void AddSpawnObjects(IEnumerable<SpawnObject> spawnObjects)
    {
        foreach (SpawnObject spawnObject in spawnObjects)
        {
            if (SpawnObjects.ContainsKey(spawnObject.Position)) { return; }

            SpawnObjects.Add(spawnObject.Position, spawnObject);
        }
    }

    private void GenerationDone()
    {
        m_finishedGenerators++;
        if (m_finishedGenerators == m_roomGenerators.Count)
        {
            if (m_debugSettings.ShowDebugAssets)
                SpawnService.Instance.SpawnDebugObjects(DebugObjects.Values);
            if (m_debugSettings.ShowGameAssets)
            {
                AssignGameObjects(SpawnObjects.Values);

                SpawnService.Instance.SpawnGameObjects(SpawnObjects.Values);
            }
        }
    }

    private void AssignGameObjects(IEnumerable<SpawnObject> values)
    {
        Theme theme = m_resourcesSettings.GetTheme();

        foreach (SpawnObject spawnObject in values)
        {
            WeightedItem<GameObject>[] gameObjectList = m_resourcesSettings.GetCorrectObjectList(spawnObject, theme);

            if (gameObjectList == null)
            {
                Debug.LogWarning($"No corresponding GameObjects of type {spawnObject.Type} found. Please add this type!");
                continue;
            }

            spawnObject.Index = gameObjectList.GetRandomIndexWeighted();
        }
    }
}