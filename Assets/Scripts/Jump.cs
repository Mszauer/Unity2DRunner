using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

    public float jumpSpeed = 240.0f;
    private Rigidbody2D body2D;
    private InputState inputState;
    public float forwardSpeed = 20.0f;

    void Awake() {
        body2D = GetComponent<Rigidbody2D>();
        inputState = GetComponent<InputState>();
    }
	// Update is called once per frame
	void Update () {
        if (inputState.standing) {
            if (inputState.actionButton) {
                body2D.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
            }
        }
	}
    void OnCollisionExit2D(Collision2D collision) {
        if((collision.gameObject.GetComponent<Obstacles>() != null || collision.gameObject.GetComponent<ObstacleTag>() != null)) {
            if (body2D.position.x < collision.gameObject.transform.position.x && !inputState.standing) {
                Vector2 newVel = body2D.velocity;
                newVel.x = forwardSpeed;
                body2D.velocity = newVel;
            }
        }
    }

}
