using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Character2D : MonoBehaviour
{
    public static Character2D instance;
    public bool facingRight = true; // For determining which way the player is currently facing.

    [SerializeField] private float defaultSpeed = 10f; // The default running speed
    public float currentSpeed; // Current speed character is running at
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [SerializeField] private float doubleJumpForce = 300f;

    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    private Transform groundCheck; // A position marking where to check if the player is grounded.
    public float groundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded.

    private Animator anim; // Reference to the player's animator component.

    private bool doubleJump = false; // Whether or not the double jump has been used
    private Rigidbody2D body; // A link to the rigidbody objecty of the character

    #region Death Orb related variables

    [SerializeField]
    private GameObject deathOrb;
    private Animator animOrb; // Reference to tha animation object that animates the orbs
    [SerializeField]
    private GameObject deathTimer; // A reference to the canvas holding the death timer
    private Text deathTimerText; // A direct reference to the text object of the GUI
    public float debuffDeathTimer = 10f; // Timer before character dies after 3 stacks of debuff
    public float debuffResetTimer = 10f; // Timer before the debuff dissapears
    public int debuffStacks = 0; // Stores the number of debuff stacks on the character
    private bool doomed = false;
    private float timeToDeath;

    #endregion
    #region Slow debuff related variable

    [SerializeField]
    private GameObject debuffSlow;
    private Animator animSlow; // Reference to the animator object to animate the slow component
    [SerializeField]
    private GameObject profSprite; // The gameobject containing the professor sprite
    private SpriteRenderer profSpriteRenderer; // Reference to the sprite component of the professor

    #endregion
    #region Pause related variables

    // Pause variables
    private bool paused; // default to paused
    private Vector2 pausedVelocity;
    private float pausedAngularVelocity;

    #endregion

    private void Awake()
    {
        // Setting up references
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        animOrb = deathOrb.GetComponent<Animator>();
        animSlow = debuffSlow.GetComponent<Animator>();
        profSpriteRenderer = profSprite.GetComponent<SpriteRenderer>();

        deathTimerText = deathTimer.GetComponent<Text>();

        // Set default speed
        currentSpeed = defaultSpeed;
        
        // Allow other classes to access this class
        instance = this;
    }

    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits 
        // anything designated as ground, and if y velocity is positive
        if (!Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround))
        {
            grounded = false;
        }
        else if (body.velocity.y <= 0)
        {
            grounded = true;
        }
        anim.SetBool("Ground", grounded);

        // Set the vertical animation
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        if (grounded)
        {
            doubleJump = false;
        }

        // Countdown death timer (when applicable)
        if (doomed && !paused)
        {
            timeToDeath -= Time.fixedDeltaTime;
            deathTimerText.text = timeToDeath.ToString("#");
            if (timeToDeath <= 0)
            {
                doomed = false;
                NavigationManager.instance.GameOver();
            }
        }
    }

    public void Move(float move, bool jump)
    {
        // Do not proceed if in a paused state
        if (paused) return;
       
        // Only control player if grounded or airControl is turned on
        if (grounded)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            body.velocity = new Vector2(move * currentSpeed, body.velocity.y);

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
            body.velocity = new Vector2(body.velocity.x, 0);

            // Disable double jump (if character just double jumped)
            if (!grounded) doubleJump = true;

            // Add a vertical force to the player
            if (doubleJump)
            {
                body.AddForce(new Vector2(0f, doubleJumpForce));
            }
            else
            {
                body.AddForce(new Vector2(0f, jumpForce));
            }
            
            grounded = false;
            anim.SetBool("Ground", false);
        }

    }

    public void Pause()
    {
        paused = true;
        anim.enabled = false;
        pausedVelocity = body.velocity;
        pausedAngularVelocity = body.angularVelocity;
        body.isKinematic = true;
        body.Sleep();
    }

    public void Resume()
    {
        paused = false;
        body.isKinematic = false;
        body.AddForce(pausedVelocity, ForceMode2D.Impulse);
        body.AddTorque(pausedAngularVelocity, ForceMode2D.Force);
        anim.enabled = true;
        body.WakeUp();
    }

    public void Reset()
    {
        paused = false;
        body.isKinematic = false;
        body.velocity = Vector3.zero;
        body.angularVelocity = 0;
        anim.enabled = true;
        body.WakeUp();
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
        paused = true;
        body.isKinematic = true;
        body.Sleep();

        // Animate death
        anim.SetBool("Ground", true);
        anim.SetTrigger("Death");
    }

    #region Debuff

    public void AddDebuff()
    {
        // Do not do anything if already at 3 stacks
        if (debuffStacks == 3) return;

        // Update the number of debuff stacks
        debuffStacks++;

        // Update the animation
        animOrb.SetInteger("DebuffStacks", debuffStacks);

        if (debuffStacks == 3)
        {
            doomed = true;
            CancelInvoke("ClearDebuff");
            timeToDeath = debuffDeathTimer;
        }
        else
        {
            CancelInvoke("ClearDebuff");
            Invoke("ClearDebuff", debuffResetTimer);
        }
    }

    private void ClearDebuff()
    {
        debuffStacks = 0;
        animOrb.SetInteger("DebuffStacks", debuffStacks);
    }

    #endregion
    #region Speed adjustment

    public void SetSpeed(float velocity, float duration)
    {
        currentSpeed = velocity;
        CancelInvoke("ResetSpeed");
        Invoke("ResetSpeed", duration);

        // Update animation
        if (currentSpeed < defaultSpeed)
        {
            ToggleSlowDebuff(true);
        }
        else
        {
            ToggleSlowDebuff(false);
        }
    }

    private void ResetSpeed()
    {
        currentSpeed = defaultSpeed;
        ToggleSlowDebuff(false);
    }

    private void ToggleSlowDebuff(bool active)
    {
        if (active)
        {
            animSlow.SetBool("Slowed", true);
            profSpriteRenderer.material.SetColor("_Color",new Color(0.5f, 0.74f, 0.47f, 1f));
        }
        else
        {
            animSlow.SetBool("Slowed", false);
            profSpriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        }            
        
    }
    #endregion

}
