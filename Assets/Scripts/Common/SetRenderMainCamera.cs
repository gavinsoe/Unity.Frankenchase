using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetRenderMainCamera : MonoBehaviour {

    // This script simply sets the Main Camera as the 'Render Camera' Component of a Canvas

	// Use this for initialization
	void Start () {
        var canvas = gameObject.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
	}
}
