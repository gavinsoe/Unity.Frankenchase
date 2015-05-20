using UnityEngine;
using System.Collections;

public class Ram : MonoBehaviour 
{
    public static Ram instance;

    [SerializeField] private float defaultSpeed = 10f; // The default speed of the ram
    public float currentSpeed; // Current speed of the ram

    private Animator anim; // Reference to the animator component of the Ram
    private Rigidbody2D body; // A link to the rigidbody of the Ram

    // Pause variables
    private bool paused;
    private Vector2 pausedVelocity;
    private float pausedAngularVelocity;

    void Awake()
    {
        // Setting up references
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        
        // Set default speed
        currentSpeed = defaultSpeed;
    }

	// Update is called once per frame
	void Update () 
    {
        if (Game.instance.currentState == GameState.Paused ||
            Game.instance.currentState == GameState.GameOver)
        {
            Pause();
        }
        else if (Game.instance.currentState == GameState.HoldingPhase)
        {
            Destroy(gameObject);
        }
        else
        {
            Resume();
        }
	}

    private void FixedUpdate()
    {
        // Move the ram
        Move();
    }

    public void Move()
    {   
        if (paused) return;

        // Move the ram
        body.velocity = new Vector2(currentSpeed, body.velocity.y);
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
}
