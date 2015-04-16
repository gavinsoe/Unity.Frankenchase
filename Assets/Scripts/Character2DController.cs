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
        if (!jump)
            // Read the jump input in Update so button presses aren't missed.
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
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
