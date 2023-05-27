using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text scoreText;
    public GameObject btnHealth;
    public GameObject starUI;
    public GameObject gameOverUI;
    public ScoreItem scoreItemPrefab;
    public Transform scoreParent;

    private int score = 0;
    public int health = 3;
    public bool gameOver = false;
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        ScoreText("" + score);
        HealthText("" + health);
    }

    public void LoadGameData()
    {
        var listScore = XMLDataManager.instance.LoadData();
        foreach(var item in listScore)
        {
            ScoreItem element = Instantiate(scoreItemPrefab, scoreParent);
            element.SetScore(item.Score);
        }
        
    }

    public void SaveGameData()
    {
        var listScore = XMLDataManager.instance.LoadData();

        string newScore = scoreText.text;

        if (!listScore.Exists(item => item.Score == newScore))
        {
            var gameData = new GameData(newScore);
            listScore.Add(gameData);
            XMLDataManager.instance.SaveData(listScore);
        }
        else
        {
            Debug.Log("Score already exists in the list.");
        }
    }


    public void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            gameOverUI.SetActive(true);
        }
    }


    public void SubHealth(int value)
    {
        if (gameOver)
        {
            return;
        }

        health -= value;
        if (health <=0)
        {
            health= 0;
            EndGame();
        }
        HealthText("" + health);
    }

    public void AddScore(int value)
    {
        if (gameOver)
        {
            return;
        }

        score += value;
        ScoreText("" + score);
    }

    public void HealthText(string txt)
    {
        if (healthText)
        {
            healthText.text = txt;
        }    
    }

    public void ScoreText(string txt)
    {
        if(scoreText) 
        {
            scoreText.text = txt;
        }
    }

    public void RePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
