using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FishingMinigame : MonoBehaviour
{
    public static FishingMinigame Instance;
    
    [Header("UI Elements")]
    public GameObject minigamePanel;
    public RectTransform fishImage;
    public RectTransform barImage;
    public TMP_Text timerText;
    public RectTransform gameArea;
    
    [Header("Game Settings")]
    public float gameDuration = 8f;
    public float requiredTime = 4f;
    public float fishMoveSpeed = 100f;
    public float fishMoveRangeX = 300f;
    public float fishMoveRangeY = 150f;
    public float barSpeed = 500f;
    
    private float successTime = 0f;
    private float currentTime = 0f;
    private Vector2 fishTargetPosition;
    private bool isMinigameActive = false;
    private Vector2 barMovementInput;
    private Rect gameAreaRect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        minigamePanel.SetActive(false);
    }

    private void Start()
    {
        gameAreaRect = new Rect(
            gameArea.rect.x + gameArea.anchoredPosition.x,
            gameArea.rect.y + gameArea.anchoredPosition.y,
            gameArea.rect.width,
            gameArea.rect.height);
    }
    
    private void Update()
    {
        if (!isMinigameActive) return;
        
        HandleBarMovement();
        UpdateFishMovement();
        CheckOverlap();
        UpdateTimer();
    }

    private void HandleBarMovement()
    {
        // Captura input (pode ser mouse ou teclado/controle)
        barMovementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
        
        // Move a barra
        Vector2 newPosition = barImage.anchoredPosition + 
                             barMovementInput * barSpeed * Time.deltaTime;
        
        // Limita a barra à área do jogo
        float barHalfWidth = barImage.rect.width / 2;
        float barHalfHeight = barImage.rect.height / 2;
        
        newPosition.x = Mathf.Clamp(
            newPosition.x,
            gameAreaRect.xMin + barHalfWidth,
            gameAreaRect.xMax - barHalfWidth);
            
        newPosition.y = Mathf.Clamp(
            newPosition.y,
            gameAreaRect.yMin + barHalfHeight,
            gameAreaRect.yMax - barHalfHeight);
        
        barImage.anchoredPosition = newPosition;
    }

    private void UpdateFishMovement()
    {
        // Movimento suave do peixe
        fishImage.anchoredPosition = Vector2.Lerp(
            fishImage.anchoredPosition, 
            fishTargetPosition, 
            Time.deltaTime * 2f);
            
        if (Vector2.Distance(fishImage.anchoredPosition, fishTargetPosition) < 5f)
        {
            SetNewFishTarget();
        }
    }

    private void SetNewFishTarget()
    {
        fishTargetPosition = new Vector2(
            Random.Range(-fishMoveRangeX, fishMoveRangeX),
            Random.Range(-fishMoveRangeY, fishMoveRangeY));
        
        // Garante que o peixe fique dentro da área
        fishTargetPosition.x = Mathf.Clamp(
            fishTargetPosition.x,
            gameAreaRect.xMin + fishImage.rect.width/2,
            gameAreaRect.xMax - fishImage.rect.width/2);
            
        fishTargetPosition.y = Mathf.Clamp(
            fishTargetPosition.y,
            gameAreaRect.yMin + fishImage.rect.height/2,
            gameAreaRect.yMax - fishImage.rect.height/2);
    }

    private void CheckOverlap()
    {
        Rect fishRect = new Rect(
            fishImage.anchoredPosition.x - fishImage.rect.width/2,
            fishImage.anchoredPosition.y - fishImage.rect.height/2,
            fishImage.rect.width,
            fishImage.rect.height);
            
        Rect barRect = new Rect(
            barImage.anchoredPosition.x - barImage.rect.width/2,
            barImage.anchoredPosition.y - barImage.rect.height/2,
            barImage.rect.width,
            barImage.rect.height);
            
        if (fishRect.Overlaps(barRect))
        {
            successTime += Time.deltaTime;
            barImage.GetComponent<Image>().color = Color.green;
        }
        else
        {
            barImage.GetComponent<Image>().color = Color.red;
        }
    }

    private void UpdateTimer()
    {
        currentTime += Time.deltaTime;
        timerText.text = (gameDuration - currentTime).ToString("F1");
        
        if (currentTime >= gameDuration)
        {
            EndMinigame();
        }
    }
    
    public void StartMinigame()
    {
        minigamePanel.SetActive(true);
        successTime = 0f;
        currentTime = 0f;
        isMinigameActive = true;
        
        // Posições iniciais
        barImage.anchoredPosition = Vector2.zero;
        fishImage.anchoredPosition = Vector2.zero;
        SetNewFishTarget();
    }
    
    private void EndMinigame()
    {
        isMinigameActive = false;
        minigamePanel.SetActive(false);
        
        if (successTime >= requiredTime)
        {
            FishingManager.Instance.OnFishingSuccess();
        }
        else
        {
            FishingManager.Instance.OnFishingFailure();
        }
    }
}