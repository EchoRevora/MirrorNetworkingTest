using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EchoNetworkSpace
{
    //public struct CreatePlayerObject : NetworkMessage { }

    public class EchoNetworkManager : NetworkManager
    {
        public Dictionary<Guid, NetworkedPlayer> networkedPlayers {get; private set; } = new();

        public static EchoNetworkManager instance => (EchoNetworkManager)singleton;

        public bool RegisterNetworkedPlayer(NetworkedPlayer player)
        {
            if(networkedPlayers.ContainsKey(player.Uid))
            {
                Debug.LogError("ALARM!! Player is already in list.");
                return false;
            }

            networkedPlayers.Add(player.Uid, player);
            Debug.Log("Player List:");
            foreach (var networkPlayer in networkedPlayers)
            {
                Debug.Log(networkPlayer.Key);
            }

            return true;
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

