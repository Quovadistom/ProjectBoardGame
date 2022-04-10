using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace ProjectTM.Managers
{
    public class NetworkManager : GenericPunSingleton<NetworkManager>, IPunOwnershipCallbacks
    {
        public event Action RoomCreatedAndReady;

        public override void Awake()
        {
            base.Awake();

            AppManager.Instance.AppInitializationDone += OnAppInitializationDone;
        }

        private void OnAppInitializationDone()
        {
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Trying to connect to server...");

        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("Connected to masterserver!");
            RoomOptions roomOptions = new RoomOptions()
            {
                MaxPlayers = 2,
                IsVisible = true,
                IsOpen = true,
                PublishUserId = true
            };

            bool connected = PhotonNetwork.JoinOrCreateRoom("Test Toom", roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Joined Room! Starting randomization...");

            RoomCreatedAndReady?.Invoke();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log($"New player joined: {newPlayer.UserId}");
            SpawnService.Instance.SendRoomPlacementInfoEvent();
        }

        public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
        {
            if (targetView.IsMine)
                targetView.TransferOwnership(requestingPlayer);
        }

        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner) { }

        public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest) { }
    }
}
