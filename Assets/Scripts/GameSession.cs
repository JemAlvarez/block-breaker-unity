using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(0.1f, 2.0f)] [SerializeField] float timeScale = 1f;
    [SerializeField] int scorePerBlock = 83;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplay = false;


    // state vars
    [SerializeField] int playerScore = 0;           // Display for debug

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;

        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PrintScore();
    }

    // Update is called once per frame
    void Update()
    {
        PrintScore();
        Time.timeScale = timeScale;
    }

    public void RestartGame()
    {
        Destroy(gameObject);
    }

    public void AddScore()
    {
        playerScore += scorePerBlock;
    }

    void PrintScore()
    {
        scoreText.text = playerScore.ToString();
    }

    public bool AutoPlay()
    {
        return autoplay;
    }
}
