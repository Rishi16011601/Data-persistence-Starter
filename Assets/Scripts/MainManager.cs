using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    Manager man;

    public static MainManager Instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public string PlayerName;
    public int PlayerScore;
    public string BestPlayer;
    public int BestScore;

    
    private void Awake()
    {
        //Manager.Instance.LoadDetails();
        PlayerName = Manager.Instance.PlayerName;
        PlayerScore = 0;
        BestPlayer = Manager.Instance.BestPlayer;
        //BestPlayer = "hello";
        BestScore = Manager.Instance.BestScore;
        //BestScore = 5;
        BestDisplay();
        Debug.Log(PlayerName);
    }
    

    /*
    public void PrintDetails()
    {
        Debug.Log(PlayerName);
        Debug.Log(PlayerScore);
        Debug.Log(BestPlayer);
        Debug.Log(BestScore);

    }
    */
    

    // Start is called before the first frame update
    void Start()
    {
        //MakeChanges();

        //PrintDetails();
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
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        //MakeChanges();

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
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        //PrintDetails();
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void BestDisplay()
    {
        BestScoreText.text = $"Best Score : {BestPlayer} : {BestScore}";
    }

    public void GameOver()
    {
        if(m_Points > BestScore)
        {
            Manager.Instance.BestPlayer = PlayerName;
            Manager.Instance.BestScore = m_Points;
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
        //man.SaveDetails(PlayerName);
        //MakeChanges();
        Manager.Instance.SaveDetails(PlayerName);
        BestDisplay();
    }

    

    
    
    

}
