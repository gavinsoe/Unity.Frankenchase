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

    // Whether this weapon is currently equipped
    public bool equipped { 
        get
        {
            return StoreInventory.IsVirtualGoodEquipped(weaponID);
        }
        set
        {
            if (value == true)
            {
                StoreInventory.EquipVirtualGood(weaponID);
            } 
            else
            {
                StoreInventory.UnEquipVirtualGood(weaponID);
            }
        } 
    }

    // Name of the weapon
    public WeaponType type;

    // Damage the weapon does
    public int damage { get; set; }

    // Time between attacks (or aim time for range weapons)
    public float cooldown { get; set; }

    // Function to update the stats of the weapon
    public void Update()
    {
        var upgradeLevel = 0;
        if (weaponID == StoreAssets.WEAPON_SWORD_ID)
        {
            // Retrieve upgrade level 
            upgradeLevel = StoreInventory.GetItemBalance(StoreAssets.UPGRADE_SWORD_ID);
            type = WeaponType.Sword;
            cooldown = 0;
        }
        else if (weaponID == StoreAssets.WEAPON_WHIP_ID)
        {
            // Retrieve upgrade level
            upgradeLevel = StoreInventory.GetItemBalance(StoreAssets.UPGRADE_WHIP_ID);
            type = WeaponType.Whip;
            cooldown = 1;
        }
        else if (weaponID == StoreAssets.WEAPON_CROSSBOW_ID)
        {
            // Retreive upgrade level
            upgradeLevel = StoreInventory.GetItemBalance(StoreAssets.UPGRADE_CROSSBOW_ID);
            type = WeaponType.Crossbow;
            cooldown = 3;
        }

        // Update the damage
        damage = 10 + (10 * upgradeLevel);
    }
}
