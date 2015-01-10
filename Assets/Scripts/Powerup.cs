using UnityEngine;
using System.Collections;

namespace UnitySampleAssets._2D
{
    public class Powerup : MonoBehaviour
    {

        public float powerupSpeed;
        public float powerupDuration;

        void OnTriggerEnter2D(Collider2D other)
        {
            // If touches player, game over
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlatformerCharacter2D>().Powerup(powerupSpeed,powerupDuration);
                Debug.Log("Triggered");
                Destroy(gameObject);
            }


        }
    }
}
