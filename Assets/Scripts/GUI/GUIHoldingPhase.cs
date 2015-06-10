using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    private GameObject healthObj; // Direct reference to the game object holding the health bar object.
    private RectTransform healthTransform; // A direct reference to the transform object of the health bar
    private Image healthImage; // Direct reference to the image component of the health bar
    public float healthStartPos;
    public float healthEndPos;


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
        healthTransform = healthObj.GetComponent<RectTransform>();
        healthImage = healthObj.GetComponent<Image>();
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

    public void UpdateHealth(float health)
    {
        // Update position
        healthTransform.anchoredPosition = new Vector2(
            (1 - health) * (healthEndPos - healthStartPos),
            -10
        );

        // Update Color
        if (health > 0.5)
        {
            healthImage.color = new Color(1 - (health - 0.5f) * 2, 1, 0);
        }
        else
        {
            healthImage.color = new Color(1, (health * 2), 0);
        }

    }
}
