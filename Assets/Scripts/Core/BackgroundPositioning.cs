using UnityEngine;
using System.Collections;

public class BackgroundPositioning : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    // Basically follow main camera position
        transform.localPosition = 
            new Vector3(Camera.main.transform.localPosition.x,
                        Camera.main.transform.localPosition.y,
                        0);
            
	}
}
