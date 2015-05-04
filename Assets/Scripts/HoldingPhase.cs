using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldingPhase : MonoBehaviour {

    public static HoldingPhase instance;

    private int counter = 0;

    public float tapStrength;
    public float endurance;
    public float secondaryEndurance;
    
    public bool active; // When the holding phase is active

    public float frankSpeedupAfterHold;
    public float frankSpeedupDuration;

    void Awake()
    {
        // make sure there is only 1 instance of this class.
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
	    if (active)
        {
            endurance -= Time.deltaTime * 10;
            secondaryEndurance -= Time.deltaTime * 10;
            if (secondaryEndurance >= 50) secondaryEndurance = 50;

            var progress = (endurance + secondaryEndurance) / 100;
            GUIHoldingPhase.instance.UpdatePositions(progress);

            if (progress < 0)
            {
                EndEvent();
            }
        }
	}

    public void TriggerEvent()
    {
        // Initialise the endurance
        endurance = 50;
        secondaryEndurance = 50;
        active = true;

        tapStrength = 5 - counter;
        if (tapStrength == 0) tapStrength = 1;
    }

    public void EndEvent()
    {
        active = false;

        NavigationManager.instance.HoldingPhaseEnd();

        // Boost Frankenstein's speed
        Frankenstein.instance.SetSpeed(frankSpeedupAfterHold, frankSpeedupDuration);
    }

    public void Tapped()
    {
        //Debug.Log("tapped");
        secondaryEndurance += tapStrength;
    }
}
