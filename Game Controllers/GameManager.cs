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
                PlayerScore.lifeCount = 2;

                GameplayController.instance.SetScore(-1);
                GameplayController.instance.SetCoinScore(0);
                GameplayController.instance.SetLifeScore(2);
            }
        }
    }

    void InitializeVariables()
    {
        if(!PlayerPrefs.HasKey("Game Initialized"))
        {
            GamePreferences.SetEasyDifficulty(0);
            GamePreferences.SetEasyDifficultyCoinScore(0);
            GamePreferences.SetEasyDifficultyHighScore(0);

            GamePreferences.SetMediumDifficulty(1);
            GamePreferences.SetMediumDifficultyCoinScore(0);
            GamePreferences.SetMediumDifficultyHighScore(0);

            GamePreferences.SetHardDifficulty(0);
            GamePreferences.SetHardDifficultyCoinScore(0);
            GamePreferences.SetHardDifficultyHighScore(0);

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
            if(GamePreferences.GetEasyDifficulty() == 1)
            {
                int highScore = GamePreferences.GetEasyDifficultyHighScore();
                int coinHighScore = GamePreferences.GetEasyDifficultyCoinScore();

                if(highScore < score)
                {
                    GamePreferences.SetEasyDifficultyHighScore(score);
                }
                if(coinHighScore < coinCount)
                {
                    GamePreferences.SetEasyDifficultyCoinScore(coinCount);
                }
            }
            if (GamePreferences.GetMediumDifficulty() == 1)
            {
                int highScore = GamePreferences.GetMediumDifficultyHighScore();
                int coinHighScore = GamePreferences.GetMediumDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetMediumDifficultyHighScore(score);
                }
                if (coinHighScore < coinCount)
                {
                    GamePreferences.SetMediumDifficultyCoinScore(coinCount);
                }
            }
            if (GamePreferences.GetHardDifficulty() == 1)
            {
                int highScore = GamePreferences.GetHardDifficultyHighScore();
                int coinHighScore = GamePreferences.GetHardDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetHardDifficultyHighScore(score);
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