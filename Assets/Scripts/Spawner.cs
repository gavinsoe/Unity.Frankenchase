using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public static Spawner instance;

    public GameObject[] obj;
    public float spawnMin = 1f;
    public float spawnMax = 2f;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        Spawn();
	}

    public void Spawn()
    {
        Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }

    public void CancelSpawn()
    {
        CancelInvoke();
    }
}
