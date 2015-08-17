using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public GameObject referencedObject;
    public float multiplier;
    private Renderer bgRenderer;
    public Background[] backgrounds;

	// Use this for initialization
	void Start ()
    {
        bgRenderer = gameObject.GetComponent<Renderer>();
        foreach (var bg in backgrounds)
        {
            if (bg.environment == Game.instance.currentEnvironment)
            {
                bgRenderer.material = bg.material;
                multiplier = bg.movementMultiplier;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        bgRenderer.material.mainTextureOffset = 
            new Vector2(multiplier * referencedObject.transform.position.x,
                        bgRenderer.material.mainTextureOffset.y);
	}
}