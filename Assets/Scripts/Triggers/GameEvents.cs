using UnityEngine;
using System.Collections;

public class GameEvents : MonoBehaviour 
{
    public enum Effects { Speed, Death, JumpFromLeft, JumpFromRight }

    public Effects effect;
    public float effectCoefficient;
    public float effectDuration;

    void OnTriggerEnter2D(Collider2D other)
    {
        // When in contact with player
        if (other.tag == "Player")
        {
            if (effect == Effects.Speed)
            {
                // Speed up/down depending on whether it's a trap/powerup
                Character2D.instance.SetSpeed(effectCoefficient, effectDuration);
                Destroy(gameObject);
            }
            else if (effect == Effects.Death)
            {
                // Game Over
                Character2D.instance.Kill();
                Frankenstein.instance.Kill();
                NavigationManager.instance.ShowGameOverGUI();
            }
        }
        else if (other.tag == "Frankenstein")
        {
            if (effect == Effects.JumpFromLeft &&
                Frankenstein.instance.facingRight)
            {
                Frankenstein.instance.jump = true;
                Destroy(gameObject);
            }
            else if (effect == Effects.JumpFromRight &&
                     Frankenstein.instance.facingRight)
            {
                Frankenstein.instance.jump = true;
                Destroy(gameObject);
            }

        }
    }
}
