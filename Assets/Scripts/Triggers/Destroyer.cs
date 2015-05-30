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
        
        if (other.transform.root.tag.Equals("StageSection"))
        {
            Game.instance.ReturnSection(other.transform.root.name);
        }

        // Pool the component.
        ObjectPool.instance.PoolObject(other.transform.root.gameObject);
    }
}
