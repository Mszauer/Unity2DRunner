using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

    private Animator animator;
    private InputState inputState;


	void Awake () {
        animator = GetComponent<Animator>();
        inputState = GetComponent<InputState>();
	}
	
	// Update is called once per frame
	void Update () {

        bool running = true;

        if (inputState.absValX > 0 && inputState.absValY < inputState.standingThreshold) {
            running = false;
        }

        animator.SetBool("Running", running);

	}
}
