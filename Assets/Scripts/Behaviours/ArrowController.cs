using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

    public float speed = 2.0f;
    private float startTime;
    private float journeyLength;
    private bool triggered;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, 
                        MonsterController.instance.transform.position);
        triggered = false;
    }

	// Update is called once per frame
	void Update () 
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, 
                                          MonsterController.instance.transform.position,fracJourney);
	}

    void OnCollision2D(Collision2D other)
    {
        Debug.Log("Collided with " + other.gameObject.tag);
        if (other.gameObject.tag.Equals("Monster"))
        {

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with " + other.gameObject.tag);
        if (other.gameObject.tag.Equals("Monster") && !triggered)
        {
            MonsterController.instance.TakeDamage();
            triggered = true;
            Invoke("DestroySelf", 1.0f);
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
