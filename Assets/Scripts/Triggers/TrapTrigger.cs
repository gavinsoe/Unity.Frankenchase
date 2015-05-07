using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {
    private Animator animator;

    private bool triggered = false; // Variable to make sure event is only triggered once.

    void Start()
    {
        animator = gameObject.GetComponentInParent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter :: " + other.tag);
        if (other.tag == "Frankenstein" && !triggered)
        {
            animator.SetTrigger("Trigger");
        }
    }
}
