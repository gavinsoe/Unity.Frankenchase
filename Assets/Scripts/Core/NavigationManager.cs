using UnityEngine;
using UnityEngine.UI;
using Soomla.Profile;
using Soomla.Levelup;
using System.Collections;

public class NavigationManager : MonoBehaviour 
{
    public static NavigationManager instance;

    public GameObject GUIGameOver;
    public GameObject GUIHoldingPhase;
    public GameObject GUIPause;
    public GameObject GUIPlayMode;
    public GameObject GUIScore;
    public GameObject GUITitleScreen;

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
    }
    
    public void GameOver()
    {
        // Update the state of the game
        Game.instance.GameOver();

        // Show and hide GUIs
        GUIGameOver.SetActive(true);
        GUIHoldingPhase.SetActive(false);
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(false);
        GUIScore.SetActive(true);
        GUITitleScreen.SetActive(false);

        // Set character states
        Character2D.instance.Kill();
        Frankenstein.instance.Kill();
    }

    public void TitleScreen()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.TitleScreen;

        Application.LoadLevel("Title Screen");

        // Show and hide GUIs
        GUIGameOver.SetActive(false);
        GUIHoldingPhase.SetActive(false);
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(false);
        GUIScore.SetActive(false);
        GUITitleScreen.SetActive(true);
    }

    public void Retry()
    {
        if (Game.instance.currentState != GameState.GameOver)
        {
            SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).End(false);
        }

        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;
        Game.instance.Reinitialise();

        // Show and hide GUIs
        GUIGameOver.SetActive(false);
        GUIHoldingPhase.SetActive(false);
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(true);
        GUIScore.SetActive(true);
        GUITitleScreen.SetActive(false);

        Application.LoadLevel("Game");

        // Update SoomlaLevelUp State
        //SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Restart(true);        
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();
    }

    public void Play()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;
        Game.instance.Reinitialise();

        // Show and hide GUIs
        GUIGameOver.SetActive(false);
        GUIHoldingPhase.SetActive(false);
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(true);
        GUIScore.SetActive(true);
        GUITitleScreen.SetActive(false);

        Application.LoadLevel("Game");

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();
    }

    public void Paused()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.Paused;

        // Show and hide GUIs

        GUIGameOver.SetActive(false);
        GUIHoldingPhase.SetActive(false);
        GUIPause.SetActive(true);
        GUIPlayMode.SetActive(false);
        GUIScore.SetActive(true);
        GUITitleScreen.SetActive(false);

        // Set character states
        Character2D.instance.Pause();
        Frankenstein.instance.Pause();

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Pause();
    }

    public void Resume()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;

        // Show and hide GUIs
        GUIGameOver.SetActive(false);
        GUIHoldingPhase.SetActive(false);
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(true);
        GUIScore.SetActive(true);
        GUITitleScreen.SetActive(false);

        // Set character states
        Character2D.instance.Resume();
        Frankenstein.instance.Resume();

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();
    }

    public void HoldingPhaseStart()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.HoldingPhase;

        // Show and hide GUIs
        GUIGameOver.SetActive(false);
        GUIHoldingPhase.SetActive(true);
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(false);
        GUIScore.SetActive(true);
        GUITitleScreen.SetActive(false);

        // Set character states
        Character2D.instance.Pause();
        Frankenstein.instance.Pause();

        // Trigger event
        HoldingPhase.instance.TriggerEvent();
    }

    public void HoldingPhaseEnd()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;

        // Show and hide GUIs
        GUIGameOver.SetActive(false);
        GUIHoldingPhase.SetActive(false);
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(true);
        GUIScore.SetActive(true);
        GUITitleScreen.SetActive(false);

        // Set character states
        Character2D.instance.Resume();
        Frankenstein.instance.Resume();
    }

    public void Rate()
    {
        SoomlaProfile.OpenAppRatingPage();
    }

}
