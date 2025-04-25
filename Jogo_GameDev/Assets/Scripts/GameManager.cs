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
    private int count;
    public TextMeshProUGUI scoreText;
    public GameObject robot;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject streakCounter;
    public GameObject gameOverScreen;
    public GameObject[ ] lixoPrefabs;
    Vector3 spawnpos;
    private GameManager gameManager;
    public Button start_button;
    public Button restart_button;
    private float spawnInterval = 1.5f;
    private AudioSource playerAudio;
    public AudioClip craftSound;
    public AudioClip startSound;
    public AudioClip overSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        start_button.onClick.AddListener(StartGame);
        restart_button.onClick.AddListener(RestartGame);
        score = 0;
        streak = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame(){
        titleScreen.gameObject.SetActive(false);
        streakCounter.gameObject.SetActive(true);
        isGameActive = true;
        StartCoroutine(SpawnRandom());
        playerAudio.PlayOneShot(startSound, 1.0f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        streakCounter.gameObject.SetActive(false);
        isGameActive = false;
        playerAudio.PlayOneShot(overSound, 1.0f);
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
                    spawnpos = new Vector3((float)0.627, 2, (float)-8.75);
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
        scoreText.text = "Streak:" + streak;

        if (score != 0 && score % 4 == 0){
            count += 1;
            robot.transform.Find("part" + count).gameObject.SetActive(true);
            playerAudio.PlayOneShot(craftSound, 1.0f);
            if (score == 56){
                GameOver();
            }
        }
    }

    public int GetStreak()
    {
        return streak;
    }
}
