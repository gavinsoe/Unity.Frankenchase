using UnityEngine;

[System.Serializable]
public class Weapon : System.Object
{
    // Initialiser
    public Weapon(string id)
    {
        weaponID = id;
        Update();
    }

    // ID corresponding to the weapon
    public string weaponID { get; set; }

    // Name of the weapon
    public WeaponType type;

    // Damage the weapon does
    public float damage { get; set; }

    // Time between attacks (or aim time for range weapons)
    public float cooldown { get; set; }

    // Speed boost from weapon
    public float speedBoost { get; set; }

    // Upgrade cost
    public int upgradeCost { get; set; }

    // Function to update the stats of the weapon
    public void Update()
    {
        if (weaponID == Constants.weapon_sword)
        {
            type = WeaponType.Sword;
            
            // Calculate stats based on rank
            damage = 1 + (0.21f * (GameData.upgradeLevelSword - 1));
            speedBoost = 0.05f + (0.05f * (GameData.upgradeLevelSword - 1));
            cooldown = 1;
            upgradeCost = Mathf.RoundToInt(20 * Mathf.Pow(1.21f, (float)GameData.upgradeLevelSword));
        }
        else if (weaponID == Constants.weapon_whip)
        {
            type = WeaponType.Whip;

            // Calculate stats based on rank
            damage = 2 + (1.21f * (GameData.upgradeLevelWhip - 1));
            speedBoost = 0;
            cooldown = 2 - (0.05f * (GameData.upgradeLevelWhip - 1));
            upgradeCost = Mathf.RoundToInt(25 * Mathf.Pow(1.21f, (float)GameData.upgradeLevelWhip));
        }
        else if (weaponID == Constants.weapon_crossbow)
        {
            type = WeaponType.Crossbow;

            // Retreive upgrade level
            damage = 1.5f + (0.71f * (GameData.upgradeLevelCrossbow - 1));
            speedBoost = 0;
            cooldown = 5 - (0.5f * Mathf.CeilToInt((GameData.upgradeLevelCrossbow - 1) / 4));
            upgradeCost = Mathf.RoundToInt(30 * Mathf.Pow(1.21f, (float)GameData.upgradeLevelCrossbow));
        }

    }

    // Upgrades the weapon
    public void Upgrade()
    {
        // Check if sufficient funds are available
        if (GameData.money >= upgradeCost)
        {
            // Take money
            GameData.money -= upgradeCost;

            // Upgrade corresponding weaopn
            if (type == WeaponType.Sword)
            {
                GameData.upgradeLevelSword += 1;
            }
            else if (type == WeaponType.Whip)
            {
                GameData.upgradeLevelWhip += 1;
            }
            else if (type == WeaponType.Crossbow)
            {
                GameData.upgradeLevelCrossbow += 1;
            }

            // Update weapon stats
            Update();
        }
    }
}
