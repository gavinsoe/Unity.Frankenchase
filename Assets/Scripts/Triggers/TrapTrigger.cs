using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour {
    public Animator animator;

    private bool triggered = false; // Variable to make sure event is only triggered once.

    void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
    }

    void OnDisable()
    {
        animator.SetTrigger("Reset");
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster" && !triggered)
        {
            animator.SetTrigger("Trigger");
        }
    }
}
