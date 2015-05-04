using UnityEngine;
using UnityEngine.UI;
using Soomla.Profile;
using Soomla.Levelup;
using System.Collections;

public class NavigationManager : MonoBehaviour 
{
    public static NavigationManager instance;

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
        GUIGameOver.instance.Show();
        GUIHoldingPhase.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Hide();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

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
        GUIGameOver.instance.Hide();
        GUIHoldingPhase.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Hide();
        GUIScore.instance.Hide();
        GUITitleScreen.instance.Show();
    }

    public void Retry()
    {
        if (Game.instance.currentState != GameState.GameOver)
        {
            SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).End(false);
        }

        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;
        Game.instance.InitializeStats();

        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIHoldingPhase.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        Application.LoadLevel("Game");

        // Update SoomlaLevelUp State
        //SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Restart(true);        
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();
    }

    public void Play()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;
        //Game.instance.Reinitialise();

        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIHoldingPhase.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        Application.LoadLevel("Game");
        Game.instance.InitializeStats();

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();
    }

    public void Paused()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.Paused;

        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIHoldingPhase.instance.Hide();
        GUIPause.instance.Show();
        GUIPlayMode.instance.Hide();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

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
        GUIGameOver.instance.Hide();
        GUIHoldingPhase.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

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
        // Set the direction the professor is to run to
        Game.instance.SetDirection();

        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIHoldingPhase.instance.Show();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Hide();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

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
        GUIGameOver.instance.Hide();
        GUIHoldingPhase.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        // Set character states
        Character2D.instance.Resume();
        Frankenstein.instance.Resume();
    }

    public void Rate()
    {
        SoomlaProfile.OpenAppRatingPage();
    }

}
