using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldingPhase : MonoBehaviour {

    public static HoldingPhase instance;

    private int counter = 0;

    public float tapStrength;
    public float maxEndurance;
    public float reductionCoefficient_1;
    public float reductionCoefficient_2;

    private float endurance;
    private float secondaryEndurance;
    
    private bool active; // When the holding phase is active

    public float frankSpeedupAfterHold; // Frankenstein's speed after holding phase
    public float frankSpeedupDuration; // Duration of the speed buff

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
            endurance -= Time.deltaTime * reductionCoefficient_1;
            secondaryEndurance -= Time.deltaTime * reductionCoefficient_2;

            if (secondaryEndurance >= endurance) secondaryEndurance = endurance;

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
        endurance = maxEndurance;
        secondaryEndurance = maxEndurance;
        active = true;
    }

    public void EndEvent()
    {
        active = false;

        NavigationManager.instance.HoldingPhaseEnd();

        // Boost Frankenstein's speed
        Frankenstein.instance.SetSpeed(frankSpeedupAfterHold, frankSpeedupDuration);
        Frankenstein.instance.EnableInvincibility(frankSpeedupDuration);
    }

    public void Tapped()
    {
        //Debug.Log("tapped");
        secondaryEndurance += tapStrength;
    }
}
