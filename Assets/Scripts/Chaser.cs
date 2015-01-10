using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Chaser : MonoBehaviour
    {
        private PlatformerCharacter2D character;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            // bool crouch = Input.GetKey(KeyCode.LeftControl);
            // float h = CrossPlatformInputManager.GetAxis("Horizontal");

            // Pass all parameters to the character control script.
            character.Move(1, false, false);

            
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // If touches player, game over
            if (other.tag == "Player")
            {
                Debug.Break();
                return;
            }
        }
    }
}