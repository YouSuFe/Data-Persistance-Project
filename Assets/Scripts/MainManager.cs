using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public int highScore = 0;
    public string playerName;

    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public Input playerNameInput;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        GetResults();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
            UpdateText();
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    void UpdateText()
    {
        if(playerName != "" && highScore != 0)
        {
            bestScoreText.text = "High Score " + playerName + ": " + highScore;
        }
        else
        {
            bestScoreText.text = "You can Do it.";
        }
    }

    void GetResults()
    {
        highScore = GameManager.Instance.bestScore;
        playerName = GameManager.Instance.playerName;
    }
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        CheckHighScore();
    }

    void CheckHighScore()
    {
        if(m_Points > highScore)
        {
            GameManager.Instance.SaveScore(m_Points);
            GameManager.Instance.LoadScore();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
