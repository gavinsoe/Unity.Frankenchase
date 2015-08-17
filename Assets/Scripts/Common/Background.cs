using UnityEngine;

[System.Serializable]
public class Background : System.Object
{
    /// <summary>
    /// The environment this background corresponds to
    /// </summary>
    public Environment environment;

    /// <summary>
    /// The material shader for the environment
    /// </summary>
    public Material material;

    /// <summary>
    ///  Multiplier to determine the direction and speed of movement of the background
    /// </summary>
    public float movementMultiplier;

}
