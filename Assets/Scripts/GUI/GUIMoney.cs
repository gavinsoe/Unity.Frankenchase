using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIMoney : GUIBaseClass 
{
    public static GUIMoney instance;

    [SerializeField]
    private GameObject MoneyTextObject;
    private Text MoneyText;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        MoneyText = MoneyTextObject.GetComponent<Text>();
    }

    void OnEnable()
    {
        UpdateGold();
    }

    public void UpdateGold()
    {
        anim.SetTrigger("Chaching");
        MoneyText.text = GameData.money.ToString();
    }
}
