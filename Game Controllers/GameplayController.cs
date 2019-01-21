using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField]
    private Text scoreText, coinText, lifeText, finalScoreText, finalCoinCountText;
    [SerializeField]
    private GameObject pausePanel, gameOverPanel;
    [SerializeField]
    private GameObject readyButton;
    [HideInInspector]
    public int score, coinCount, lifeCount;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        Time.timeScale = 0.0f;
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
   
    public void ShowGameOverPanel(int finalScore, int finalCoinScore)
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = finalScore.ToString();
        finalCoinCountText.text = finalCoinScore.ToString();
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenu");
        //SceneFader.instance.LoadLevel("MainMenu");
    }

    public void PlayerDiedRestartTheGame()
    {
        StartCoroutine(PlayerDiedRestart());
    }

    IEnumerator PlayerDiedRestart()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameScene");
        //SceneFader.instance.LoadLevel("GameScene");
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetCoinScore(int coinScore)
    {
        coinText.text = "x" + coinScore;
    }

    public void SetLifeScore(int lifeScore)
    {
        lifeText.text = "x" + lifeScore;
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        //SceneFader.instance.LoadLevel("MainMenu");
    }

    public void ReadyToStart()
    {
        Time.timeScale = 1.0f;
        readyButton.SetActive(false);
    }
}
