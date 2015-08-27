using UnityEngine;
using UnityEngine.UI;
using Soomla.Levelup;
using System.Collections;

public class GUITitleScreen : GUIBaseClass 
{
    public static GUITitleScreen instance;

    [SerializeField] 
    private GameObject HighscoreObject; // A reference to the object containing the highscore text
    private Text HighscoreText; // A reference to the highscore text object

    [SerializeField]
    private GameObject SoundToggleBtnObject; // A referebce to the object containing the sound toggle button
    private Toggle SoundToggleBtn; // A direct reference to the Toggle Component of the GUI object

    void Awake()
    {
        // make sure there is only 1 instance of this class.
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Get the referenced object
        HighscoreText = HighscoreObject.GetComponent<Text>();
        SoundToggleBtn = SoundToggleBtnObject.GetComponent<Toggle>();
    }

    void Start()
    {
        // Initialise button states
        if (SoundToggleBtn.isOn != GameData.settingSound)
        {
            SoundToggleBtn.isOn = GameData.settingSound;
            ToggleSound();
        } else
        {
            ToggleSound();
        }
            
    }
    
    public void UpdateHighscore()
    {
        var score = SoomlaLevelUp.GetLevel(Constants.lvlup_level_main).GetSingleScore().Record;
        if (score < 0 ) score = 0;
        HighscoreText.text = score.ToString("0");
    }

    public void ToggleSound()
    {
        
        GameData.settingSound = SoundToggleBtn.isOn;

        if (GameData.settingSound)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }
}
