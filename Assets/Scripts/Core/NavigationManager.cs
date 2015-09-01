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
        // Show and hide GUIs
        GUIGameOver.instance.Show();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Hide();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        // Update the state of the game
        Game.instance.GameOver();

        // Set character states
        Character2D.instance.Kill();
        MonsterController.instance.Kill();
    }

    public void TitleScreen()
    {
        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Hide();
        GUIScore.instance.Hide();
        GUITitleScreen.instance.Show();

        // Load the Scene
        Application.LoadLevel("Title Screen");

        // Update the state of the game
        Game.instance.currentState = GameState.TitleScreen;
    }

    public void Retry()
    {
        // Let soomla know that a stage has ended
        if (Game.instance.currentState != GameState.GameOver)
        {
            SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).End(false);
        }

        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        // Load the Scene
        Application.LoadLevel("Game");

        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;
        Game.instance.InitializeStats();

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();
    }

    public void Play()
    {

        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        // Load the scene
        Application.LoadLevel("Game");

        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;
        Game.instance.InitializeStats();

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();
    }

    public void Paused()
    {
        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIPause.instance.Show();
        GUIPlayMode.instance.Hide();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        // Update the state of the game
        Game.instance.currentState = GameState.Paused;

        // Set character states
        Character2D.instance.Pause();
        MonsterController.instance.Pause();

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Pause();

        Time.timeScale = 0;
    }

    public void Resume()
    {
        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;

        // Set character states
        Character2D.instance.Resume();
        MonsterController.instance.Resume();

        // Update SoomlaLevelUp State
        SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).Start();

        Time.timeScale = 1;
    }

    public void HoldingPhaseEnd()
    {
        // Show and hide GUIs
        GUIGameOver.instance.Hide();
        GUIPause.instance.Hide();
        GUIPlayMode.instance.Show();
        GUIScore.instance.Show();
        GUITitleScreen.instance.Hide();

        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;

        // Set character states
        Character2D.instance.Resume();
        MonsterController.instance.Resume();
    }

    public void Rate()
    {
        SoomlaProfile.OpenAppRatingPage();
    }

}
