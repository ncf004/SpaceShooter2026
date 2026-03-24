using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;

    public bool IsGameActive { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowTitleScreen();
    }

    public void ShowTitleScreen()
    {
        IsGameActive = false;

        if (titleScreen != null) titleScreen.SetActive(true);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (gameWinScreen != null) gameWinScreen.SetActive(false);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        IsGameActive = true;

        if (titleScreen != null) titleScreen.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (gameWinScreen != null) gameWinScreen.SetActive(false);
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.BeginWaves();
        }

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        IsGameActive = false;

        if (gameOverScreen != null) gameOverScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    public void GameWin()
    {
        IsGameActive = false;

        if (gameWinScreen != null) gameWinScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}