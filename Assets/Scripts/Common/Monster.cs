using UnityEngine;

[System.Serializable]
public class Monster : System.Object 
{
    /// <summary>
    /// The name of the monster
    /// </summary>
    public string name;

    /// <summary>
    /// Animator object corresponding to this specific monster
    /// </summary>
    public RuntimeAnimatorController anim;    

    /// <summary>
    /// Environment where this monster belongs to
    /// </summary>
    public Environment environment;

    public float GetMaxHealth()
    {
        var maxHealth = 6 * Mathf.Pow(1.075f, (float)GameData.rank);
        return maxHealth;
    }

    public float GetSpeed()
    {
        float speed = 3.6f * Mathf.Pow(1.0196f, (float)GameData.rank);
        return speed;
    }

    public int GetGold()
    {
        var reward =  Mathf.RoundToInt(10 * (Mathf.Log(GameData.rank + 1)));
        return reward;
    }
}
