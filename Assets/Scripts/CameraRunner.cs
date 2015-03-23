using UnityEngine;
using System.Collections;

public class CameraRunner : MonoBehaviour {

    public Transform player;

    public float cameraXOffset = 85;
    public float cameraYOffset = 80;
    public float cameraZOffset = -10;
    	
	// Update is called once per frame
	void Update () 
    {
        transform.position = new Vector3(player.position.x + cameraXOffset, cameraYOffset, cameraZOffset);
	}
}
