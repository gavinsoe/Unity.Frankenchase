using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Frankenstein : MonoBehaviour
{
    public static Frankenstein instance;
    public bool facingRight = true; // For determining which way the player is currently facing.

    public float move = 1; // moving direction 1 = right, -1 = left;
    public bool jump = false;
    [SerializeField] private float defaultSpeed = 10f; // The default running speed
    public float currentSpeed; // Current speed character is running at
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.

    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    private Transform groundCheck; // A position marking where to check if the player is grounded.
    public float groundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded.

    private Animator anim; // Reference to the player's animator component.
    private Rigidbody2D body; // A link to the rigidbody objecty of the character
    private GameEvents trigger; // A link to the trigger that will initiate the holding phase
    
    // Things related to frankenstein's health
    public float maxHealth;
    private float curHealth;
    [SerializeField] 
    private GameObject damageTxtObject;

    // Pause variables
    private bool paused;
    private bool immune;
    private Vector2 pausedVelocity;
    private float pausedAngularVelocity;

    private void Awake()
    {
        // Setting up references
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        trigger = GetComponentInChildren<GameEvents>();

        // Set default speed
        currentSpeed = defaultSpeed;

        // set health
        curHealth = maxHealth;

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

        // Move the character
        Move();

        // Make sure frankenstein never goes behind the professor.
        if (Game.instance.currentState != GameState.HoldingPhase &&
            Character2D.instance.transform.position.x > transform.position.x)
        {
            // Only do this when frankenstein is not immune
            if (!trigger.triggered)
            {
                SetSpeed(Character2D.instance.defaultSpeed + 1, 2);
            }
        }
    }

    public void Move()
    {
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
        if (((grounded && anim.GetBool("Ground"))) && jump)
        {
            // reset velocity for double jump
            body.velocity = new Vector2(body.velocity.x, 0);

            // Add a vertical force to the player            
            body.AddForce(new Vector2(0f, jumpForce));

            jump = false;
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
        anim.enabled = false;
        body.isKinematic = true;
        body.Sleep();
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

    public void EnableInvincibility(float duration)
    {
        trigger.triggered = true;
        CancelInvoke("DisableInvincibility");
        Invoke("DisableInvincibility", duration);
    }

    public void DisableInvincibility()
    {
        trigger.triggered = false;
    }
    #endregion
    
    #region Damage

    public void TakeDamage(int damage)
    {
        var text = Instantiate(damageTxtObject);
        text.transform.parent = transform;
        text.transform.localPosition = new Vector3(0, 0, 0);

        text.GetComponentInChildren<Text>().text = damage.ToString();
        curHealth -= damage;

        if (curHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        // TODO: Ideally destroy and summon another monster
        // Reset health
        maxHealth += 100;
        curHealth = maxHealth;
        var nextPosition = transform.position;
        nextPosition.x += 10;

        transform.position = nextPosition;

    }

    #endregion
}
