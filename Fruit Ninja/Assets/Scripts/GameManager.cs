using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Score Elements")]
    public int score;
    public int bestScore;
    public Text textScore;
    public Text textBestScore;
    public AudioSource soundBestScore;
    public bool hasPlayedBestScoreSound = false;

    [Header("GameOver Elements")]
    public GameObject gameOverPanel;
    public Text textFinalScore;
    public Text textFinalBestScore;

    [Header("Win Elements")]
    public GameObject winPanel;
    public Text textFinalScoreWin;
    public Text textFinalBestScoreWin;

    [Header("Pause Elements")]
    public GameObject pausePanel;
    public GameObject buttonPause;
    private bool isPaused = false;

    [Header("Time Elements")]
    public float gameTime = 60.00f; // Duración del juego en segundos
    private float currentTime;
    public Text textTime;
    private Coroutine updateTimeCoroutine;
    public AudioSource soundTime;

    [Header("Life Elements")]
    public int lifes = 3;
    public GameObject[] imagesLife;

    public ToThrowFruits toThrowFruits;

    void Start()
    {
        InitGame();
        print("entro a start");
    }

    void Awake()
    {
        SetBestScore();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
        Reset();
    }

    private void InitGame()
    {
        Time.timeScale = 1f;

        hasPlayedBestScoreSound = false;
        currentTime = gameTime;
        UpdateTimerUI();

        // Reinicia la puntuación
        score = 0;
        textScore.text = score.ToString();

        // Reinicia las vidas
        lifes = 3;

        isPaused = false;

        foreach (GameObject lifeImage in imagesLife)
        {
            lifeImage.SetActive(false);
        }

        // Detiene cualquier instancia previa de la corutina
        if (updateTimeCoroutine != null)
        {
            StopCoroutine(updateTimeCoroutine);
        }

        // Inicia la corutina del temporizador
        updateTimeCoroutine = StartCoroutine(UpdateTime());

        // Oculta los paneles de gameOver y win
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        buttonPause.SetActive(true);

       
        toThrowFruits.RestartThrowing();
           
        
        print("entro a init");
        print(lifes);
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
                YouWin();
            }
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f); // Obtener los minutos
        int seconds = Mathf.FloorToInt(currentTime % 60f); // Obtener los segundos
        string timerText = string.Format("{0:00}:{1:00}", minutes, seconds); // Formatear el texto del temporizador
        textTime.text = timerText; // Mostrar el temporizador en la UI

        if (currentTime <= 5f)
        {   
            textTime.color = Color.red;
            soundTime.Play();         
        }
        else
        {
            textTime.color = Color.white;
        }
    }

    private void SetBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        textBestScore.text = "BEST: " + bestScore.ToString();
    }

    public void IncreaseScore()
    {
        score += 100;
        textScore.text = score.ToString();
        
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", score);
            textBestScore.text = "BEST: " + score.ToString();
            if (!hasPlayedBestScoreSound && soundBestScore != null)
            {
                soundBestScore.Play();
                hasPlayedBestScoreSound = true; // Marcar que el sonido se ha reproducido
            }
        }
    }

    public void DecreaseScore() 
    {
        if (score <= 300)
        {
            score = 0;
            textScore.text = score.ToString();
        }
        else
        {
            score -= 300;
            textScore.text = score.ToString();
        }
        
    }

    public void ToTouchBomb()
    {
        print(lifes);
        lifes -= 1;
        print(lifes);
        if (lifes == 2)
        {
            ShowLifeImage(0);
        }
        else if (lifes == 1)
        {
            ShowLifeImage(1);
        }
        else if (lifes == 0)
        {
            ShowLifeImage(2);
            GameOver();
        }
    }

    // Método para mostrar la imagen de vida en la posición indicada
    private void ShowLifeImage(int index)
    {
        if (index >= 0 && index < imagesLife.Length)
        {
            imagesLife[index].SetActive(true);
        }
    }

    public void ToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactive"))
        {
            Destroy(g);
        }

        InitGame();
    }
    
    public void Pause()
    {
        isPaused = !isPaused;
        print(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            buttonPause.SetActive(false);
        }
    }

    public void Continue()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            buttonPause.SetActive(true);
        }
    }

    private void GameOver()
    {
        print(isPaused);
        if (!isPaused) // Si el juego no está pausado
        {
            print("entro aca");
            // Mostrar panel de fin de juego
            gameOverPanel.SetActive(true);
            gameOverPanel.GetComponent<AudioSource>().Play();
            textFinalScore.text = "Score: " + score.ToString();
            textFinalBestScore.text = "BEST SCORE: " + bestScore.ToString();

            // Detener el juego
            Time.timeScale = 0f;
        }
    }

    private void YouWin()
    {
        if (!isPaused)
        {
            winPanel.SetActive(true);
            winPanel.GetComponent<AudioSource>().Play();
            textFinalScoreWin.text = "Score: " + score.ToString();
            textFinalBestScoreWin.text = "BEST SCORE: " + bestScore.ToString();

            Time.timeScale = 0f;
        }
    }
}

