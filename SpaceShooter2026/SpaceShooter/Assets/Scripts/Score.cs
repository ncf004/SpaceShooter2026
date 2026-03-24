using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void HitEnemy()
    {
        score += 1;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}