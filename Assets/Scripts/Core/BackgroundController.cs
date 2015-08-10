using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public GameObject referencedObject;
    public float multiplier;
    private Renderer background;

	// Use this for initialization
	void Start () {
        background = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        background.material.mainTextureOffset = new Vector2(multiplier * referencedObject.transform.position.x, background.material.mainTextureOffset.y);
	}
}
