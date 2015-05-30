using UnityEngine;
using System.Collections;

public class CreepSpawner : MonoBehaviour 
{
    [SerializeField] private GameObject batTop;
    [SerializeField] private GameObject batMid;
    [SerializeField] private GameObject batBot;
    [SerializeField] private GameObject witchTop;
    [SerializeField] private GameObject witchMid;
    [SerializeField] private GameObject witchBot;
    [SerializeField] private GameObject ram;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("CreepBatTop"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var bat = ObjectPool.instance.GetObjectForType(batTop.name, false);
            bat.transform.parent = Camera.main.transform;
            bat.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepBatMid"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var bat = ObjectPool.instance.GetObjectForType(batMid.name, false);
            bat.transform.parent = Camera.main.transform;
            bat.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepBatBot"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var bat = ObjectPool.instance.GetObjectForType(batBot.name, false);
            bat.transform.parent = Camera.main.transform;
            bat.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepWitchTop"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var witch = ObjectPool.instance.GetObjectForType(witchTop.name, false);
            witch.transform.parent = Camera.main.transform;
            witch.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepWitchMid"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var witch = ObjectPool.instance.GetObjectForType(witchMid.name, false);
            witch.transform.parent = Camera.main.transform;
            witch.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepWitchBot"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var witch = ObjectPool.instance.GetObjectForType(witchBot.name, false);
            witch.transform.parent = Camera.main.transform;
            witch.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("CreepRam"))
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(0, 0, 10);
            var spawn = ObjectPool.instance.GetObjectForType(ram.name, false);
            spawn.transform.parent = Camera.main.transform;
            spawn.transform.localPosition = spawnPosition;

            // Destroy trigger to prevent event from triggering multiple times
            Destroy(other.gameObject);
        }
    }
}
