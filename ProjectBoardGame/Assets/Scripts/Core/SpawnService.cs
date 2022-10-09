using System.Collections.Generic;
using UnityEngine;

public struct SpawnObject
{
    public SpawnObject(Vector3 position, Quaternion rotation, Vector3 scale, string objectType, int index)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
        ObjectType = objectType;
        Index = index;
    }

    public Vector3 Position;
    public Vector3 Scale;
    public Quaternion Rotation;
    public string ObjectType;
    public int Index;
}

public struct SpawnCollection
{
    public SpawnCollection(Transform parentTransform)
    {
        ParentTransform = parentTransform;
        SpawnObjects = new List<SpawnObject>();
    }

    public Transform ParentTransform;
    public List<SpawnObject> SpawnObjects;
}

public class SpawnService : GenericSingleton<SpawnService>
{
    private Dictionary<string, SpawnCollection> m_groupedSpawnCollections = new Dictionary<string, SpawnCollection>();
    private TileAssets m_tileAssets;

    public override void Awake()
    {
        base.Awake();

        //GenerationService.Instance.GenerationFinished += OnGenerationFinished;
    }

    public void Start()
    {
        m_tileAssets = ScriptableObjectService.Instance.GetScriptableObject<TileAssets>();
        OnGenerationFinished();
    }

    public SpawnCollection RegisterNewSpawnCollection(string spawnCollectionName, Transform spawnCollectionParent)
    {
        SpawnCollection spawnCollection = new SpawnCollection(spawnCollectionParent);
        m_groupedSpawnCollections.Add(spawnCollectionName, spawnCollection);

        return spawnCollection;
    }

    private void OnGenerationFinished()
    {
        foreach (KeyValuePair<string, SpawnCollection> item in m_groupedSpawnCollections)
        {
            foreach (SpawnObject spawnObject in item.Value.SpawnObjects)
            {
                if (m_tileAssets.TryGetStaticObjectList(spawnObject.ObjectType, out WeightedItem<TileMetaData>[] list))
                {
                    GameObject gameObject = GameObject.Instantiate(list[spawnObject.Index].Item.gameObject);
                    gameObject.transform.position = spawnObject.Position;
                    gameObject.transform.rotation = spawnObject.Rotation;
                    gameObject.transform.localScale = spawnObject.Scale;
                }
            }
        }
    }
}