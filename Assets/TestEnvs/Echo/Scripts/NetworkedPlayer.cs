using Mirror;
using System;
using System.Linq;
using UnityEngine;

namespace EchoNetworkSpace
{
    [Serializable]
    public class NetworkedPlayer : NetworkBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;

        public Guid Uid { get; private set; }

        // For now, just spawn a player object
        public override void OnStartAuthority()
        {
            Debug.Log("NetworkedPlayer isOwned: " + isOwned);

            CmdSpawnPlayerObject();
        }

        [Command]
        private void CmdSpawnPlayerObject()
        {
            GameObject go = Instantiate(playerPrefab);

            NetworkConnectionToClient ownerConnection = NetworkServer.connections.Values.First(x => x.connectionId == netIdentity.connectionToClient.connectionId);
            go.name = playerPrefab.name + "[owner=" + ownerConnection.connectionId + "]";

            NetworkServer.Spawn(go, ownerConnection);
        }


        // Must buffer this somehow..
        [ClientRpc]
        public void RpcOnMirrorInstantiate(string uid)
        {
            this.Uid = Guid.Parse(uid);

            EchoNetworkManager.instance.RegisterNetworkedPlayer(this);
        }
    }
}
