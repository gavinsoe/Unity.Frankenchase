using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


// Script used to store the core game data such as the score
// State of the game

public class Game : MonoBehaviour 
{
    public static Game instance; // An accessible variable that is usable by other classes

    public GameState currentState; // Stores the state of the game.

    public GameObject GUIScore; // A reference to the GUI object to update the interface
    private Text GUIScoreText; //  A direct reference to the Text object of the GUI

    private float distanceTraveled; // Keeps track on how far the professor has travelled.
    private Vector3 lastPosition; // used to calculate the distance the professor has traveled
    public float distanceScoreMultiplier = 1f; // To be multiplied by the distance to calculate score

    private float holdPhaseBonus; // Bonus score obtained in the holding phase
    public float holdPhaseScoreMultiplier = 50f; // To be multiplied by the holding time to calculate score.

    private float score; // The total score of current playthrough

    void Awake()
    {
        // Make sure there is only 1 instance of this class.
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Retrieve object references
        GUIScoreText = GUIScore.GetComponent<Text>();

        // Temporarily set to Chasing Phase, should be TitleScreen
        currentState = GameState.ChasingPhase;
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        // Calculate the distance travelled by professor.
        var deltaDistance = Mathf.Abs(Character2D.instance.transform.position.x - lastPosition.x);
        distanceTraveled += deltaDistance * distanceScoreMultiplier;
        lastPosition = Character2D.instance.transform.position;

        if (currentState == GameState.HoldingPhase)
        {
            holdPhaseBonus += Time.deltaTime * holdPhaseScoreMultiplier;
        }
        
    }

    void FixedUpdate()
    {
        score = distanceTraveled + holdPhaseBonus;
        // Update the score on the GUI
        GUIScoreText.text = score.ToString("0");
    }

    private void Reset()
    {
        distanceTraveled = 0;
        score = 0;
        lastPosition = Character2D.instance.transform.position;
    }
}
