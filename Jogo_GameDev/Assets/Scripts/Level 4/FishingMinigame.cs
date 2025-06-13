using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FishingMinigame : MonoBehaviour
{
    public static FishingMinigame Instance;
    
    [Header("UI Elements")]
    public GameObject minigamePanel;
    public Image fishImage;
    public Image barImage;
    public RectTransform gameBackground;
    
    [Header("Game Settings")]
    public float gameDuration = 8f;
    public float requiredTime = 4f;
    public float fishMoveSpeed = 100f;
    public float fishMoveRange = 150f;
    
    private float successTime = 0f;
    private float currentTime = 0f;
    private Vector2 fishTargetPosition;
    private bool isMinigameActive = false;
    
    [Header("Progress Bars")]
    public Slider timeBar;
    public Slider progressBar;
    
    [Header("Audio Feedback")]
    public AudioClip successSound;
    public AudioClip failureSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        minigamePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isMinigameActive) return;

        // Movimento do peixe
        fishImage.rectTransform.anchoredPosition = Vector2.Lerp(
            fishImage.rectTransform.anchoredPosition,
            fishTargetPosition,
            Time.deltaTime * 2f);

        if (Vector2.Distance(fishImage.rectTransform.anchoredPosition, fishTargetPosition) < 5f)
        {
            SetNewFishTarget();
        }

        // Controle da barra com limites
        Vector2 mousePos = Input.mousePosition;
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            minigamePanel.GetComponent<RectTransform>(),
            mousePos,
            null,
            out canvasPos);

        // Obtém o retângulo da área de jogo
        Rect gameArea = gameBackground.GetComponent<RectTransform>().rect;
        Vector2 barSize = barImage.rectTransform.sizeDelta;

        // Calcula os limites
        float minX = gameArea.xMin + barSize.x / 2;
        float maxX = gameArea.xMax - barSize.x / 2;
        float minY = gameArea.yMin + barSize.y / 2;
        float maxY = gameArea.yMax - barSize.y / 2;

        // Aplica os limites
        float clampedX = Mathf.Clamp(canvasPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(canvasPos.y, minY, maxY);

        barImage.rectTransform.anchoredPosition = new Vector2(clampedX, clampedY);

        // Verifica sobreposição
        Rect fishRect = new Rect(
            fishImage.rectTransform.anchoredPosition.x - fishImage.rectTransform.sizeDelta.x / 2,
            fishImage.rectTransform.anchoredPosition.y - fishImage.rectTransform.sizeDelta.y / 2,
            fishImage.rectTransform.sizeDelta.x,
            fishImage.rectTransform.sizeDelta.y);

        Rect barRect = new Rect(
            barImage.rectTransform.anchoredPosition.x - barImage.rectTransform.sizeDelta.x / 2,
            barImage.rectTransform.anchoredPosition.y - barImage.rectTransform.sizeDelta.y / 2,
            barImage.rectTransform.sizeDelta.x,
            barImage.rectTransform.sizeDelta.y);

        if (fishRect.Overlaps(barRect))
        {
            successTime += Time.deltaTime;
            barImage.color = Color.green;

            progressBar.value = successTime / requiredTime;

            // Verifica vitória antecipada
            if (progressBar.value >= 1f)
            {
                PlaySuccess();
                EndMinigame(true);
                return;
            }
        }
        else
        {
            barImage.color = Color.red;
        }

        // Verifica derrota por tempo
        if (timeBar.value <= 0f)
        {
            PlayFailure();
            EndMinigame(false);
            return;
        }

        // Atualiza as barras de progresso
        currentTime += Time.deltaTime;
        timeBar.value = 1 - (currentTime / gameDuration);

        progressBar.value = successTime / requiredTime;
    }
    
    private void PlaySuccess()
    {
        audioSource.PlayOneShot(successSound);
    }

    private void PlayFailure()
    {
        audioSource.PlayOneShot(failureSound);
    }
    private void SetNewFishTarget()
    {
        fishTargetPosition = new Vector2(
            Random.Range(-fishMoveRange, fishMoveRange),
            Random.Range(-fishMoveRange / 2, fishMoveRange / 2));
    }

    public void StartMinigame()
    {
        minigamePanel.SetActive(true);
        successTime = 0f;
        currentTime = 0f;
        isMinigameActive = true;
        SetNewFishTarget();
        fishImage.rectTransform.anchoredPosition = Vector2.zero;
        // Reseta as barras
        timeBar.value = 1;
        progressBar.value = 0;
    }
    
    private void EndMinigame(bool wasSuccessful)
    {
        isMinigameActive = false;
        minigamePanel.SetActive(false);

        if (wasSuccessful)
        {
            // Efeitos adicionais de sucesso
            FishingManager.Instance.OnFishingSuccess();
        }
        else
        {
            // Efeitos adicionais de falha
            FishingManager.Instance.OnFishingFailure();
        }
    }
}