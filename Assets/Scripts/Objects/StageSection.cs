using UnityEngine;

[System.Serializable]
public class StageSection : System.Object
{
    /// <summary>
    /// Holds the prefab that will be spawned
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// Location where this section will start to spawn
    /// </summary>
    public float startDistance;

    /// <summary>
    /// Location where this section will stop spawning
    /// </summary>
    public float endDistance;
}