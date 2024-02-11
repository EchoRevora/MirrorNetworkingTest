using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EchoNetworkSpace
{
    // NOTES for later:
    // connectionId local seems to always be 0
    // To check whether an object is owned by a local player, use isOwned. Not authority



    public class EchoNetworkManager : NetworkManager
    {
        public Dictionary<Guid, NetworkedPlayer> networkedPlayers {get; private set; } = new();

        public static EchoNetworkManager instance => (EchoNetworkManager)singleton;

        public void RegisterNetworkedPlayer(NetworkedPlayer player)
        {
            if(networkedPlayers.ContainsKey(player.Uid))
            {
                Debug.LogError("ALARM!! Networked Player is already in list.");
            }

            networkedPlayers.Add(player.Uid, player);

            Debug.Log($"Player with id {player.Uid} ADDED to list.");
        }

        public void UnregisterPlayer(NetworkedPlayer player)
        {
            if (!networkedPlayers.ContainsKey(player.Uid))
            {
                Debug.LogError("ALARM!! Disconnecting Networked Player is not in the list.");
            }

            Debug.Log($"Player with id {player.Uid} REMOVED from list.");

            networkedPlayers.Remove(player.Uid);
        }


        public override void OnStartServer()
        {
            base.OnStartServer();

            //NetworkServer.RegisterHandler<CreatePlayerObject>(OnCreatePlayer);
        }


        //private void OnCreatePlayer(NetworkConnectionToClient conn, CreatePlayerObject character)
        //{
        //    //Manager but you can use different prefabs per race for example
        //    GameObject gameobject = Instantiate(playerPrefab);

        //    //call this to use this gameobject as the primary controller

        //    NetworkServer.AddPlayerForConnection(conn, gameobject);
        //}

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            GameObject player = Instantiate(playerPrefab);

            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, player);
        }


        public override void OnClientConnect()
        {
            base.OnClientConnect();
        }
    }
}

