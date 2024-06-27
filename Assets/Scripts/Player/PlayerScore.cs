using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public Text scoreText; 
    public Text highScoreText; 
    private int score;

    private void Start()
    {
        // Загрузка сохраненного счета
        score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + score;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    public void EnemyKilled()
    {
        score += 10; 
        PlayerPrefs.SetInt("Score", score);
        UpdateScoreText();

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score: " + score;
        }
    }

    public void DoorTouched()
    {
        score += 500;
        PlayerPrefs.SetInt("Score", score);
        UpdateScoreText();

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "High Score: " + score;
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; 
    }
}
