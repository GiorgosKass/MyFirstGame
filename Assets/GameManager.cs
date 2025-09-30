using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject gameLogo;
    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI congratsText;
    public AudioSource backgroundMusic;




    private bool isGameOver = false;
    void Start()
    {
        score = 0;
        UpdateScore(0);
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        gameLogo.SetActive(false);
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score >= 100 && !isGameOver)
        {
            Debug.Log("Reached 100!");
            isGameOver = true;

            if (congratsText != null)
            {
                congratsText.gameObject.SetActive(true);
                congratsText.text = "Congratulations!";
            }

            if (gameLogo != null)
            {
                gameLogo.SetActive(true);
            }

            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }

            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }

            Time.timeScale = 0f;
        }
    }
    


    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }
            Invoke("ShowGameOverUI", 1.5f);
        }
    }

    void ShowGameOverUI()
    {
        Time.timeScale = 0f;
       
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        gameLogo.SetActive(true);
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Restart button clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
