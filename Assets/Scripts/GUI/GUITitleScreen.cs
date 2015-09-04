using UnityEngine;
using UnityEngine.UI;
using Soomla.Levelup;
using System.Collections;

public class GUITitleScreen : GUIBaseClass 
{
    public static GUITitleScreen instance;

    [SerializeField] 
    private GameObject RankObject; // A reference to the object containing the highscore text
    private Text RankText; // A reference to the highscore text object

    [SerializeField]
    private GameObject SoundToggleBtnObject; // A referebce to the object containing the sound toggle button
    private Toggle SoundToggleBtn; // A direct reference to the Toggle Component of the GUI object

    [SerializeField]
    private GameObject SwordToggleBtnObject;
    private Toggle SwordToggleBtn;

    [SerializeField]
    private GameObject WhipToggleBtnObject;
    private Toggle WhipToggleBtn;

    [SerializeField]
    private GameObject CrossbowToggleBtnObject;
    private Toggle CrossbowToggleBtn;

    [SerializeField]
    private GameObject SwordUpgradeBtnObject;
    [SerializeField]
    private GameObject SwordUpgradeCostObject;
    private Text SwordUpgradeCost;
    [SerializeField]
    private GameObject SwordLevelObject;
    private Text SwordLevel;

    [SerializeField]
    private GameObject WhipUpgradeBtnObject;
    [SerializeField]
    private GameObject WhipUpgradeCostObject;
    private Text WhipUpgradeCost;
    [SerializeField]
    private GameObject WhipLevelObject;
    private Text WhipLevel;

    [SerializeField]
    private GameObject CrossbowUpgradeBtnObject;
    [SerializeField]
    private GameObject CrossbowUpgradeCostObject;
    private Text CrossbowUpgradeCost;
    [SerializeField]
    private GameObject CrossbowLevelObject;
    private Text CrossbowLevel;

    [SerializeField]
    private GameObject BootsUpgradeCostObject;
    private Text BootsUpgradeCost;
    [SerializeField]
    private GameObject BootsLevelObject;
    private Text BootsLevel;

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

        // Get the referenced object
        RankText = RankObject.GetComponent<Text>();
        SoundToggleBtn = SoundToggleBtnObject.GetComponent<Toggle>();
        SwordToggleBtn = SwordToggleBtnObject.GetComponent<Toggle>();
        WhipToggleBtn = WhipToggleBtnObject.GetComponent<Toggle>();
        CrossbowToggleBtn = CrossbowToggleBtnObject.GetComponent<Toggle>();

        SwordUpgradeCost = SwordUpgradeCostObject.GetComponent<Text>();
        SwordLevel = SwordLevelObject.GetComponent<Text>();

        WhipUpgradeCost = WhipUpgradeCostObject.GetComponent<Text>();
        WhipLevel = WhipLevelObject.GetComponent<Text>();

        CrossbowUpgradeCost = CrossbowUpgradeCostObject.GetComponent<Text>();
        CrossbowLevel = CrossbowLevelObject.GetComponent<Text>();

        BootsUpgradeCost = BootsUpgradeCostObject.GetComponent<Text>();
        BootsLevel = BootsLevelObject.GetComponent<Text>();
    }

    void Start()
    {
        // Initialise button states
        if (SoundToggleBtn.isOn != GameData.settingSound)
        {
            SoundToggleBtn.isOn = GameData.settingSound;
            ToggleSound();
        } 
        else
        {
            ToggleSound();
        }
        
        if (GameData.equippedWeapon == Constants.weapon_sword)
        {
            SwordToggleBtn.isOn = true;
        }
        else if (GameData.equippedWeapon == Constants.weapon_whip)
        {
            WhipToggleBtn.isOn = true;
        }
        else if (GameData.equippedWeapon == Constants.weapon_crossbow)
        {
            CrossbowToggleBtn.isOn = true;
        }
    }
    
    public void Update()
    {
        UpdateWeaponLevel();
        UpdateBootsLevel();
        UpdateRank();
    }

    public void UpdateWeaponLevel()
    {
        SwordUpgradeCost.text = Game.instance.weaponArsenal[Constants.weapon_sword]
                                    .upgradeCost.ToString();
        SwordLevel.text = GameData.upgradeLevelSword.ToString();

        WhipUpgradeCost.text = Game.instance.weaponArsenal[Constants.weapon_whip]
                                   .upgradeCost.ToString();
        WhipLevel.text = GameData.upgradeLevelWhip.ToString();

        CrossbowUpgradeCost.text = Game.instance.weaponArsenal[Constants.weapon_crossbow]
                                       .upgradeCost.ToString();
        CrossbowLevel.text = GameData.upgradeLevelCrossbow.ToString();
    }

    public void UpdateBootsLevel()
    {
        BootsUpgradeCost.text = Boots.upgradeCost.ToString();
        BootsLevel.text = GameData.upgradeLevelBoots.ToString();
    }

    public void UpdateRank()
    {
        RankText.text = GameData.rank.ToString();
    }

    public void ToggleSound()
    {
        GameData.settingSound = SoundToggleBtn.isOn;

        if (GameData.settingSound)
            AudioListener.volume = 1;
        else
            AudioListener.volume = 0;
    }

    public void equipWeapon(string weapon_id)
    {
        GameData.equippedWeapon = weapon_id;

        if (weapon_id == Constants.weapon_sword)
        {
            SwordToggleBtnObject.SetActive(false);
            WhipToggleBtnObject.SetActive(true);
            CrossbowToggleBtnObject.SetActive(true);

            SwordUpgradeBtnObject.SetActive(true);
            WhipUpgradeBtnObject.SetActive(false);
            CrossbowUpgradeBtnObject.SetActive(false);
        }
        else if (weapon_id == Constants.weapon_whip)
        {
            SwordToggleBtnObject.SetActive(true);
            WhipToggleBtnObject.SetActive(false);
            CrossbowToggleBtnObject.SetActive(true);

            SwordUpgradeBtnObject.SetActive(false);
            WhipUpgradeBtnObject.SetActive(true);
            CrossbowUpgradeBtnObject.SetActive(false);
        }
        else if (weapon_id == Constants.weapon_crossbow)
        {
            SwordToggleBtnObject.SetActive(true);
            WhipToggleBtnObject.SetActive(true);
            CrossbowToggleBtnObject.SetActive(false);

            SwordUpgradeBtnObject.SetActive(false);
            WhipUpgradeBtnObject.SetActive(false);
            CrossbowUpgradeBtnObject.SetActive(true);
        }
    }

    public void upgradeWeapon(string weapon_id)
    {
        Game.instance.GetEquippedWeapon().Upgrade();
        GUIMoney.instance.UpdateGold();
    }

    public void upgradeBoots()
    {
        Boots.Upgrade();
        GUIMoney.instance.UpdateGold();
    }
}
