using UnityEngine;
using System.Collections;

public class GameEvents : MonoBehaviour 
{
    public enum Effects { Speed, Death, GraveDebuff, JumpFromLeft, JumpFromRight }

    public Effects effect;
    public float effectCoefficient;
    public float effectDuration;

    private bool triggered = false; // Variable to make sure event is only triggered once.

    void OnTriggerEnter2D(Collider2D other)
    {
        // When in contact with player
        if (other.tag == "Player" && !triggered)
        {
            if (effect == Effects.Speed)
            {
                // Speed up/down depending on whether it's a trap/powerup
                //Character2D.instance.SetSpeed(effectCoefficient, effectDuration);
                triggered = true;
                Destroy(gameObject);
            }
            else if (effect == Effects.Death)
            {
                // Game Over
                NavigationManager.instance.GameOver();
                triggered = true;
            }
            else if (effect == Effects.GraveDebuff)
            {
                // Apply or Add or Replenish debuff
                Character2D.instance.AddDebuff();
                triggered = true;
            }
        }
        else if (other.tag == "Frankenstein" && !triggered)
        {
            if (effect == Effects.JumpFromLeft &&
                Frankenstein.instance.facingRight)
            {
                Frankenstein.instance.jump = true;
                triggered = true;
                Destroy(gameObject);
            }
            else if (effect == Effects.JumpFromRight &&
                     Frankenstein.instance.facingRight)
            {
                Frankenstein.instance.jump = true;
                triggered = true;
                Destroy(gameObject);
            }

        }
    }
}
