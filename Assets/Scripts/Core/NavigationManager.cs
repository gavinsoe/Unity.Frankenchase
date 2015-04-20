using UnityEngine;
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
        Game.instance.currentState = GameState.GameOver;

        // Show and hide GUIs
        GUIGameOver.SetActive(true);

        // Set character states
        Character2D.instance.Kill();
        Frankenstein.instance.Kill();
    }

    public void Play()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;

        // Show and hide GUIs
        GUIScore.SetActive(true);
    }

    public void Paused()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.Paused;

        // Show and hide GUIs
        GUIPause.SetActive(true);
        GUIPlayMode.SetActive(false);

        // Set character states
        Character2D.instance.Pause();
        Frankenstein.instance.Pause();
    }

    public void Resume()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.ChasingPhase;

        // Show and hide GUIs
        GUIPause.SetActive(false);
        GUIPlayMode.SetActive(true);

        // Set character states
        Character2D.instance.Resume();
        Frankenstein.instance.Resume();
    }

    public void TitleScreen()
    {
        // Update the state of the game
        Game.instance.currentState = GameState.TitleScreen;

        // Show and hide GUIs
        GUITitleScreen.SetActive(true);
        
    }

    public void HighScore()
    {

    }

    public void Rate()
    {

    }

}
