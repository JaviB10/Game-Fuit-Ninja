using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score elements")]
    public int score;
    public int bestScore;
    public Text textScore;
    public Text textBestScore;

    [Header("Game Over elements")]
    public GameObject gameOverPanel;
    public Text textFinalScore;
    public Text textFinalBestScore;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        SetBestScore();
    }

    private void SetBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        textBestScore.text = "BEST: " + bestScore.ToString();
    }

    public void IncreaseScore()
    {
        score += 2;
        textScore.text = score.ToString();

        if(score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            textBestScore.text = "BEST: " + score.ToString();
        }
    }

    public void ToTouchBomb()
    {
        Time.timeScale = 0;
        textFinalScore.text = "Score: " + score.ToString();
        gameOverPanel.SetActive(true);
        textFinalBestScore.text = "BEST SCORE: " + bestScore.ToString();
    }

    public void Reset()
    {
        score = 0;
        textScore.text = score.ToString();
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactive"))
        {
            Destroy(g);
        }
    }
}
