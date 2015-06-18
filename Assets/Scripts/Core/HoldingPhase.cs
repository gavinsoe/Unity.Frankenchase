using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldingPhase : MonoBehaviour {

    public static HoldingPhase instance;

    private int counter = 0;

    public float tapStrength;
    public float maxEndurance;
    private float endurance;
    public float holdingPhaseDuration;
    private float remainingTime;
    
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

        endurance = maxEndurance;
    }

	// Update is called once per frame
	void Update () {
	    if (active)
        {
            remainingTime -= Time.deltaTime;

            var progress = remainingTime / holdingPhaseDuration;
            var health = endurance / maxEndurance;
            GUIHoldingPhase.instance.UpdatePositions(progress);
            GUIHoldingPhase.instance.UpdateHealth(health);

            /* End the phase when one of the following is true
             *    - Duration ends
             *    - Environment is Castle and Health < 66%
             *    - Environment is Graveyard and Health < 33%
             *    - Health runs out
             */ 
            if (remainingTime < 0)
            {
                EndEvent();
            } 
            else if (Game.instance.currentEnvironment == Environment.Castle 
                     && health < 0.66)
            {
                // Change environment and end event
                Game.instance.currentEnvironment = Environment.Graveyard;
                EndEvent();
            }
            /*
            else if (Game.instance.currentEnvironment == Environment.Graveyard
                     && health < 0.33)
            {
                // Change environment and end event
                Game.instance.currentEnvironment = Environment.Town;
                EndEvent();
            }
            else if (health < 0)
            {
                EndEvent();
                NavigationManager.instance.GameOver();
            }
                 * */
        }
	}

    public void TriggerEvent()
    {
        // Initialise the endurance
        //endurance = maxEndurance;
        //secondaryEndurance = maxEndurance;
        remainingTime = holdingPhaseDuration;
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

    public void ResetEndurance()
    {
        endurance = maxEndurance;
    }

    public void Tapped()
    {
        //Debug.Log("tapped");
        //secondaryEndurance += tapStrength;
        endurance -= tapStrength;
    }
}
