using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{
    public Text scoreText;

    public void SetScore(string score)
    {
        scoreText.text = score;
    }
}

