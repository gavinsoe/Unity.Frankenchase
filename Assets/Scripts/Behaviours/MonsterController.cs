using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterController : MonoBehaviour {

    public static MonsterController instance;
    
    private Animator anim; // Reference to the monster's animator component
    private Rigidbody2D body; // Reference to the rigidbody object of the monster
    private GameEvents trigger; // Reference to the game event trigger attached to the monster

    [SerializeField] public Monster[] monsters; // List of monsters
    private Monster selectedMonster; // Active monster in current playthrough

    #region Movement

    [SerializeField]
    private float defaultSpeed = 10f; // The default running speed
    public float currentSpeed; // Current speed character is running at.
    public float speedBoost; // Amount of extra speed when boosted (when monster goes behind the professor)
    public float speedBoostDuration; // Duration of the speed buff

    #endregion
    #region HP/Damage related

    [SerializeField] private GameObject scopeObject; // Reference to the scope animation
    [SerializeField]
    private float maxHealth; // Maximum health of the monster
    public float curHealth; // Current health of the monster
    [SerializeField]
    private GameObject damageTxtObject; // Reference to the damage text animation

    #endregion
    #region Pause

    private bool paused; // Whether or not the game is paused, which will in turn 'pause' the character
    private Vector2 pausedVelocity;
    private float pausedAngularVelocity;

    #endregion
    #region Ground Check

    [SerializeField]
    private LayerMask whatIsGround; // A mask determining what is ground to the character
    private Transform groundCheck; // A position marking where to check if the player is grounded
    public float groundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded

    #endregion
    #region propelling force

    public float xDir = 10f;
    public float yDir = 10f;
    public ForceMode2D forceMode = ForceMode2D.Force;

    #endregion
    private void Awake()
    {
        // Setting up references
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        trigger = GetComponentInChildren<GameEvents>();

        // Set default speed of monster
        currentSpeed = defaultSpeed;

        // Set health
        curHealth = maxHealth;

        // Allow other classes to access this class
        instance = this;
    }

    private void Start()
    {
        // Randomly select one monster to activate
        var rand = Random.Range(0, monsters.Length);
        selectedMonster = monsters[rand];
        anim.runtimeAnimatorController = selectedMonster.anim;
        Game.instance.currentEnvironment = selectedMonster.environment;

    }

    private void FixedUpdate()
    {
        #region Check for ground
        
        // Monster is grounded if a circlecast to the groundcheck position hits 
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

        #endregion
        #region Movement

        // Set the vertical animation
        anim.SetFloat("vSpeed", body.velocity.y);

        // Move the character
        Move();

        // Make sure the monster never goes behind the professor
        if (Character2D.instance.transform.position.x > transform.position.x)
        {
            // Only do this when frankenstein is not immune
            if (!trigger.triggered)
            {
                SetSpeed(Character2D.instance.currentSpeed + speedBoost, speedBoostDuration);
            }
        }
        #endregion
    }
    
    private void Move()
    {
        // Do not move if paused
        if (paused) return;

        // Only alter velocity while grounded
        if (grounded)
        {
            // Move the monster
            body.velocity = new Vector2(currentSpeed, body.velocity.y);
        }
    }

    // Kill the character
    public void Kill()
    {
        paused = true;
        anim.enabled = false;
        body.isKinematic = true;
        body.Sleep();
    }

    #region Game States

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

    #endregion
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
    #region Endurance / Health
    public void TakeDamageDelay(float delay)
    {
        Invoke("TakeDamage", delay);
    }

    public void TakeDamage()
    {
        var damage = Character2D.instance.equippedWeapon.damage;

        CameraRunner.instance.Shake(0.1f);
        var text = Instantiate(damageTxtObject);
        text.transform.SetParent(transform, false);

        text.GetComponentInChildren<Text>().text = damage.ToString();
        curHealth -= damage;

        if (curHealth <= 0)
        {
            // Kill it somehow
            //NavigationManager.instance.GameOver();
        }

        // Slightly push/stagger monster away when attacked
        body.AddForce(new Vector2(xDir, yDir), forceMode);
        SetSpeed(Character2D.instance.currentSpeed + speedBoost, 0.5f);
    }

    public void Targeted()
    {
        var scope = Instantiate(scopeObject);
        scope.transform.SetParent(transform, false);
    }
    #endregion
}
