using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneratedCollection
{
    public Dictionary<Vector3, Node> Nodes = new Dictionary<Vector3, Node>();
    public Dictionary<Vector3, FloorObject> FloorTiles = new Dictionary<Vector3, FloorObject>();
    public Dictionary<Vector3, WallObject> WallObjects = new Dictionary<Vector3, WallObject>();
    public Dictionary<Vector3, SpawnObject> CornerObjects = new Dictionary<Vector3, SpawnObject>();
    public Dictionary<Vector3, PuzzleSlotObject> PuzzleSlotObjects = new Dictionary<Vector3, PuzzleSlotObject>();
    public Action OnGeneratingDone { get; set; }
    public float RotationOffset { get; set; }
    public string CollectionName { get; set; }

    private int m_collectionNumber;
    private DebugSettings m_debugSettings;
    private ResourcesSettings m_resourcesSettings;
    private ResourcesSettings.DebugAssets m_debugObjects;

    private static int m_collections;


    public GeneratedCollection()
    {
        m_collectionNumber = m_collections;
        CollectionName = m_collectionNumber.ToString();
        m_collections++;

        GeneratorService.Instance.AddGeneratedCollection(this);
        m_resourcesSettings = ScriptableObjectService.Instance.GetScriptableObject<ResourcesSettings>();
        m_debugSettings = ScriptableObjectService.Instance.GetScriptableObject<DebugSettings>();

        m_debugObjects = m_resourcesSettings.GetDebugAssets();
    }

    public bool AddFloorTile(FloorObject tile) => FloorTiles.AddDictionaryItemWithPositionKey(tile);

    public bool AddPuzzleSlot(PuzzleSlotObject puzzleSlot) => PuzzleSlotObjects.AddDictionaryItemWithPositionKey(puzzleSlot);

    public bool AddCorner(SpawnObject spawnObject) => CornerObjects.AddDictionaryItemWithPositionKey(spawnObject);

    public WallObject AddWall(WallObject tile)
    {
        if (WallObjects.TryGetValue(tile.Position, out WallObject wall)) { return wall; }

        tile.Rotation = Quaternion.Euler(0, tile.Rotation.eulerAngles.y + RotationOffset, 0);
        WallObjects.Add(tile.Position, tile);
        return tile;
    }

    public void Init()
    {
        CalculateDoorPosition();
        if (m_debugSettings.ShowDebugAssets)
        {
            AssignDebugObjects(FloorTiles.Values);
            AssignDebugObjects(WallObjects.Values);
            GeneratorService.Instance.AddDebugObjects(FloorTiles.Values);
            GeneratorService.Instance.AddDebugObjects(WallObjects.Values);
        }
        if (m_debugSettings.ShowGameAssets)
        {
            CreateSpawnObjects(FloorTiles.Values);
            CreateSpawnObjects(WallObjects.Values);
            GeneratorService.Instance.AddSpawnObjects(CornerObjects.Values);
        }

        OnGeneratingDone.Invoke();
    }

    private void CalculateDoorPosition()
    {
        List<WallObject> outsideWallObjects = new List<WallObject>();
        outsideWallObjects = WallObjects.Values.Where(x => !x.Inside && x.RoomNumber == 1).ToList();

        List<WallObject> insideWallObjects = new List<WallObject>();
        insideWallObjects = WallObjects.Values.Where(x => x.Inside).ToList();

        // Outside door can only connect to room 1
        int outsideDoorLocation = UnityEngine.Random.Range(0, outsideWallObjects.Count);
        WallObject doorOutside = outsideWallObjects[outsideDoorLocation];
        doorOutside.RoomObjectType = RoomObjectTypes.DOOR;
        SetDoorTiles(doorOutside);

        int insideDoorLocation = UnityEngine.Random.Range(0, insideWallObjects.Count);
        WallObject doorInside = insideWallObjects[insideDoorLocation];
        doorInside.RoomObjectType = RoomObjectTypes.DOOR;
        SetDoorTiles(doorInside);
    }

    private void SetDoorTiles(WallObject door)
    {
        foreach (FloorObject floorObject in door.ConnectingFloorTiles)
        {
            floorObject.RoomObjectType = RoomObjectTypes.DOORTILE;
        }
    }

    private void CreateSpawnObjects(IEnumerable<RoomObject> roomObjects)
    {
        foreach (RoomObject roomObject in roomObjects)
        {
            ObjectType type = ObjectType.NONE;
            switch (roomObject.RoomObjectType)
            {
                case RoomObjectTypes.WALL:
                    type = roomObject.Inside ? ObjectType.WALLINSIDE : ObjectType.WALLOUTSIDE;
                    break;
                case RoomObjectTypes.DOOR:
                    type = roomObject.Inside ? ObjectType.DOORINSIDE : ObjectType.DOOROUTSIDE;
                    break;
                case RoomObjectTypes.CORNERTILE:
                    type = ObjectType.FLOORINSIDE;
                    break;
                case RoomObjectTypes.WALLTILE:
                    type = ObjectType.FLOORINSIDE;
                    break;
                case RoomObjectTypes.MIDDLETILE:
                    type = ObjectType.FLOORINSIDE;
                    break;
                case RoomObjectTypes.DOORTILE:
                    type = ObjectType.FLOORINSIDE;
                    break;
                case RoomObjectTypes.EMPTY:
                    type = ObjectType.FLOOROUTSIDE;
                    break;
            }

            SpawnObject spawnObject = new SpawnObject(type, roomObject.Position, roomObject.Scale, roomObject.Rotation);
            GeneratorService.Instance.AddSpawnObject(spawnObject);
        }
    }

    private void AssignDebugObjects(IEnumerable<RoomObject> roomObjects)
    {
        foreach (RoomObject roomObject in roomObjects)
        {
            GameObject asset = null;
            switch (roomObject.RoomObjectType)
            {
                case RoomObjectTypes.WALL:
                    asset = roomObject.Inside ? m_debugObjects.InsideWall : m_debugObjects.OutsideWall;
                    break;
                case RoomObjectTypes.DOOR:
                    asset = roomObject.Inside ? m_debugObjects.InsideDoor : m_debugObjects.OutsideDoor;
                    break;
                case RoomObjectTypes.CORNERTILE:
                    asset = m_debugObjects.CornerTile;
                    break;
                case RoomObjectTypes.WALLTILE:
                    asset = m_debugObjects.WallTile;
                    break;
                case RoomObjectTypes.MIDDLETILE:
                    asset = m_debugObjects.MiddleTile;
                    break;
                case RoomObjectTypes.DOORTILE:
                    asset = m_debugObjects.DoorTile;
                    break;
                case RoomObjectTypes.EMPTY:
                    asset = m_debugObjects.GrassTile;
                    break;
                case RoomObjectTypes.PUZZLESLOT:
                    asset = m_debugObjects.PuzzleSlot;
                    break;
            }

            roomObject.Asset = asset;
        }
    }
}
