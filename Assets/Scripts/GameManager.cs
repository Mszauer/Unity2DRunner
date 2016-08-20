using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
    private TimeManager timeManager;
    private bool gameStarted;
    private float blinkTime = 0f;
    private bool blinkingText;
    private float timeElapsed = 0f;
    private float bestTime = 0f;

    public Text continueText;
    public GameObject playerPrefab;
    public Text scoreText;

    private GameObject player;
    private GameObject floor;
    private Spawner spawner;
    private bool beatBestTime;

    void Awake() {
        //set floor
        floor = GameObject.Find("Foreground");
        //set spawner
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        //
        timeManager = GetComponent<TimeManager>();
    }
    void Start() {
        //set floor height to bottom of screen
        float floorHeight = transform.localPosition.y;
        Vector3 pos = floor.transform.position;
        pos.x = 0;
        pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight/2);
        floor.transform.position = pos;

        //turn off spawner temporarily
        spawner.active = false;
        //begin instantiation
        Time.timeScale = 0f;

        continueText.text = "PRESS ANY BUTTON TO START";
        bestTime = PlayerPrefs.GetFloat("BestTime");
    }
    void Update() {
        string textColor = beatBestTime ? "#FF0" : "#FFF";

        if (!gameStarted && Time.timeScale == 0) {
            if (Input.anyKeyDown) {
                timeManager.ManipulateTime(1.0f, 1.0f);
                ResetGame();
            }
        }
        if (!gameStarted) {
            blinkTime++;
            if (blinkTime % 40f == 0f) {
                blinkingText = !blinkingText;
            }

            continueText.canvasRenderer.SetAlpha(blinkingText ? 0 : 1);
            scoreText.text = "TIME: " + FormatTime(timeElapsed) + " \n <color="+textColor+">BEST: " + FormatTime(bestTime)+"</color>";
        }
        else {
            timeElapsed += Time.deltaTime;
            scoreText.text = "TIME: " + FormatTime(timeElapsed);
        }
    }

    void OnPlayerKilled() {
        spawner.active = false;
        //Get component for destroyOffscreen
        DestroyOffscreen playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        //unlink delegate for garbage caller
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;

        //reset velocity of player
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        timeManager.ManipulateTime(0f, 5.5f);
        gameStarted = false;

        continueText.text = "PRESS ANY BUTTON TO RESTART";

        if(timeElapsed > bestTime) {
            bestTime = timeElapsed;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            beatBestTime = true;
        }
    }

    void ResetGame() {
        spawner.active = true;

        //create player and set spawn height
        player = GameObjectUtility.Instantiate(playerPrefab, new Vector3(0f, (Screen.height / PixelPerfectCamera.pixelsToUnits) / 2 + 100.0f, 0f));
        //Get component for destroyOffscreen
        DestroyOffscreen playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        //hook up destroyOffscreen delegate to execute OnPlayerKilled when called
        playerDestroyScript.DestroyCallback += OnPlayerKilled;

        gameStarted = true;

        continueText.canvasRenderer.SetAlpha(0f);
        timeElapsed = 0f;
        beatBestTime = false;
    }

    string FormatTime(float time) {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        return minutes + ":" + seconds;
        /*
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
        */

    }
}
