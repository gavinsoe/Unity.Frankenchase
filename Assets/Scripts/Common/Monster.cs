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

}
