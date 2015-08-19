using UnityEngine;
using System.Collections;

public class CameraRunner : MonoBehaviour {
    public static CameraRunner instance;
    private Transform player;

    public float cameraXOffset = 85;
    public float cameraYOffset = 80;
    public float cameraZOffset = -10;

    #region camera shake

    private float shake = 0;
    private float shakeAmount = 0.025f;
    private float decreaseFactor = 1.0f;
    private Vector2 shakeOffset;

    #endregion
    	
    private void Awake()
    {
        instance = this;
    }

	// Update is called once per frame
	void Update () 
    {
        if (player == null) player = Character2D.instance.transform;

        if (shake > 0)
        {
            shakeOffset = Random.insideUnitCircle * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else 
        {
            shakeOffset = new Vector2(0, 0);
            shake = 0;
        }
        
        transform.localPosition = new Vector3(player.position.x + cameraXOffset + shakeOffset.x, cameraYOffset + shakeOffset.y, cameraZOffset);
        
	}

    public void Shake(float duration)
    {
        shake = duration;
    }
    
}

