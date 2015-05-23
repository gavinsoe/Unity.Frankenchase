using UnityEngine;
using System.Collections;

public class Character2DController : MonoBehaviour {

    public static Character2DController instance;
    private Character2D character;
    private bool jump;
    private float move;

    private void Awake()
    {

        // set the static variable so that other classes can easily use this class
        instance = this;

        character = GetComponent<Character2D>();

        // default to run
        move = 1;
    }

    private void Update()
    {
        var count = Input.touchCount;
 
        if (!jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            jump = Input.GetButtonDown("Jump");

            // Touch input
            foreach (Touch touch in Input.touches)
            {
                //if (Debug.isDebugBuild) Debug.Log("[" + touch.fingerId + "] NextCommand : " + nextCommand.ToString());
                //if (Debug.isDebugBuild) Debug.Log("[" + touch.fingerId + "] Phase : " + touch.phase.ToString() + " | Time : " + (Time.time - touchStartTime[touch.fingerId]));
                // When a finger touches the screen...
                if (touch.phase == TouchPhase.Began)
                {
                    jump = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // Read the inputs.
        // bool crouch = Input.GetKey(KeyCode.LeftControl);
        // float h = CrossPlatformInputManager.GetAxis("Horizontal");

        // Pass all parameters to the character control script.
        character.Move(move, jump);
        jump = false;
    }

}
