[System.Serializable]
public class Effect : System.Object
{
    /// <summary>
    /// The type of effect that will trigger when it fires
    /// </summary>
    public EffectType type;

    /// <summary>
    /// Mostly used for how 'strong' the effect is (not used for all effects)
    /// </summary>
    public float coefficient;

    /// <summary>
    /// How long the effect lasts
    /// </summary>
    public float duration;
}