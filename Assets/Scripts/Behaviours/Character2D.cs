using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Soomla.Store;

public class Character2D : MonoBehaviour
{
    public static Character2D instance;
    public bool facingRight = true; // For determining which way the player is currently facing.

    [SerializeField] public float defaultSpeed = 10f; // The default running speed
    [SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.
    [SerializeField] private float doubleJumpForce = 300f;

    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
    private Transform groundCheck; // A position marking where to check if the player is grounded.
    public float groundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded.

    private Animator anim; // Reference to the player's animator component.

    private float move; // 1 = move, 0 = stop
    private bool doubleJump = false; // Whether or not the double jump has been used
    private Rigidbody2D body; // A link to the rigidbody objecty of the character

    #region Weapons

    public Weapon equippedWeapon;
    private bool weaponOnCooldown;
    public GameObject arrow;
    public float arrowSpawnXOffset = 0.2419f;
    public float arrowSpawnYOffset = -0.177f;

    #endregion

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
        move = 1;
        defaultSpeed = Boots.speed;

        // Initialise equipped weapon
        equippedWeapon = Game.instance.GetEquippedWeapon();
        defaultSpeed += equippedWeapon.speedBoost;

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

    public void Move(bool jump)
    {
        // Do not proceed if in a paused state
        if (paused) return;
       
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(move));

        // Move the character
        body.velocity = new Vector2(move * defaultSpeed, body.velocity.y);

        // If the player should jump...
        if (((grounded && anim.GetBool("Ground")) || !doubleJump) && jump  && move != 0)
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

    #region Attack related
    public void Attack(GameObject target)
    {
        // Attacks everytime something is in range (no cooldown time)
        if (equippedWeapon.type == WeaponType.Sword && !weaponOnCooldown)
        {
            if (target.tag == "Monster")
            {
                anim.SetTrigger("Attack");
                MonsterController.instance.TakeDamageDelay(0.25f);
                weaponOnCooldown = true;
                Invoke("ResetCooldown", equippedWeapon.cooldown);
            }
        }
        // Attacks everytime something is in range (has cooldown)
        else if (equippedWeapon.type == WeaponType.Whip && !weaponOnCooldown)
        {
           if (target.tag == "Monster")
           {
               anim.SetTrigger("Attack");
               MonsterController.instance.TakeDamageDelay(0.25f);
               weaponOnCooldown = true;
               Invoke("ResetCooldown", equippedWeapon.cooldown);
           }
        }
        // Has aim time.
        else if (equippedWeapon.type == WeaponType.Crossbow && !weaponOnCooldown)
        {
            anim.SetTrigger("Aim");
            weaponOnCooldown = true;
            MonsterController.instance.Targeted(equippedWeapon.cooldown);
            Invoke("ResetCooldown", equippedWeapon.cooldown);
        }
    }

    public void ShootProjectile()
    {
        anim.SetTrigger("Shoot");
        Instantiate(arrow,
                    new Vector3(transform.position.x + arrowSpawnXOffset,
                                transform.position.y + arrowSpawnYOffset,
                                transform.position.z),
                    transform.rotation);
    }

    public void ResetCooldown()
    {
        weaponOnCooldown = false;
    }

    #endregion

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
    #region Slow

    public void Slow(float percentage, float duration)
    {
        move = percentage;
        animSlow.SetBool("Slowed", true);
        profSpriteRenderer.material.SetColor("_Color", new Color(0.5f, 0.74f, 0.47f, 1f));

        CancelInvoke("RemoveSlow");
        Invoke("RemoveSlow", duration);
    }

    private void RemoveSlow()
    {
        move = 1;
        animSlow.SetBool("Slowed", false);
        profSpriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
    }

    #endregion
    #region Stun Effect

    public void Stun(float duration)
    {
        // Pause current animation
        anim.SetBool("Stunned", true);
        move = 0;

        CancelInvoke("RemoveStun");
        Invoke("RemoveStun", duration);
    }

    private void RemoveStun()
    {
        // Resume character movement and animation
        anim.SetBool("Stunned", false);
        move = 1;
    }

    #endregion
}

