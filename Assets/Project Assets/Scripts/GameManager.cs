using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;

    /*private void Start()
    {
        ResetScore();
    }*/

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void DecreaseScore()
    {
        score--;
        scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return this.score;
    }
}
