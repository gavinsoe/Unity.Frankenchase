using UnityEngine;
using Soomla.Store;

[System.Serializable]
public class Weapon : System.Object
{
    // Initialiser
    public Weapon(string id)
    {
        weaponID = id;
        Update();
    }

    // ID corresponding to the weapon (in soomla)
    public string weaponID { get; set; }

    // Name of the weapon
    public WeaponType type;

    // Damage the weapon does
    public int damage { get; set; }

    // Time between attacks (or aim time for range weapons)
    public float cooldown { get; set; }

    // Function to update the stats of the weapon
    public void Update()
    {
        if (weaponID == Constants.weapon_sword)
        {
            // Retrieve upgrade level 
            damage = 10 + (10 * GameData.upgradeLevelSword);
            type = WeaponType.Sword;
            cooldown = 0.75f;
        }
        else if (weaponID == Constants.weapon_whip)
        {
            // Retrieve upgrade level
            damage = 10 + (10 * GameData.upgradeLevelWhip);
            type = WeaponType.Whip;
            cooldown = 2;
        }
        else if (weaponID == Constants.weapon_crossbow)
        {
            // Retreive upgrade level
            damage = 10 + (10 * GameData.upgradeLevelWhip);
            type = WeaponType.Crossbow;
            cooldown = 3;
        }

    }
}
