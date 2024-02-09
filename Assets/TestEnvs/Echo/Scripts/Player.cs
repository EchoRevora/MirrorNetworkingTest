using EchoNetworkSpace;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private bool authorityAndReady;

    // Update is called once per frame
    void Update()
    {
        if (authorityAndReady)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal") * .1f;
        float vertical = Input.GetAxis("Vertical") * .1f;
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.position += movement;
    }


    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        authorityAndReady = true;
    }

    private void Start()
    {
        NetworkIdentity netIdentity = GetComponent<NetworkIdentity>();

        var spawnedNetworkedObjects = NetworkServer.spawned;

        //
        foreach (var obj in spawnedNetworkedObjects)
        {
            if (netIdentity.netId == obj.Key)
            {
                // Then we know that this is the player object that was spawned.

                
            }
        }
    }
}
