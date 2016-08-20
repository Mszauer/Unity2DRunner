using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour {

    public Vector2 speed = Vector2.zero;
    private Vector2 offset = Vector2.zero;

    private Material material;

	// Use this for initialization
	void Start () {
        //find material and set variable
        material = GetComponent<Renderer>().material;
        //correctly make the size
        offset = material.GetTextureOffset("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {
        //apply movement
        offset += speed * Time.deltaTime;
        //visuall apply texture offset
        material.SetTextureOffset("_MainTex", offset);
	}
}
