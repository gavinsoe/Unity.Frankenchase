using UnityEngine;
using System.Collections;

public class CameraRunner : MonoBehaviour {

    public Transform player;

    public float cameraXOffset = 8;
    	
	// Update is called once per frame
	void Update () 
    {
        transform.position = new Vector3(player.position.x + cameraXOffset, 0, -10);
	}
}
