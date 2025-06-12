using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L4GameManager : MonoBehaviour
{
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject endLevelScreen;
    public Button prev_button;
    public Button start_button;
    public Button restart_button;
    public Button restartGO_button;
    public Button next_button;
    // Start is called before the first frame update
    void Start()
    {
        prev_button.onClick.AddListener(PrevGame);
        start_button.onClick.AddListener(StartGame);
        restart_button.onClick.AddListener(RestartGame);
        restartGO_button.onClick.AddListener(RestartGame);
        next_button.onClick.AddListener(NextGame);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
    }
    public void EndLevel()
    {
        isGameActive = false;
        endLevelScreen.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextGame()
    {
        SceneManager.LoadScene("LevelFive");
    }
    public void PrevGame()
    {
        SceneManager.LoadScene("LevelThree");
    }
}
