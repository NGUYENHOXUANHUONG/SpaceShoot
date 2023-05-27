using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //public GameObject starUI;
    public Text scoreText;
    public ScoreItem scoreItemPrefab;
    public Transform scoreParent;

    private void Start()
    {
        LoadGameData();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGameData()
    {
        var listScore = XMLDataManager.instance.LoadData();
        foreach (var item in listScore)
        {
            ScoreItem element = Instantiate(scoreItemPrefab, scoreParent);
            element.SetScore(item.Score);
        }

    }


}
