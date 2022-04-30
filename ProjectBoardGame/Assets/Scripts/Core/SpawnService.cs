using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static ResourcesSettings;

public class SpawnService : GenericSingleton<SpawnService>
{
    private ResourcesSettings m_resourcesSettings;
    private bool m_creatingData = false;
    private Queue<SpawnObject> m_roomPlacementInfoQueue = new Queue<SpawnObject>();

    public SpawnService()
    {
        m_resourcesSettings = ScriptableObjectService.Instance.GetScriptableObject<ResourcesSettings>();
        //PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void QueueObjectsToSend(IEnumerable<SpawnObject> values)
    {
        foreach (var value in values)
        {
            m_roomPlacementInfoQueue.Enqueue(value);
        }
    }

    /*
    public void SpawnGameObjects(IEnumerable<SpawnObject> values)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            m_creatingData = false;
            QueueObjectsToSend(values);
            m_creatingData = true;

            foreach (var objectToSpawn in values)
            {
                GameObject gameObject = GetAsset(objectToSpawn);
                if (gameObject != null)
                    Instantiate(gameObject, objectToSpawn.Position, objectToSpawn.Rotation);
            }
        }
    }

    public const byte RoomPlacementInfoEventCode = 1;

    public async void SendRoomPlacementInfoEvent()
    {
        while (!m_creatingData)
            await Task.Delay(20);

        while (m_roomPlacementInfoQueue.Count != 0)
        {
            SendObject(m_roomPlacementInfoQueue.Dequeue());
            await Task.Delay(20);
        }
    }

    private void SendObject(SpawnObject sendObject)
    {
        string json = JsonUtility.ToJson(sendObject);
        object[] content = new object[] { json };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(RoomPlacementInfoEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == RoomPlacementInfoEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            string json = (string)data[0];
            SpawnObject sendObject = JsonUtility.FromJson<SpawnObject>(json);
            SpawnRecievedGameObject(sendObject);
        }
    }

    internal void SpawnRecievedGameObject(SpawnObject sendObject)
    {
        GameObject gameObject = GetAsset(sendObject);
        if (gameObject != null)
            Instantiate(gameObject, sendObject.Position, sendObject.Rotation);
    }

    private GameObject GetAsset(SpawnObject spawnObject)
    {
        Theme theme = m_resourcesSettings.GetTheme();

        WeightedItem<GameObject>[] objectList = m_resourcesSettings.GetCorrectObjectList(spawnObject, theme);

        if (objectList == null) { return null; }
        return objectList[spawnObject.Index].Item;
    }

    public void Dispose()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    */
}