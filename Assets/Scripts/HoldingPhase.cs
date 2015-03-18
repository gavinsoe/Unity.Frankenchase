using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldingPhase : MonoBehaviour {

    public static HoldingPhase instance;

    private int counter = 0;
    private GameObject canvas;
    private Image image;

    public float tapStrength;
    public float endurance;
    public float secondaryEndurance;
    private bool active;
    void Awake()
    {
        // set the static variable so that other classes can easily use this class
        instance = this;

        // Hide canvas as a start
        image = gameObject.GetComponent<Image>();
        image.fillAmount = 1;

        canvas = gameObject.transform.parent.gameObject;
        canvas.SetActive(false);


        // Disable the update
        active = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (active)
        {
            endurance -= Time.deltaTime * 10;
            secondaryEndurance -= Time.deltaTime * 10;
            if (secondaryEndurance >= 50) secondaryEndurance = 50;
            
            image.fillAmount = (endurance + secondaryEndurance) / 100;

            if (image.fillAmount <= 0)
            {
                EndEvent();
            }
        }
	}

    public void TriggerEvent()
    {
        // Stop player and frankenstein from running
        Character2DController.instance.SetSpeed(0);
        Chaser.instance.SetSpeed(0);
        Spawner.instance.CancelInvoke();

        // Show GUI
        canvas.SetActive(true);

        endurance = 50;
        secondaryEndurance = 50;
        active = true;

        tapStrength = 5 - counter;
        if (tapStrength == 0) tapStrength = 1;
    }

    public void EndEvent()
    {
        active = false;
        canvas.SetActive(false);

        // Stop player and frankenstein from running
        Character2DController.instance.SetSpeed(1);
        Chaser.instance.SetSpeed(1);
        Chaser.instance.character.SetSpeed(14, 4);
    }

    public void Tapped()
    {
        Debug.Log("tapped");
        secondaryEndurance += tapStrength;
    }
}
