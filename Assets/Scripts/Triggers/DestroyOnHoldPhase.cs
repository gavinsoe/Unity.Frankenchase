using UnityEngine;
using System.Collections;

public class DestroyOnHoldPhase : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if (Game.instance.currentState == GameState.HoldingPhase)
        {
            Destroy(gameObject);
        }
	}
}
