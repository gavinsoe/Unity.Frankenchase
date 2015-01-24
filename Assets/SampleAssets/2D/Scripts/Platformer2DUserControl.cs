using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        public static Platformer2DUserControl instance;
        private PlatformerCharacter2D character;
        private bool jump;
        private float move;

        private void Awake()
        {

            // set the static variable so that other classes can easily use this class
            instance = this;

            character = GetComponent<PlatformerCharacter2D>();

            // default to run
            move = 1;
        }

        private void Update()
        {
            if(!jump)
            // Read the jump input in Update so button presses aren't missed.
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            // bool crouch = Input.GetKey(KeyCode.LeftControl);
            // float h = CrossPlatformInputManager.GetAxis("Horizontal");
			
            // Pass all parameters to the character control script.
            character.Move(move, false, jump);
            jump = false;
        }

        public void SetSpeed(float speed)
        {
            // Currently either 1 or 0
            move = speed;
        }
    }
}