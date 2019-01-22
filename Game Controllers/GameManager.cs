using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedAfterDeath;
    [HideInInspector]
    public int score, coinCount, lifeCount;
        
    void Awake()
    {
        MakeSingleton();        
    }

    void Start()
    {
        InitializeVariables();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            if (gameRestartedAfterDeath)
            {
                score = score - 1;
                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetCoinScore(coinCount);
                GameplayController.instance.SetLifeScore(lifeCount);

                PlayerScore.scoreCount = score;
                PlayerScore.coinCount = coinCount;
                PlayerScore.lifeCount = lifeCount;

            }
            else if (gameStartedFromMainMenu)
            {
                PlayerScore.scoreCount = -1;
                PlayerScore.coinCount = 0;
                PlayerScore.lifeCount = 3;

                GameplayController.instance.SetScore(-1);
                GameplayController.instance.SetCoinScore(0);
                GameplayController.instance.SetLifeScore(3);
            }
        }
    }

    void InitializeVariables()
    {
        if(!PlayerPrefs.HasKey("Game Initialized"))
        {
            GamePreferences.SetEasyDifficultyState(1);
            GamePreferences.SetEasyDifficultyCoinScore(0);
            GamePreferences.SetEasyDifficultyHighscore(0);

            GamePreferences.SetMediumDifficultyState(0);
            GamePreferences.SetMediumDifficultyCoinScore(0);
            GamePreferences.SetMediumDifficultyHighscore(0);

            GamePreferences.SetHardDifficultyState(1);
            GamePreferences.SetHardDifficultyCoinScore(0);
            GamePreferences.SetHardDifficultyHighscore(0);

            GamePreferences.SetMusicState(1);

            PlayerPrefs.SetInt("Game Initialized", 420);
        }

    }

    //Singleton pattern
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CheckGameStatus(int score, int coinCount, int lifeCount)
    {
        if(lifeCount <=0 )
        {
            if(GamePreferences.GetEasyDifficultyState() == 0)
            {
                int highScore = GamePreferences.GetEasyDifficultyHighscore();
                int coinHighScore = GamePreferences.GetEasyDifficultyCoinScore();

                if(highScore < score)
                {
                    GamePreferences.SetEasyDifficultyHighscore(score);
                }
                if(coinHighScore < coinCount)
                {
                    GamePreferences.SetEasyDifficultyCoinScore(coinCount);
                }
            }
            if (GamePreferences.GetMediumDifficultyState() == 0)
            {
                int highScore = GamePreferences.GetMediumDifficultyHighscore();
                int coinHighScore = GamePreferences.GetMediumDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetMediumDifficultyHighscore(score);
                }
                if (coinHighScore < coinCount)
                {
                    GamePreferences.SetMediumDifficultyCoinScore(coinCount);
                }
            }
            if (GamePreferences.GetHardDifficultyState() == 0)
            {
                int highScore = GamePreferences.GetHardDifficultyHighscore();
                int coinHighScore = GamePreferences.GetHardDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetHardDifficultyHighscore(score);
                }
                if (coinHighScore < coinCount)
                {
                    GamePreferences.SetHardDifficultyCoinScore(coinCount);
                }
            }

            gameStartedFromMainMenu = false;
            gameRestartedAfterDeath = false;

            GameplayController.instance.ShowGameOverPanel(score, coinCount);
        }
        else
        {
            this.score = score;
            this.coinCount = coinCount;
            this.lifeCount = lifeCount;

            GameplayController.instance.SetScore(score);
            GameplayController.instance.SetCoinScore(coinCount);
            GameplayController.instance.SetLifeScore(lifeCount);

            gameRestartedAfterDeath = true;
            gameStartedFromMainMenu = false;

            GameplayController.instance.PlayerDiedRestartTheGame();
        }
    }
}