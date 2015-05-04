using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIPlayMode : GUIBaseClass
{
    public static GUIPlayMode instance;

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
    }

}
