using UnityEngine;
using System.Collections;

public class Character2D : MonoBehaviour
{
    public static Character2D instance;
    private bool facingRight = true; // For determining which way the player is currently facing.

    [SerializeField] private float defaultSpeed = 10f; // The default running speed
    private float currentSpeed; // Current speed character is running at
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [Range(0, 1)][SerializeField] private float crouchSpeed = 0.36f; // Amount of maxspeed applied to crouching movement. 1 = 100%

    [SerializeField] private bool airControl = false; // Whether or not a player can steer while jumping
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character

    private Transform groundCheck; // A position marking where to check if the player is grounded.
    public float groundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded.

    private Transform ceilingCheck; // A Position marking where to check for ceilings
    public float ceilingRadius = 0.01f; // Radius of the overlap circle to determine if the player can stand up
    
    private Animator anim; // Reference to the player's animator component.

    private bool doubleJump = false; // Whether or not the double jump has been used

    private void Awake()
    {
        // Setting up references
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();

        // Set default speed
        currentSpeed = defaultSpeed;

        // Allow other classes to access this class
        instance = this;
    }

    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        // Set the vertical animation
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        if (grounded)
        {
            doubleJump = false;
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch && anim.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        anim.SetBool("Crouch", crouch);

        // Only control player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            move = (crouch ? move * crouchSpeed : move);

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            rigidbody2D.velocity = new Vector2(move * currentSpeed, rigidbody2D.velocity.y);

            // If the input is moving the palyer right and the player is facing left
            if (move > 0 && !facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
            {
                // ...flip the player
                Flip();
            }
        }
        
        // If the player should jump...
        if (((grounded && anim.GetBool("Ground")) || !doubleJump) && jump)
        {
            // reset velocity for double jump
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);

            // Add a vertical force to the player
            rigidbody.AddForce(new Vector2(0f, jumpForce));
            grounded = false;
            anim.SetBool("Ground", false);

            // Disable double jump (if character just double jumped)
            if (!grounded) doubleJump = true;

        }

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Kill the character
    public void Kill()
    {
        // Animation?
        // Fall off?
    }

    #region Speed adjustment
    public void SetSpeed(float velocity, float duration)
    {
        currentSpeed = velocity;
        CancelInvoke("ResetSpeed");
        Invoke("ResetSpeed", duration);
    }
    private void ResetSpeed()
    {
        currentSpeed = defaultSpeed;
    }
    #endregion

}
