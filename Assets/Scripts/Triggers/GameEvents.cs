using UnityEngine;
using System.Collections;

public class GameEvents : MonoBehaviour 
{
    [SerializeField] public Effect effectPrimary;
    [SerializeField] public Effect effectSecondary;
    public bool activateSecondaryEffect;
    public bool destroyOnTrigger; // Whether or not to destroy the object after it has triggered
    public bool triggered = false; // Variable to make sure event is only triggered once.
    
    void OnEnable()
    {
        triggered = false;
    }

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
    }

    void OnTriggerStay2D(Collider2D other)
    {
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
    }

    void PlayerEvents(Effect effect)
    {
        if (effect.type == EffectType.Slow)
        {
            // Speed up/down depending on whether it's a trap/powerup
            Character2D.instance.Slow(effect.coefficient, effect.duration);
            triggered = true;
            if (destroyOnTrigger) Destroy(gameObject);
        }
        else if (effect.type == EffectType.Stun)
        {
            // Game Over
            Character2D.instance.Stun(effect.duration);
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
        else if (effect.type == EffectType.Attack)
        {
            Character2D.instance.Attack(gameObject);
        }
    }

    public void ResetTrigger()
    {
        triggered = false;
    }
}
