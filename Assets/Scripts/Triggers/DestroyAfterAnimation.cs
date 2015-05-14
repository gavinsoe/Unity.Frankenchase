using UnityEngine;
using System.Collections;

// This script simply destroys an object when their animation state is "Destroy" which 
// signifies that the animation has eneded.
public class DestroyAfterAnimation : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
        {
            Destroy(gameObject);
        }
	}
}
