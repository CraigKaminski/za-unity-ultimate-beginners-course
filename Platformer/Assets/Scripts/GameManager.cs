using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int score = 0;
    public int highScore = 0;
    public int currentLevel = 1;
    public int highestLevel = 2;
    public static GameManager instance;
    HudManager hudManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            instance.hudManager = FindObjectOfType<HudManager>();
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        hudManager = FindObjectOfType<HudManager>();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        if (hudManager != null)
            hudManager.ResetHud();
        print("new score: " + score);

        if (score > highScore)
        {
            highScore = score;
            print("new high score: " + highScore);
        }
    }

    public void ResetGame()
    {
        score = 0;
        if (hudManager != null)
            hudManager.ResetHud();
        currentLevel = 1;
        SceneManager.LoadScene("Level1");
    }

    public void IncreaseLevel()
    {
        if (currentLevel < highestLevel)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 1;
        }

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
