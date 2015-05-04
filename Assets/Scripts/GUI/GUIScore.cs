using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScore : GUIBaseClass
{
    public static GUIScore instance;

    [SerializeField] 
    private GameObject ScoreObject; // A reference to the object containing the score text
    private Text ScoreText; // A reference to the score text object

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
        ScoreText = ScoreObject.GetComponent<Text>();
    }

    public void SetScore(double score)
    {
        ScoreText.text = score.ToString("0");
    }
}
