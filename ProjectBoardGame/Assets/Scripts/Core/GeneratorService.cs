using System;
using System.Collections.Generic;
using UnityEngine;
using static ResourcesSettings;

public class GeneratorService : GenericSingleton<GeneratorService>
{
    public Dictionary<Vector3, SpawnObject> SpawnObjects = new Dictionary<Vector3, SpawnObject>();
    private int m_finishedGenerators = 0;
    private ResourcesSettings m_resourcesSettings;
    private DebugSettings m_debugSettings;

    public GeneratorService()
    {
        m_resourcesSettings = ScriptableObjectService.Instance.GetScriptableObject<ResourcesSettings>();
        m_debugSettings = ScriptableObjectService.Instance.GetScriptableObject<DebugSettings>();
    }
    public void AddSpawnObjects(IEnumerable<SpawnObject> spawnObjects)
    {
        foreach (SpawnObject spawnObject in spawnObjects)
        {
            AddSpawnObject(spawnObject);
        }
    }

    public void AddSpawnObject(SpawnObject spawnObject)
    {
        if (SpawnObjects.ContainsKey(spawnObject.Position)) { return; }

        SpawnObjects.Add(spawnObject.Position, spawnObject);
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