using Mirror;
using System;
using System.Linq;
using UnityEngine;

namespace EchoNetworkSpace
{
    public class NetworkClient : NetworkBehaviour
    {
        [SerializeField]
        private GameObject networkPlayerPrefab;

        private bool _spawnedPlayer = false;


        private void Update()
        {
            if (isLocalPlayer)
            {
                // Join action from input system. For now hardcap to 1.
                if (!_spawnedPlayer && Input.GetKeyDown(KeyCode.Return))
                {
                    _spawnedPlayer = true;

                    Cmd_CreateNetworkedPlayer();
                }
            }
        }


        [Command]
        private void Cmd_CreateNetworkedPlayer()
        {
            Guid guid;
            do
            {
                guid = Guid.NewGuid();
            }
            while (EchoNetworkManager.instance.networkedPlayers.ContainsKey(guid));

            GameObject go = Instantiate(networkPlayerPrefab);
            go.name = networkPlayerPrefab.name + "_" + guid;

            NetworkConnectionToClient ownerConnection = NetworkServer.connections.Values.First(x => x.connectionId == connectionToClient.connectionId);


            NetworkServer.Spawn(go, ownerConnection);

            NetworkedPlayer p = go.GetComponent<NetworkedPlayer>();

            p.RpcOnMirrorInstantiate(guid.ToString());
        }
    }
}