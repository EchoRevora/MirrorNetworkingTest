using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkClient : NetworkBehaviour
{
    // Equivalent of NetClient in Buhei

    [SerializeField]
    private GameObject actualPlayerPrefab;
    [SerializeField]
    private GameObject networkPlayerPrefab;

    private Dictionary<Guid, Player> _localPlayers = new();

    // Basically on connection created
    void OnLocalPlayerJoined()
    {
        //NetworkServer.connections
        // Spawn Network Player prefab
    }

    // Basically on connection terminated
    void OnLocalPlayerLeft()
    {
        // Destroy Network Player
    }

    public bool _spawnedPlayer = false;


    private void Update()
    {
        if (isLocalPlayer)
        {
            // Join action from input system. For now hardcap to 1.
            if (!_spawnedPlayer && Input.GetKeyDown(KeyCode.Return))
            {
                _spawnedPlayer = true;

                Cmd_InstantiatePlayer();
            }
        }
    }

    [Command]
    private void Cmd_InstantiatePlayer()
    {
        Guid guid = Guid.NewGuid();
        GameObject go = Instantiate(actualPlayerPrefab);
        go.name = actualPlayerPrefab.name + "_" + guid;

        NetworkConnectionToClient ownerConnection = NetworkServer.connections.Values.First(x => x.connectionId == connectionToClient.connectionId);

        // lol this just throws and error.
        NetworkServer.Spawn(go, ownerConnection);

        //Player p = go.GetComponent<Player>();
        //p.OnMirrorInstantiate();

        //Rpc_InstantiatePlayer(new PlayerInfo(guid.ToString(), ownerConnectionId));
    }

    private struct PlayerInfo
    {
        public string guid;
        public int connectionId;

        public PlayerInfo(string guid, int ownerConnectionId)
        {
            this.guid = guid;
            this.connectionId = ownerConnectionId;
        }
    }
}
