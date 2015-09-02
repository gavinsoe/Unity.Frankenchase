using UnityEngine;
using System.Collections;

[System.Serializable]
public static class Boots : System.Object 
{
    // speed of the professor
    public static float speed
    {
        get
        {
            return 4 + ((GameData.bootsLevel - 1) * 0.315f);
        }
    }

    // percentage reduction to status effects
    public static float resistance
    {
        get
        {
            return (GameData.bootsLevel - 1) * 3;
        }
    }

    // cost to upgrade to the next level
    public static int upgradeCost
    {
        get
        {
            return Mathf.RoundToInt(50 * Mathf.Pow(1.215f, (float)GameData.upgradeLevelWhip));
        }
    }

}
