using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int _playerLives = 3;
    [SerializeField] private int _playerScore;

    [SerializeField] private Text _livesText;
    [SerializeField] private Text _scoreText;
    
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _livesText.text = _playerLives.ToString();
        _scoreText.text = _playerScore.ToString();
    }

    public void AddToScore(int pointToAdd)
    {
        _playerScore += pointToAdd;
        _scoreText.text = _playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (_playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        _playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        _livesText.text = _playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
