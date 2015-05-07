using UnityEngine;
using System.Collections;

public class GameEvents : MonoBehaviour 
{
    [SerializeField] public Effect effectPrimary;
    [SerializeField] public Effect effectSecondary;
    public bool activateSecondaryEffect;
    public bool destroyOnTrigger; // Whether or not to destroy the object after it has triggered

    private bool triggered = false; // Variable to make sure event is only triggered once.
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // When in contact with player
        if (other.tag == "Player" && !triggered)
        {   
            if (!activateSecondaryEffect)
            {
                PlayerEvents(effectPrimary);
            }
            else
            {
                PlayerEvents(effectSecondary);
            }
            
        }
        else if (other.tag == "Frankenstein" && !triggered)
        {
            if (!activateSecondaryEffect)
            {
                FrankensteinEvents(effectPrimary);
            }
            else
            {
                FrankensteinEvents(effectSecondary);
            }
        }
    }

    void PlayerEvents(Effect effect)
    {
        if (effect.type == EffectType.Speed)
        {
            // Speed up/down depending on whether it's a trap/powerup
            Character2D.instance.SetSpeed(effect.coefficient, effect.duration);
            triggered = true;
            if (destroyOnTrigger) Destroy(gameObject);
        }
        else if (effect.type == EffectType.Death)
        {
            // Game Over
            NavigationManager.instance.GameOver();
            triggered = true;
            if (destroyOnTrigger) Destroy(gameObject);
        }
        else if (effect.type == EffectType.GraveDebuff)
        {
            // Apply or Add or Replenish debuff
            Character2D.instance.AddDebuff();
            triggered = true;
            if (destroyOnTrigger) Destroy(gameObject);
        }
    }

    void FrankensteinEvents(Effect effect)
    {
        if (effect.type == EffectType.JumpFromLeft &&
            Frankenstein.instance.facingRight)
        {
            Frankenstein.instance.jump = true;
            triggered = true;
            if (destroyOnTrigger) Destroy(gameObject);
        }
        else if (effect.type == EffectType.JumpFromRight &&
                 Frankenstein.instance.facingRight)
        {
            Frankenstein.instance.jump = true;
            triggered = true;
            if (destroyOnTrigger) Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class Effect : System.Object
{
    public EffectType type;
    public float coefficient;
    public float duration;
}