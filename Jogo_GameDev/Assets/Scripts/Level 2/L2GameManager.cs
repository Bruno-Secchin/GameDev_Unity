using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L2GameManager : MonoBehaviour
{
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    private L2GameManager gameManager;
    public Button start_button;
    public Button restart_button;
    public Button next_button;
    // Start is called before the first frame update
    void Start()
    {
        start_button.onClick.AddListener(StartGame);
        restart_button.onClick.AddListener(RestartGame);
        next_button.onClick.AddListener(NextGame);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void StartGame(){
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
    }
    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextGame()
    {
        SceneManager.LoadScene("LevelThree");
    }
}
