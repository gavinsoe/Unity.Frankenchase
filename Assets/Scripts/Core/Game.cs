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

    private float distanceTraveled; // Keeps track on how far the professor has travelled.
    private Vector3 lastPosition; // used to calculate the distance the professor has traveled
    public float distanceScoreMultiplier = 1f; // To be multiplied by the distance to calculate score

    private float holdPhaseBonus; // Bonus score obtained in the holding phase
    public float holdPhaseScoreMultiplier = 50f; // To be multiplied by the holding time to calculate score.

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
        Initialise();

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
            // Calculate the distance travelled by professor.
            var deltaDistance = Mathf.Abs(Character2D.instance.transform.position.x - lastPosition.x);
            distanceTraveled += deltaDistance * distanceScoreMultiplier;
            lastPosition = Character2D.instance.transform.position;

            if (currentState == GameState.HoldingPhase)
            {
                holdPhaseBonus += Time.deltaTime * holdPhaseScoreMultiplier;
            }
        }
    }

    void FixedUpdate()
    {
        score = distanceTraveled + holdPhaseBonus;
        // Update the score on the GUI
        GUIScoreText.text = score.ToString("0");
        
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;

        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).SetSingleScoreValue(score);
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).End(true);
        
        GUIHighscoreText.text = 
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).GetSingleScore().Record.ToString("0");
    }

    public void Reinitialise()
    {
        lastPosition = transform.position;
        distanceTraveled = 0;
        holdPhaseBonus = 0;
        score = 0;
        // Update the score on the GUI
        GUIScoreText.text = score.ToString("0");
    }

    // Initialise the world?
    private void Initialise()
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
