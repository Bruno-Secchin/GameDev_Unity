using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L3GameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool isGameActive;

    [Header("UI Screens")]
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject endLevelScreen;

    [Header("Buttons")]
    public Button prevButton;
    public Button startButton;
    public Button restartButton;
    public Button restartGOButton;
    public Button nextButton;

    [Header("Camera Settings")]
    public float cameraMoveSpeed = 5.0f;
    private GameObject mainCamera;
    private Vector3 cameraPosition;
    private bool moveCamera = false;

    [Header("Score System")]
    public int currentScore = 0;
    public TextMeshProUGUI scoreText; // Referência ao texto UI que mostra a pontuação


    // Singleton pattern para fácil acesso
    private static L3GameManager _instance;
    public static L3GameManager Instance => _instance;

    private void Awake()
    {
        // Implementação do singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        InitializeButtons();
        FindCamera();
        SetInitialGameState();
        UpdateScoreUI(); // Inicializa a UI de pontuação
    }

    private void InitializeButtons()
    {
        prevButton.onClick.AddListener(LoadPreviousLevel);
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        restartGOButton.onClick.AddListener(RestartGame);
        nextButton.onClick.AddListener(LoadNextLevel);
    }

    private void FindCamera()
    {
        mainCamera = Camera.main.gameObject;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }
        cameraPosition = mainCamera.transform.position;
    }

    private void SetInitialGameState()
    {
        isGameActive = false;
        titleScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        endLevelScreen.SetActive(false);
    }

    void Update()
    {
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        if (!moveCamera) return;

        mainCamera.transform.position = Vector3.MoveTowards(
            mainCamera.transform.position,
            cameraPosition,
            cameraMoveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(mainCamera.transform.position, cameraPosition) < 0.01f)
        {
            moveCamera = false;
        }
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        isGameActive = true;
        // Adicione qualquer inicialização adicional do jogo aqui
    }

    public IEnumerator GameOver()
    {
        isGameActive = false;

        // Configura movimento da câmera
        cameraPosition = new Vector3(
            mainCamera.transform.position.x,
            mainCamera.transform.position.y,
            -10.5f
        );
        moveCamera = true;

        yield return new WaitForSeconds(1.0f);
        gameOverScreen.SetActive(true);
    }

    public void EndLevel()
    {
        isGameActive = false;
        endLevelScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        // Carrega próxima cena no build settings
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next level available!");
            // Volta para o menu principal ou primeira cena
            SceneManager.LoadScene(0);
        }
    }

    public void LoadPreviousLevel()
    {
        // Carrega cena anterior no build settings
        int prevSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (prevSceneIndex >= 0)
        {
            SceneManager.LoadScene(prevSceneIndex);
        }
        else
        {
            Debug.LogWarning("No previous level available!");
            // Volta para o menu principal ou última cena
            SceneManager.LoadScene(0);
        }
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }
}