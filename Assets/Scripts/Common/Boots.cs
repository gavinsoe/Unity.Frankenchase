using UnityEngine;
using System.Collections;

[System.Serializable]
public static class Boots : System.Object 
{
    /// <summary>
    /// Initializer
    /// </summary>
    static Boots()
    {
        Update();
    }

    // speed of the professor
    public static float speed { get; set; }

    // percentage reduction to status effects
    public static float resistance { get; set; }

    // cost to upgrade to the next level
    public static int upgradeCost { get; set; }

    private static void Update()
    {
        speed = 4 + ((GameData.upgradeLevelBoots - 1) * 0.315f);
        resistance = ((GameData.upgradeLevelBoots - 1) * 3) / 100;
        upgradeCost = Mathf.RoundToInt(50 * Mathf.Pow(1.215f, (float)GameData.upgradeLevelBoots));
    }

    public static void Upgrade()
    {
        if (GameData.money >= upgradeCost)
        {
            // Take money
            GameData.money -= upgradeCost;
            // Upgrade boots
            GameData.upgradeLevelBoots += 1;
        }

        Update();
    }

}
