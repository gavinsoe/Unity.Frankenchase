using UnityEngine;

[RequireComponent(typeof(Character2D))]
public class Chaser : MonoBehaviour
    {
        public static Chaser instance;
        public Character2D character;

        private float move;

        private void Awake()
        {
            // set the static variable so that other classes can easily use this class
            instance = this;

            character = GetComponent<Character2D>();

            // default to run
            move = 1;
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            // bool crouch = Input.GetKey(KeyCode.LeftControl);
            // float h = CrossPlatformInputManager.GetAxis("Horizontal");

            // Pass all parameters to the character control script.
            character.Move(move, false, false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // If touches player, game over
            if (other.tag == "Player")
            {
                // Trigger tap to escape mode.
                HoldingPhase.instance.TriggerEvent();
                //Debug.Break();
                //Destroy(gameObject);
                
                return;
            }
        }

        public void SetSpeed(float speed)
        {
            // Currently either 1 or 0
            move = speed;
        }
    }