using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace ProjectTM.Networkables
{
    public class NeworkPlayerSpawner : MonoBehaviourPunCallbacks
    {
        private GameObject spawnedPlayer;

        public HandPresence Left, Right;

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            spawnedPlayer = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
            spawnedPlayer.GetComponent<NetworkPlayer>().Init(Left, Right);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            PhotonNetwork.Destroy(spawnedPlayer);
        }

    }
}
