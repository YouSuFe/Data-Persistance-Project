using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text bestScoreText;
    public InputField nameField;
    public int score;
    public string playerName;
    public string currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadScore();
        if(GameManager.Instance.playerName != "")
        {
            playerName = GameManager.Instance.playerName;
            score = GameManager.Instance.bestScore;
            bestScoreText.text = "Best Score : " + playerName + " : " + score;
        }
        else
        {
            bestScoreText.text = " You can do it.";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateName();
    }

    void UpdateName()
    {
        currentPlayer = nameField.text;
    }

    public void StartGame()
    {
        GameManager.Instance.playerName = playerName;
        GameManager.Instance.currentPlayer = currentPlayer;
        SceneManager.LoadScene(1);
    }

    public void ResetScore()
    {
        if(GameManager.Instance.playerName != "")
        {
            GameManager.Instance.ResetScore();
            GameManager.Instance.LoadScore();
            bestScoreText.text = "Best Score Reseted!";
        }
        else
        {
            bestScoreText.text = "There is nothing to do.";
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
