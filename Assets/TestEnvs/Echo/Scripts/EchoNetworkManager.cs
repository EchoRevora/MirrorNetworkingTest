using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EchoNetworkSpace
{
    public struct CreatePlayerObject : NetworkMessage { }

    //public static class NetworkGlobals
    //{
    //    public static Dictionary<int, LocalPlayerManager> clients;
    //}

    public class EchoNetworkManager : NetworkManager
    {

        //public Dictionary<int, LocalPlayerManager> clients;

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
            //Transform startPos = GetStartPosition();
            //GameObject player = startPos != null
            //    ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            //    : Instantiate(playerPrefab);

            GameObject player = Instantiate(playerPrefab);

            // instantiating a "Player" prefab gives it the name "Player(clone)"
            // => appending the connectionId is WAY more useful for debugging!
            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, player);

            //NetworkGlobals.clients.Add(conn.connectionId, player.GetComponent<LocalPlayerManager>());
        }


        public override void OnClientConnect()
        {
            base.OnClientConnect();
        }
    }
}

