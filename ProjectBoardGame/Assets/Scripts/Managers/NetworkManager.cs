using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NetworkPlayer = ProjectTM.Networkables.NetworkPlayer;

namespace ProjectTM.Managers
{
    public class NetworkManager : GenericSingleton<NetworkManager>, INetworkRunnerCallbacks
    {
        public HandPresence LeftHandPresence, RightHandPresence;

        public event Action RoomCreatedAndReady;
        private NetworkRunner _runner;

        [SerializeField] private NetworkPlayer m_playerPrefab;

        public override void Awake()
        {
            base.Awake();

            AppManager.Instance.AppInitializationDone += OnAppInitializationDone;
        }

        private void OnAppInitializationDone()
        {
            StartGame(GameMode.Shared);
        }


        async void StartGame(GameMode mode)
        {
            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            // Start or join (depends on gamemode) a session with a specific name
            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = "TestRoom",
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneObjectProvider = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"New player joined: {player.PlayerId}");
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log($"Player Left: {player.PlayerId}");
        }

        public void OnInput(NetworkRunner runner, NetworkInput input) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            Debug.Log("Joined Room! Starting randomization...");
            RoomCreatedAndReady?.Invoke();

            NetworkPlayer spawnedPlayer = runner.Spawn(m_playerPrefab);
            spawnedPlayer.Init(LeftHandPresence, RightHandPresence);
        }

        public void OnDisconnectedFromServer(NetworkRunner runner) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }

        /*
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
        */
    }
}
