using UnityEngine;
using System.Collections;

public class GUIHoldingPhase : GUIBaseClass
{
    public static GUIHoldingPhase instance;

    [SerializeField]
    private GameObject prof; // Direct reference to the game object holding the professor sprite
    private RectTransform profTransform; // A direct reference to the transform object of the prof
    public float profStartPosition; // The default starting position
    public float profEndPosition; // The end position

    [SerializeField]
    private GameObject frank; // Direct reference to the game object holding the frankenstein sprite
    private RectTransform frankTransform; // A direct reference to the transform object of frankenstein
    public float frankStartPosition;
    public float frankEndPosition;

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

        // Retrieve the transform components
        profTransform = prof.GetComponent<RectTransform>();
        frankTransform = frank.GetComponent<RectTransform>();
    }

    public override void Show()
    {
        // Make GUI visible
        gameObject.SetActive(true);

        // Initialise the start positions
        profTransform.anchoredPosition = new Vector2(
            profStartPosition,
            profTransform.anchoredPosition.y
        );
        frankTransform.anchoredPosition = new Vector2(
            frankStartPosition,
            frankTransform.anchoredPosition.y
        );
    }

    public void UpdatePositions(float progress)
    { 
        float xOffset = (frankEndPosition - frankStartPosition) * progress;

        frankTransform.anchoredPosition = new Vector2(
            frankEndPosition - xOffset,
            frankTransform.anchoredPosition.y
        );
    }
}
