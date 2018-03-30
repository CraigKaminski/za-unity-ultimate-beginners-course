using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUiManager : MonoBehaviour {

    public Text score;
    public Text highScore;

	// Use this for initialization
	void Start () {
        score.text = GameManager.instance.score.ToString();
        highScore.text = GameManager.instance.highScore.ToString();
	}

    public void RestartGame()
    {
        GameManager.instance.ResetGame();
        SceneManager.LoadScene("Level1");
    }
}
