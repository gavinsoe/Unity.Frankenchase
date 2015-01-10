using UnityEngine;
using System.Collections;

public class MainGameGUI : MonoBehaviour {

    float playerScore = 0;

	// Update is called once per frame
	void Update () {
        playerScore += Time.deltaTime;
	}

    public void IncreaseScore (int amount)
    {
        playerScore += amount;
    }
}
