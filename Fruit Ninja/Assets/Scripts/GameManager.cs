using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Pause elements")]
    public GameObject pausePanel;
    private bool isPaused = false;

    [Header("Time elements")]
    public float gameTime = 60.00f; // Duración del juego en segundos
    public Text textTime;
    private float currentTime;

    public GameObject buttonPause;

    private Coroutine updateTimeCoroutine;

    private void Awake()
    {
        SetBestScore();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        currentTime = gameTime;
        UpdateTimerUI();
        updateTimeCoroutine = StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(3f);

        while (currentTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            UpdateTimerUI();

            if (currentTime <= 0f)
            {
                GameOver();
            }
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f); // Obtener los minutos
        int seconds = Mathf.FloorToInt(currentTime % 60f); // Obtener los segundos
        string timerText = string.Format("{0:00}:{1:00}", minutes, seconds); // Formatear el texto del temporizador
        textTime.text = timerText; // Mostrar el temporizador en la UI
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
        textFinalBestScore.text = "BEST SCORE: " + bestScore.ToString();
        gameOverPanel.SetActive(true);
    }

    public void Reset()
    {
        score = 0;
        textScore.text = score.ToString();
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactive"))
        {
            Destroy(g);
        }
        if (updateTimeCoroutine != null)
        {
            StopCoroutine(updateTimeCoroutine);
        }

        StartGame();
    }
    
    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Detiene el tiempo en el juego
            pausePanel.SetActive(true);
            buttonPause.SetActive(false);
        }
    }

    public void Continue()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f; // Reanuda el tiempo en el juego
            pausePanel.SetActive(false);
            buttonPause.SetActive(true);
        }
    }

    private void GameOver()
    {
        if (!isPaused) // Si el juego no está pausado
        {
            // Mostrar panel de fin de juego
            gameOverPanel.SetActive(true);
            textFinalScore.text = "Score: " + score.ToString();
            textFinalBestScore.text = "BEST SCORE: " + bestScore.ToString();

            // Detener el juego
            Time.timeScale = 0f;
        }
    }

    public void ToHome()
    {
        SceneManager.LoadScene(0);
    }
}

