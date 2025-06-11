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
    public GameObject endLevelScreen;
    public GameObject timerText;
    private L2GameManager gameManager;
    public Button prev_button;
    public Button start_button;
    public Button restart_button;
    public Button restartGO_button;
    public Button next_button;
    private GameObject mainCamera;
    private Vector3 cameraPosition;
    private bool moveCamera = false;
    private Vector3 spawnposEco;
    private Vector3 spawnposPipe;
    private float spawnInterval = 1.5f;
    public GameObject[ ] obstaclePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        spawnposEco = new Vector3(0f, 4.1f, 75f);
        spawnposPipe = new Vector3(0f, 7.25f, 75f);
        prev_button.onClick.AddListener(PrevGame);
        start_button.onClick.AddListener(StartGame);
        restart_button.onClick.AddListener(RestartGame);
        restartGO_button.onClick.AddListener(RestartGame);
        next_button.onClick.AddListener(NextGame);
        mainCamera = GameObject.Find("Main Camera");
        cameraPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, cameraPosition, 5.0f * Time.deltaTime);
            if (Vector3.Distance(mainCamera.transform.position, cameraPosition) < 0.01f)
            {
                moveCamera = false;
            }
        }
    }

    public void StartGame()
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        timerText.gameObject.SetActive(true);
        StartCoroutine(SpawnRandom());
    }
    public IEnumerator GameOver()
    {
        isGameActive = false;
        cameraPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10.5f);
        moveCamera = true;
        yield return new WaitForSeconds(1.0f);
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
        SceneManager.LoadScene("LevelThree");
    }
    public void PrevGame()
    {
        SceneManager.LoadScene("LevelOne");
    }

    IEnumerator SpawnRandom()
    {
        while(isGameActive){
            yield return new WaitForSeconds(spawnInterval);
            // Define uma posicao aleatoria de Spawn e instancia o Spawn:
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            switch(obstacleIndex){
                case 0:
                    Instantiate(obstaclePrefabs[obstacleIndex], spawnposEco, obstaclePrefabs[obstacleIndex].transform.rotation);
                    break;
                case 1:
                    Instantiate(obstaclePrefabs[obstacleIndex], spawnposEco, obstaclePrefabs[obstacleIndex].transform.rotation);
                    break;
                case 2:
                    Instantiate(obstaclePrefabs[obstacleIndex], spawnposEco, obstaclePrefabs[obstacleIndex].transform.rotation);
                    break;
                case 3:
                    Instantiate(obstaclePrefabs[obstacleIndex], spawnposEco, obstaclePrefabs[obstacleIndex].transform.rotation);
                    break;
                case 4:
                    Instantiate(obstaclePrefabs[obstacleIndex], spawnposPipe, obstaclePrefabs[obstacleIndex].transform.rotation);
                    break;
                case 5:
                    Instantiate(obstaclePrefabs[obstacleIndex], spawnposPipe, obstaclePrefabs[obstacleIndex].transform.rotation);
                    break;
            }
        }
    }
}
