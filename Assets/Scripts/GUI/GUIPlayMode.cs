using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIPlayMode : GUIBaseClass
{
    public static GUIPlayMode instance;

    [SerializeField]
    private GameObject DistanceIndicatorObject;
    private Text DistanceIndicatorText;
    private Animator DistanceIndicatorAnimator;

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

        // Get the referenced objects
        DistanceIndicatorText = DistanceIndicatorObject.GetComponent<Text>();
        DistanceIndicatorAnimator = DistanceIndicatorObject.GetComponent<Animator>();
    }

    public void TriggerDistanceIndicator(string msg)
    {
        if (DistanceIndicatorAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            DistanceIndicatorText.text = msg;
            DistanceIndicatorAnimator.SetTrigger("Popup");
        }
    }

}
