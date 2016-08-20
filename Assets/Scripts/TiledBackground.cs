using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour {

    public int textureSize = 32; // optimized for powers of 2
    public bool scaleHorizontally = true;
    public bool scaleVertically = true;
    
    void Start() {
        //ceil fills in gaps left by partial amounts
        var newWidth = !scaleHorizontally ? 1 : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
        var newHeight = !scaleVertically ? 1 : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));

        //resize the "background" gameobject
        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);

        //resize the material/texture
        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);

    }
}