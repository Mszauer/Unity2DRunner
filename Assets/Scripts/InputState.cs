using UnityEngine;
using System.Collections;

public class InputState : MonoBehaviour {

    public bool actionButton;
    public float absValX = 0f;
    public float absValY = 0f;
    public bool standing;
    public float standingThreshold = 1.0f;

    private Rigidbody2D body2D;

    void Awake() {
        body2D = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {
        actionButton = Input.anyKeyDown;
	}

    // All physics calculations go here
    void FixedUpdate() {
        absValX = System.Math.Abs(body2D.velocity.x);
        absValY = System.Math.Abs(body2D.velocity.y);

        standing = absValY <= standingThreshold;
    }
}
