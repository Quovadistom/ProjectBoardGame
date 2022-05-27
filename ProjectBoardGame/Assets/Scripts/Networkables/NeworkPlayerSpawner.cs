using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace ProjectTM.Networkables
{
    public class NeworkPlayerSpawner : MonoBehaviour
    {
        private GameObject spawnedPlayer;

        public HandPresence LeftHandPresence, RightHandPresence;

        /*
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            spawnedPlayer = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
            spawnedPlayer.GetComponent<NetworkPlayer>().Init(LeftHandPresence, RightHandPresence);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonNetwork.Destroy(spawnedPlayer);
        }
        */
    }
}
