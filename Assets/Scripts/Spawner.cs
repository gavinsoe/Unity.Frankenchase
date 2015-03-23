using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public static Spawner instance;

    public GameObject[] obj;

    void Awake()
    {
        instance = this;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("SectionEnd"))
        {
            // Get spawn position
            var spawnPosition = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
            Instantiate(obj[Random.Range(0, obj.Length)], spawnPosition, Quaternion.identity);
        }
    }

	// Use this for initialization
	void Start () {
        //Spawn();
	}

    /*
    public void Spawn()
    {
        Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }

    public void CancelSpawn()
    {
        CancelInvoke();
    }
     */
}
