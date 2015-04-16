using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldingPhase : MonoBehaviour {

    public static HoldingPhase instance;

    private int counter = 0;

    public float tapStrength;
    public float endurance;
    public float secondaryEndurance;
    
    private bool active; // When the holding phase is active

    
    public GameObject prof;
    private RectTransform profTransform;
    public float profStartPosition;
    public float profEndPosition;

    public GameObject frank;
    private RectTransform frankTransform;
    public float frankStartPosition;
    public float frankEndPosition;

    public float frankSpeedupAfterHold;
    public float frankSpeedupDuration;

    void Awake()
    {
        // set the static variable so that other classes can easily use this class
        instance = this;

        // Retrieve the Transform components
        profTransform = prof.GetComponent<RectTransform>();
        frankTransform = frank.GetComponent<RectTransform>();

        // disable object
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
	    if (active)
        {
            endurance -= Time.deltaTime * 10;
            secondaryEndurance -= Time.deltaTime * 10;
            if (secondaryEndurance >= 50) secondaryEndurance = 50;

            float xPositionOffset  = (frankEndPosition - frankStartPosition)*(endurance + secondaryEndurance) / 100;

            frankTransform.anchoredPosition = new Vector2(
                    frankEndPosition - xPositionOffset,
                    frankTransform.anchoredPosition.y
                );


            if (frankTransform.anchoredPosition.x >= frankEndPosition)
            {
                EndEvent();
            }
        }
	}

    public void TriggerEvent()
    {
        // Stop player and frankenstein from running
        Character2D.instance.Pause();
        Frankenstein.instance.Pause();

        // Show GUI
        gameObject.SetActive(true);
        Game.instance.currentState = GameState.HoldingPhase;

        // Reinitialise the position of Frankenstein and the Professor
        profTransform.anchoredPosition = new Vector2(
                profStartPosition,
                profTransform.anchoredPosition.y
            );

        frankTransform.anchoredPosition = new Vector2(
                frankStartPosition,
                frankTransform.anchoredPosition.y
            );

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
        Game.instance.currentState = GameState.ChasingPhase;
        gameObject.SetActive(false);

        // Let frankenstein and player to continue running
        Character2D.instance.Resume();
        Frankenstein.instance.Resume();
        Frankenstein.instance.SetSpeed(frankSpeedupAfterHold, frankSpeedupDuration);
    }

    public void Tapped()
    {
        //Debug.Log("tapped");
        secondaryEndurance += tapStrength;
    }
}
