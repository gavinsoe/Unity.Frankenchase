using UnityEngine;
using System.Collections;

public class GameEvents : MonoBehaviour 
{
    public enum Effects { Speed, Death }

    public Effects effect;
    public float effectCoefficient;
    public float effectDuration;

    void OnTriggerEnter2D(Collider2D other)
    {
        // When in contact with player
        if (other.tag == "Player")
        {
            Debug.Log("Triggered!!");
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
            }
        }
    }
}
