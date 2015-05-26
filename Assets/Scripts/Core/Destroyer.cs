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
        
        // If anything else, remove the component
        // If object has parent destroy parent
        if (other.gameObject.transform.parent)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            if (other.gameObject.transform.parent.tag == "StageSection")
            {
                Game.instance.ReturnSection(other.gameObject.transform.parent.name);
            }
        }
        else
        {
            Destroy(other.gameObject);
            if (other.gameObject.tag == "StageSection")
            {
                Game.instance.ReturnSection(other.gameObject.name);
            }
        }
    }
}
