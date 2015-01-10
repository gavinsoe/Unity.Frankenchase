using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        // If touches player, game over
        if (other.tag == "Player")
        {
            Debug.Break();
            return;
        }

        
        // If it is the ground, relocate
        if (other.gameObject.transform.parent != null &&
            other.gameObject.transform.parent.name == "LargeGround")
        {
            other.transform.position = new Vector3(other.transform.position.x + 30, other.transform.position.y, other.transform.position.z);
            return;
        }

        // If anything else, remove the component
        // If object has parent destroy parent
        if (other.gameObject.transform.parent)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
