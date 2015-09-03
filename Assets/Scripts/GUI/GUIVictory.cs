using UnityEngine;
using System.Collections;

public class GUIVictory : GUIBaseClass  
{
    public static GUIVictory instance;

	void Awake () 
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
