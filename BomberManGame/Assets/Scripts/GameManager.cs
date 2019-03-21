using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public sealed class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameObject WinScreen, DrawScreen;
    bool screenShown = false;
    public Text[] scoreTexts;
    void Start () {
        SetScoers ();
    }
    void SetScoers () {
        scoreTexts[0].text = DataSaver.scores[0].ToString ();
        scoreTexts[1].text = DataSaver.scores[1].ToString ();
        scoreTexts[2].text = DataSaver.scores[2].ToString ();
        scoreTexts[3].text = DataSaver.scores[3].ToString ();

    }
    void OnEnable () {
        EventManager.OnPlayerDeath += StartCheckingGameOver;
    }

    void OnDisable () {
        EventManager.OnPlayerDeath -= StartCheckingGameOver;
    }
    void Awake () {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {

    }
    public void what () {

    }
    public void RestartGame () {
        SceneManager.LoadScene ("SampleScene");
    }

    public IEnumerator CheckForGameOver () {
        yield return new WaitForSeconds (0.3f);
        Component[] players;

        players = FindObjectsOfType<PlayerMovement> ();
        int count = 0;
        foreach (PlayerMovement player in players) {
            count++;
        }
        Debug.LogError ("SHISHIR" + count);
        if (count == 1 && !screenShown) {
            foreach (PlayerMovement player in players) {
                player.alreadyDead = true;
            }
            //win
            WinScreen.SetActive (true);
            WinScreen.transform.GetChild (2).gameObject.GetComponent<Text> ().text = "Player " + (((PlayerMovement) players[0]).playerId ) + " Won.";
            Debug.Log ("Adding to " + DataSaver.scores[((PlayerMovement) players[0]).playerId - 1]);
            DataSaver.scores[((PlayerMovement) players[0]).playerId - 1] += 1;
            screenShown = true;

        }
        if (count == 0 && !screenShown) {
            //draw
            WinScreen.SetActive (false);
            DrawScreen.SetActive (true);
            screenShown = true;
        }
    }

    public void ShowDrawScreen () {
        DrawScreen.SetActive (true);
        Component[] players;

        players = FindObjectsOfType<PlayerMovement> ();
        int count = 0;
        foreach (PlayerMovement player in players) {
            player.alreadyDead = true;
        }
    }

    public void StartCheckingGameOver () {
        StartCoroutine (CheckForGameOver ());
    }
}