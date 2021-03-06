﻿using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

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
                if (bg.material == null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    bgRenderer.material = bg.material;
                    multiplier = bg.movementMultiplier;
                    transform.localScale = new Vector3(bg.scaleX, bg.scaleY, 1);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Character2D.instance != null &&
            Character2D.instance.transform != null)
        {
            bgRenderer.material.mainTextureOffset =
            new Vector2(multiplier * Character2D.instance.transform.position.x,
                        bgRenderer.material.mainTextureOffset.y);    
        }
	}
}