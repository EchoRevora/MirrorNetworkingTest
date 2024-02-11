using Mirror;
using UnityEngine;

namespace EchoNetworkSpace
{
    public class Player : NetworkBehaviour
    {
        private bool ownedAndReady;

        void Update()
        {
            if (ownedAndReady)
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
            Debug.Log("Player isOwned: " + isOwned);

            // This is called on client (who is NOT server), even though authority is false. Idk.
            base.OnStartAuthority();

            ownedAndReady = true;
        }
    }
}