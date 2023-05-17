using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public Text textScore;

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        this.score = 0;
        this.scoreText.text = this.score.ToString();
    }

    public void IncreaseScore()
    {
        this.score++;
        this.scoreText.text = this.score.ToString();
    }
}
