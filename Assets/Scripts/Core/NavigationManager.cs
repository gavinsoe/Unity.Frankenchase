using UnityEngine;
using System.Collections;

public class NavigationManager : MonoBehaviour 
{
    public static NavigationManager instance;

    public GameObject GUIGameOver;
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
    
    public void ShowGameOverGUI()
    {
        GUIGameOver.SetActive(true);
        Game.instance.currentState = GameState.GameOver;
    }

    public void HideGameOverGUI()
    {
        GUIGameOver.SetActive(false);
    }

    public void ShowTitleScreenGUI()
    {
        GUITitleScreen.SetActive(true);
        Game.instance.currentState = GameState.TitleScreen;
    }

    public void HideTitleScreenGUI()
    {
        GUITitleScreen.SetActive(false);
    }

}
