using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float horizontal = Input.GetAxis("Horizontal") * .1f;
            float vertical = Input.GetAxis("Vertical") * .1f;
            Vector3 movement = new Vector3(horizontal, 0, vertical);
            transform.position += movement;
        }
    }
}
