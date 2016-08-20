using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {
    //set delegate to communicate with GameManager
    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    public float offset = 16f;
    private bool offscreen;
    private float offscreenX = 0f;
    private Rigidbody2D body2d;

    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        //figure out where offscreen is based on screen size
        offscreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;

	}
	
	// Update is called once per frame
	void Update () {
        //find the current position and direction
        float posX = transform.position.x;
        float dirX = body2d.velocity.x;

        //if offscreen (any dir)
        if (Mathf.Abs(posX) > offscreenX) {
            if (dirX < 0 && posX < -offscreenX) { //if offscreen to left
                offscreen = true;
            }
            else if (dirX > 0 && posX > offscreenX) {//if offscreen to right
                offscreen = true;
            }
        }
        else {
            offscreen = false;
        }
        if (offscreen) {
            //if offscreen, call apply outofbounds logic
            OnOutOfBounds();
        }
	}
    public void OnOutOfBounds() {
        //reset offscreen bool
        offscreen = false;
        //destroy gameobject
        GameObjectUtility.Destroy(gameObject);
        //if delegate exists, call delegate logic
        if (DestroyCallback != null) {
            DestroyCallback();
        }
    }
}
