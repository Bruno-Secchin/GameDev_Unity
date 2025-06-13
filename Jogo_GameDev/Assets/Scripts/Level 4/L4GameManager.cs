using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class L4GameManager : MonoBehaviour
{
    public static L4GameManager Instance;

    [Header("Game State")]
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject endLevelScreen;
    public Button prevButton;
    public Button startButton;
    public Button restartButton;
    public Button restartGOButton;
    public Button nextButton;
    private AudioSource playerAudio;
    public AudioClip startSound;
    public AudioClip overSound;
    private AudioSource gameAudio;

    [Header("Camera Settings")]
    public float cameraMoveSpeed = 5.0f;
    private GameObject mainCamera;
    private Vector3 cameraPosition;
    private bool moveCamera = false;

    [Header("Fishing Settings")]
    public int piecesToFish = 3;
    public float fishingZoneRadius = 5f;
    public Transform fishingSpot;
    public GameObject bobberPrefab;
    public AudioClip castSound;
    public AudioClip biteSound;
    public ParticleSystem biteParticle;

    private int piecesFished = 0;
    private bool isFishing = false;
    private GameObject currentBobber;
    private bool canReel = false;

    [Header("Victory Settings")]
    public GameObject victoryScreenPrefab;
    public AudioClip victorySound;

    
    public TextMeshProUGUI resultText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        InitializeButtons();
        FindCamera();
        SetInitialGameState();
    }

    private void SetInitialGameState()
    {
        isGameActive = false;
        titleScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        endLevelScreen.SetActive(false);
        resultText.gameObject.SetActive(false);        
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isFishing)
            {
                TryCastFishingRod();
            }
            else if (canReel)
            {
                StartFishingMinigame();
            }
        }
    }

    private void TryCastFishingRod()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(fishingSpot.position, hit.point) <= fishingZoneRadius)
            {
                StartFishing(hit.point);
            }
        }
    }

    private void StartFishing(Vector3 position)
    {
        isFishing = true;
        currentBobber = Instantiate(bobberPrefab, position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(castSound, position);

        // Começa a esperar pela fisgada
        StartCoroutine(WaitForBite());
    }

    private IEnumerator WaitForBite()
    {
        float waitTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(waitTime);

        // Peixe mordeu
        canReel = true;
        AudioSource.PlayClipAtPoint(biteSound, currentBobber.transform.position);
        biteParticle.transform.position = currentBobber.transform.position;
        biteParticle.Play();
    }

    private void StartFishingMinigame()
    {
        canReel = false;
        biteParticle.Stop();
        FishingMinigame.Instance.StartMinigame();
    }

    public void OnFishingSuccess()
    {
        piecesFished++;
        Destroy(currentBobber);
        isFishing = false;

        if (piecesFished >= piecesToFish)
        {
            EndLevel();
        }
    }

    public void OnFishingFailure()
    {
        Destroy(currentBobber);
        isFishing = false;
        canReel = false;
        // Pode adicionar feedback de falha aqui
    }

    public IEnumerator GameOver()
    {
        isGameActive = false;
        yield return new WaitForSeconds(1.0f);
        gameOverScreen.SetActive(true);
    }

    public void EndLevel()
    {
        isGameActive = false;
        gameAudio.PlayOneShot(overSound, 1.0f);
        endLevelScreen.SetActive(true);
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        isGameActive = true;
        gameAudio.PlayOneShot(startSound, 1.0f);
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
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}