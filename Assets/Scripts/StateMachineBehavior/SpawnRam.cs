﻿using UnityEngine;
using System.Collections;

public class SpawnRam : StateMachineBehaviour {

    [SerializeField] private GameObject ram;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
	    // Spawn a Ram
        // Make sure no other Ram exists before spawning.
        if (Ram.instance == null)
        {
            // Set the spawn position (should be zero)
            var spawnPosition = new Vector3(
                    Character2D.instance.transform.position.x - 2,
                    Character2D.instance.transform.position.y,
                    Character2D.instance.transform.position.z
                );                
            var ram = ObjectPool.instance.GetObjectForType(this.ram.name, false);
            ram.transform.position = spawnPosition;
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
