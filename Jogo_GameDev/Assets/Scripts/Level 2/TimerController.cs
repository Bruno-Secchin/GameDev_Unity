using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    private float timeRemaining = 30f; // Tempo inicial (em segundos)
    public bool isCountingDown = true;
    private TextMeshProUGUI timerText;
    private L2GameManager gameManager;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<L2GameManager>();
        UpdateTimerDisplay(timeRemaining);
    }

    void Update()
    {
        if (gameManager.isGameActive && isCountingDown && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining);

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isCountingDown = false;
                gameManager.EndLevel();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        int seconds = Mathf.CeilToInt(timeToDisplay); // Arredonda para cima
        timerText.text = "Timer:\n" + seconds.ToString();
    }
}
