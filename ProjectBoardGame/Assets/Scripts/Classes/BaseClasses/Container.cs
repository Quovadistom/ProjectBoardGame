using ExitGames.Client.Photon;
using Photon.Pun;
using ProjectTM.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTM.Containers
{
    public class Container : MonoBehaviour, ILockable
    {
        public List<Transform> LockPositions;
        public List<Transform> StorageLocations;
        protected List<GameObject> content = new List<GameObject>();
        private bool unlocked;

        private PhotonView photonView;

        private void OnEnable() => PhotonNetwork.AddCallbackTarget(this);

        private void Awake()
        {
            photonView = PhotonView.Get(this);
        }

        private void OnDisable() => PhotonNetwork.RemoveCallbackTarget(this);

        // ILockable
        public bool GetUnlocked() => unlocked;

        public virtual void Unlock()
        {
            unlocked = true;
            if (!ActivateContent())
                Debug.LogError("Content to activate not found.");
            
            photonView.RPC("UnlockRPC", RpcTarget.OthersBuffered);
        }

        public bool ActivateContent()
        {
            foreach (GameObject item in content)
            {
                if (item != null)
                    item.SetActive(true);
                else
                    return false;
            }
            return true;
        }

        public void AddPuzzle(GameObject newPuzzle)
        {
            newPuzzle.transform.position = LockPositions[0].position;
            newPuzzle.transform.rotation = LockPositions[0].rotation;
            newPuzzle.transform.parent = this.transform;
        }

        public void AddObject(GameObject newObject)
        {
            content.Add(newObject);
            newObject.SetActive(false);

            int i = Random.Range(0, StorageLocations.Count);
            newObject.transform.position = StorageLocations[i].position;
            StorageLocations.RemoveAt(i);
        }


        [PunRPC]
        public void UnlockRPC()
        {
            if (!unlocked)
                Unlock();
        }
    }
}

