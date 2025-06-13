using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L5GameManager : MonoBehaviour
{
    public bool isGameActive;
    private int score = 0;
    public L5PlayerController playerController;
    public FollowPlayer enemy;
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject endLevelScreen;
    public Button prev_button;
    public Button start_button;
    public Button restart_button;
    public Button restartGO_button;
    public Button next_button;
    public GameObject bag;
    private Vector3 spawnpos;
    private float spawnInterval = 3.0f;
    private float xRange = 20.7f;
    private float zRange = 8.6f;
    private AudioSource playerAudio;
    public AudioClip goodSound;
    public AudioClip startSound;
    public AudioClip overSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
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
        scoreText.gameObject.SetActive(true);
        isGameActive = true;
        StartCoroutine(SpawnRandom());
        playerAudio.PlayOneShot(startSound, 1.0f);
    }
    public IEnumerator GameOver()
    {
        isGameActive = false;
        yield return new WaitForSeconds(1.0f);
        gameOverScreen.gameObject.SetActive(true);
    }
    public void EndLevel()
    {
        playerController.StopRun();
        isGameActive = false;
        endLevelScreen.gameObject.SetActive(true);
        playerAudio.PlayOneShot(overSound, 1.0f);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextGame()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void PrevGame()
    {
        SceneManager.LoadScene("LevelFour");
    }
    IEnumerator SpawnRandom()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnInterval);
            // Define uma posicao aleatoria de Spawn e instancia o Spawn:
            spawnpos = new Vector3(Random.Range(-xRange, xRange), 4, Random.Range(-zRange, zRange));
            Instantiate(bag, spawnpos, bag.transform.rotation);
        }
    }
    public void UpdateScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
        playerAudio.PlayOneShot(goodSound, 1.0f);
        if (score == 10)
        {
            EndLevel();
        }
        enemy.moveSpeed += 0.1f;
    }
}
