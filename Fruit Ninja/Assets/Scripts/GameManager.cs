using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text textScore;

    public void IncreaseScore()
    {
        score += 2;
        textScore.text = score.ToString();
    }

    public void ToTouchBomb()
    {
        Time.timeScale = 0;
    }
}
