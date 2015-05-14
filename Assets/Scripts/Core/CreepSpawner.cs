using UnityEngine;
using System.Collections;

public class CreepSpawner : MonoBehaviour 
{
    public GameObject batTop;
    public GameObject batMid;
    public GameObject batBot;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("CreepBatTop"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var bat = Instantiate(batTop);
            bat.transform.parent = Camera.main.transform;
            bat.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepBatMid"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var bat = Instantiate(batMid);
            bat.transform.parent = Camera.main.transform;
            bat.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepBatBot"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var bat = Instantiate(batBot);
            bat.transform.parent = Camera.main.transform;
            bat.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
    }
}
