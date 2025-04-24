using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private int streak;
    public TextMeshProUGUI scoreText;
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
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
