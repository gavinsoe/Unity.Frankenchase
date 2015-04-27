using UnityEngine;
using UnityEngine.UI;
using Soomla.Highway;
using Soomla.Levelup;
using Soomla.Profile;
using System;
using System.Collections;


// Script used to store the core game data such as the score
// State of the game

public class Game : MonoBehaviour 
{
    public static Game instance; // An accessible variable that is usable by other classes

    public GameState currentState = GameState.TitleScreen; // Stores the state of the game.

    public GameObject GUIScore; // A reference to the GUI object to update the interface
    private Text GUIScoreText; //  A direct reference to the Text object of the Score GUI
    public GameObject GUIHighscore; // A reference to the GUI object to update the interface
    public Text GUIHighscoreText; // A direct reference to the Text object of the Highscore GUI

    private Vector3 lastPosition; // used to calculate the distance the professor has traveled
    private float distanceScore; // Keeps track on how far the professor has travelled.
    public float distanceScoreMultiplier = 1f; // To be multiplied by the distance to calculate score
    private float holdPhaseScore; // Bonus score obtained in the holding phase
    public float holdPhaseScoreMultiplier = 50f; // To be multiplied by the holding time to calculate score.

    public Direction currentDirection; // The direction the character is travelling
    public Environment currentEnvironment; // The current environment the character is in
    public float currentLocation; // current location of the character (different from distance travelled)
    public float locationCastleEnd; 
    public float locationGraveyardEnd;
    public float locationTownEnd;
    


    public float score; // The total score of current playthrough

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
        GUIHighscoreText = GUIHighscore.GetComponent<Text>();
    }

    void Start()
    {
        // Initialise game
        InitializeWorld();

        // Retrieve highscore
        GUIHighscoreText.text =
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).GetSingleScore().Record.ToString("0");

        // Default to Title Screen
        NavigationManager.instance.TitleScreen();
    }
    
    void Update()
    {
        if (currentState == GameState.ChasingPhase ||
            currentState == GameState.HoldingPhase)
        {
            // Distance travelled within the last frame.
            var deltaDistance = Mathf.Abs(Character2D.instance.transform.position.x - lastPosition.x);

            UpdateScore(deltaDistance);

            UpdateEnvironment(deltaDistance);

            // Store location of last position 
            lastPosition = Character2D.instance.transform.position;
        }

    }

    private void UpdateScore(float deltaDistance)
    {
        // Calculate the score obtained by distance travelled
        distanceScore += deltaDistance * distanceScoreMultiplier;

        // Calculate holding phase score
        if (currentState == GameState.HoldingPhase)
        {
            holdPhaseScore += Time.deltaTime * holdPhaseScoreMultiplier;
        }

        // Update score and GUI
        score = distanceScore + holdPhaseScore;
        GUIScoreText.text = score.ToString("0");
    }

    private void UpdateEnvironment(float deltaDistance)
    {
        if (currentDirection == Direction.Right)
        {
            currentLocation += deltaDistance;
            // Cannot go above the last environment
            if (currentLocation > locationTownEnd)
            {
                currentLocation = locationTownEnd;
            }
        }
        else
        {
            currentLocation -= deltaDistance;
            // Cannot go lower than 0
            if (currentLocation < 0)
            {
                currentLocation = 0;
            }
        }

        // Determine the current location
        if (currentLocation < locationCastleEnd)
        {
            currentEnvironment = Environment.Castle;
        }
        else if (currentLocation < locationGraveyardEnd)
        {
            currentEnvironment = Environment.Graveyard;
        }
        else
        {
            currentEnvironment = Environment.Town;
        }
    }

    // This is called after the end of a holding phase to determine which direction to go to.
    public void SetDirection()
    {
        // Determine which direction to move to
        var rand = UnityEngine.Random.Range(0, locationTownEnd);
        if (rand < currentLocation)
        {
            Debug.Log("Set Direction [" + rand + "] Left");
            currentDirection = Direction.Left;
        }
        else
        {
            Debug.Log("Set Direction [" + rand + "] Right");
            currentDirection = Direction.Right;
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;

        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).SetSingleScoreValue(score);
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).End(true);
        
        GUIHighscoreText.text = 
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).GetSingleScore().Record.ToString("0");
    }

    // Initialise game state
    public void InitializeStats()
    {
        distanceScore = 0;
        holdPhaseScore = 0;
        score = 0;

        // Update the score on the GUI
        GUIScoreText.text = score.ToString("0");

        // Initialise location
        currentDirection = Direction.Right;
        currentEnvironment = Environment.Castle;
        currentLocation = 0;
    }


    // Initialise the world?
    private void InitializeWorld()
    {
        // Initialise Soomla Highway (online statistics)
        //SoomlaHighway.Initialize();
        
        // Initialise Soomla Profile (Social media integrations)
        SoomlaProfile.Initialize();

        // Initialise LevelUp (along with the initial world)
        World world = new World(Constants.lvlup_world_main);

        Score level_score = new Score(
            Constants.lvlup_score_main, // ID
            "Main Score",               // Name
            true                        // Ascending (higher is better)
        );

        Level level = new Level(Constants.lvlup_level_main);
        level.AddScore(level_score);

        world.AddInnerWorld(level);
        SoomlaLevelUp.Initialize(world);
    }

    
}
