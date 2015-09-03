using UnityEngine;
using UnityEngine.UI;
using Soomla.Store;
using Soomla.Highway;
using Soomla.Levelup;
using Soomla.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// Script used to store the core game data such as the score
// State of the game

public class Game : MonoBehaviour 
{
    public static Game instance; // An accessible variable that is usable by other classes

    public GameState currentState = GameState.TitleScreen; // Stores the state of the game.

    public float distanceFromMonster;
    public float maxDistanceFromMonster;

    public Environment currentEnvironment; // The current environment the character is in
    public float currentLocation; // current location of the character (different from distance travelled)

    // Collection of stages
    public List<StageSection> sectionsCastle;
    public List<StageSection> sectionsGraveyard;
    public List<StageSection> sectionsTown;

    public Dictionary<string, Weapon> weaponArsenal; // List of available weapons (and upgrade Levels)

    // Stage sections that are currently active
    private Dictionary<string, StageSection> sectionsActive;

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

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        // Initialise game
        InitializeWorld();

        // Retreive / Update highscore
        GUITitleScreen.instance.UpdateHighscore();

        // Default to Title Screen
        NavigationManager.instance.TitleScreen();
    }
    
    void Update()
    {
        if (currentState == GameState.ChasingPhase)
        {
            UpdateDistanceFromTarget();
        }
    }

    /// <summary>
    /// Currently not used, environment will be based on the Monster's health.
    /// </summary>
    /// <param name="deltaDistance"></param>

    private void UpdateDistanceFromTarget()
    {
        if (Character2D.instance != null && MonsterController.instance != null)
        {
            distanceFromMonster = Mathf.Abs(Character2D.instance.transform.position.x -
                                                 MonsterController.instance.transform.position.x);

            // Depending on how far apart they are, display certain messages
            if (distanceFromMonster > maxDistanceFromMonster)
            {
                // Game Over
                NavigationManager.instance.GameOver();
            }
            else if (distanceFromMonster > maxDistanceFromMonster * 0.8)
            {
            }
            else if (distanceFromMonster > maxDistanceFromMonster * 0.6)
            {
            }
            else if (distanceFromMonster > maxDistanceFromMonster * 0.4)
            {
            }
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;

        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).End(true);

        GUITitleScreen.instance.UpdateHighscore();
    }

    // Initialise game state
    public void InitializeStats()
    {
        // Initialise location        
        currentEnvironment = Environment.Castle;
        currentLocation = 0;

        ResetStageSections();
    }

    // Initialise the world?
    private void InitializeWorld()
    {
        // Initialise Soomla Highway (online statistics)
        SoomlaHighway.Initialize();
        
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

        InitializeArsenal();
    }

    #region Weapons

    private void InitializeArsenal()
    {
        weaponArsenal = new Dictionary<string, Weapon>();
        weaponArsenal.Add(Constants.weapon_sword, new Weapon(Constants.weapon_sword));
        weaponArsenal.Add(Constants.weapon_whip, new Weapon(Constants.weapon_whip));
        weaponArsenal.Add(Constants.weapon_crossbow, new Weapon(Constants.weapon_crossbow));
    }

    public Weapon GetEquippedWeapon()
    {
        return weaponArsenal[GameData.equippedWeapon];
    }

    public void EquipWeapon(string weapon_id)
    {
        GameData.equippedWeapon = weapon_id;
    }

    #endregion
    #region Stage Sections

    public StageSection GetSection()
    {
        if (currentEnvironment == Environment.Castle)
        {
            var sections = sectionsCastle
                           .Where(s => s.startRank <= GameData.rank && 
                                      (s.endRank >= GameData.rank || s.endRank == 0)).ToArray();
            
            // Randomise which section to retrieve
            var rand = UnityEngine.Random.Range(0, sections.Length);

            // Remove specific section from list.
            sectionsCastle.RemoveAt(rand);

            // Return the section
            return sections[rand];
        }
        else if (currentEnvironment == Environment.Graveyard)
        {
            var sections = sectionsGraveyard
                           .Where(s => s.startRank <= GameData.rank &&
                                      (s.endRank >= GameData.rank || s.endRank == 0)).ToArray();

            // Randomise which section to retrieve
            var rand = UnityEngine.Random.Range(0, sections.Length);

            // Remove specific section from list.
            sectionsGraveyard.RemoveAt(rand);

            // Return the section
            return sections[rand];
        }
        else // if (currentEnvironment == Environment.Town)
        {
            var sections = sectionsTown
                           .Where(s => s.startRank <= GameData.rank &&
                                      (s.endRank >= GameData.rank || s.endRank == 0)).ToArray();

            // Randomise which section to retrieve
            var rand = UnityEngine.Random.Range(0, sections.Length);

            // Remove specific section from list.
            sectionsTown.RemoveAt(rand);

            // Return the section
            return sections[rand];
        }
    }

    public void PlaceSectionInActiveList(string key, StageSection section)
    {
        sectionsActive.Add(key, section);
    }

    public void ReturnSection(string name)
    {
        if (sectionsActive.ContainsKey(name))
        {
            if (sectionsActive[name].environment == Environment.Castle)
            {
                sectionsCastle.Add(sectionsActive[name]);
                sectionsActive.Remove(name);
            }
            else if (sectionsActive[name].environment == Environment.Graveyard)
            {
                sectionsGraveyard.Add(sectionsActive[name]);
                sectionsActive.Remove(name);
            }
            else if (sectionsActive[name].environment == Environment.Town)
            {
                sectionsTown.Add(sectionsActive[name]);
                sectionsActive.Remove(name);
            }
        }
    }

    public void ResetStageSections()
    {
        // Initialise (or reset) Stage Sections
        if (sectionsActive != null)
        {
            foreach (var section in sectionsActive.Values)
            {
                if (section.environment == Environment.Castle)
                {
                    sectionsCastle.Add(section);
                }
                else if (section.environment == Environment.Graveyard)
                {
                    sectionsGraveyard.Add(section);
                }
                else if (section.environment == Environment.Town)
                {
                    sectionsTown.Add(section);
                }
            }
        }

        sectionsActive = new Dictionary<string, StageSection>();

        // Make sure the 'environment' variable on each stage section is set properly
        foreach (var section in sectionsCastle) section.environment = Environment.Castle;
        foreach (var section in sectionsGraveyard) section.environment = Environment.Graveyard;
        foreach (var section in sectionsTown) section.environment = Environment.Town;
    }
    
    #endregion
}
