using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public GameObject gameTitleText;
    public GameObject playText;
    public GameObject deadText;
    public GameObject scoreText;
    public GameObject scoreValueText;
    public GameObject playAgainText;

    public TargetSpawner spawner;

    public GameObject playerPrefab;
    public CameraController cam;

    void Start() {
        gameTitleText.SetActive(true);
        playText.SetActive(true);
    }

    public void StartGame() {
        gameTitleText.SetActive(false);
        playText.SetActive(false);
        GameObject go = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        go.GetComponent<Player>().game = this;
        cam.player = go.transform;
        spawner.DeleteTargets();
        spawner.spawningActive = true;
    }

    public void Gameover(int score) {
        deadText.SetActive(true);
        scoreText.SetActive(true);
        scoreValueText.GetComponent<Text>().text = score.ToString();
        scoreValueText.SetActive(true);
        playAgainText.SetActive(true);
        spawner.spawningActive = false;
    }

    public void RestartGame() {
        deadText.SetActive(false);
        scoreText.SetActive(false);
        scoreValueText.SetActive(false);
        playAgainText.SetActive(false);
        StartGame();
    }
}
