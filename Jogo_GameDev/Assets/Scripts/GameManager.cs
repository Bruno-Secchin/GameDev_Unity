using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private int score;
    private int streak;
    public TextMeshProUGUI scoreText;
    public GameObject robot;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject[ ] lixoPrefabs;
    Vector3 spawnpos;
    private GameManager gameManager;
    public Button start_button;
    public Button restart_button;
    private float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        start_button.onClick.AddListener(StartGame);
        restart_button.onClick.AddListener(RestartGame);
        score = 0;
        streak = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score == 8)
        {
            robot.transform.Find("part1").gameObject.SetActive(true);
        }
        else if (score == 10)
        {
            robot.transform.Find("part2").gameObject.SetActive(true);
        }
        else if (score == 12)
        {
            robot.transform.Find("part3").gameObject.SetActive(true);
        }
        else if (score == 14)
        {
            robot.transform.Find("part4").gameObject.SetActive(true);
        }
        else if (score == 16)
        {
            robot.transform.Find("part5").gameObject.SetActive(true);
        }
        else if (score == 19)
        {
            robot.transform.Find("part6").gameObject.SetActive(true);
        }
        else if (score == 22)
        {
            robot.transform.Find("part7").gameObject.SetActive(true);
        }
        else if (score == 28)
        {
            robot.transform.Find("part8").gameObject.SetActive(true);
        }
        else if (score == 31)
        {
            robot.transform.Find("part9").gameObject.SetActive(true);
        }
        else if (score == 34)
        {
            robot.transform.Find("part10").gameObject.SetActive(true);
        }
        else if (score == 37)
        {
            robot.transform.Find("part11").gameObject.SetActive(true);
        }
        else if (score == 41)
        {
            robot.transform.Find("part12").gameObject.SetActive(true);
        }
        else if (score == 45)
        {
            robot.transform.Find("part13").gameObject.SetActive(true);
        }
        else if (score == 50)
        {
            robot.transform.Find("part14").gameObject.SetActive(true);
            GameOver();
        }

    }

    void StartGame(){
        titleScreen.gameObject.SetActive(false);
        restart_button.gameObject.SetActive(true);
        isGameActive = true;
        StartCoroutine(SpawnRandom());
        //InvokeRepeating("SpawnRandom", startDelay, spawnInterval);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
    }

    IEnumerator SpawnRandom()
    {
        while(isGameActive){
            yield return new WaitForSeconds(spawnInterval);
            // Define uma posicao aleatoria de Spawn e instancia o Spawn:
            int lixoIndex = Random.Range(0, lixoPrefabs.Length);
            switch(lixoIndex){
                case 0:
                    spawnpos = new Vector3((float)-0.048, 2, (float)-8.75);
                    Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                    break;
                case 1:
                    spawnpos = new Vector3((float)0.3, 2, (float)-8.75);
                    Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                    break;
                case 2:
                    spawnpos = new Vector3((float)0.597, 2, (float)-8.75);
                    Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                    break;
                case 3:
                    spawnpos = new Vector3((float)1, 2, (float)-8.75);
                    Instantiate(lixoPrefabs[lixoIndex], spawnpos, lixoPrefabs[lixoIndex].transform.rotation);
                    break;
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (scoreToAdd == 0){
            streak = 0;
        }
        else {
            score += scoreToAdd;
            streak += scoreToAdd;
        }
        scoreText.text = "Streak: " + streak;
    }

    public int GetStreak()
    {
        return streak;
    }
}
