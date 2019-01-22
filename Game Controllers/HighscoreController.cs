using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText, coinText;


    // Start is called before the first frame update
    void Start()
    {
        SetScoreBasedOnDifficulty();
    }

    void SetScore(int score, int coinScore)
    {
        //scoreText.text = score.ToString();
        //coinText.text = coinScore.ToString();   
        scoreText.text = "" + score;
        coinText.text = "" + coinScore;
    }

    void SetScoreBasedOnDifficulty()
    {
        if(GamePreferences.GetEasyDifficultyState() == 0)
        {
            SetScore(GamePreferences.GetEasyDifficultyHighscore(), GamePreferences.GetEasyDifficultyCoinScore());
        }
        if (GamePreferences.GetMediumDifficultyState() == 0)
        {
            SetScore(GamePreferences.GetMediumDifficultyHighscore(), GamePreferences.GetMediumDifficultyCoinScore());
        }
        if (GamePreferences.GetHardDifficultyState() == 0)
        {
            SetScore(GamePreferences.GetHardDifficultyHighscore(), GamePreferences.GetHardDifficultyCoinScore());
        }
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
