using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    Text scoreText;
    private void Start()
    {
        scoreText = GetComponent<Text>();
        Player.instance.scoreValueChanged += OnChangedScore;
    }
    void OnChangedScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
