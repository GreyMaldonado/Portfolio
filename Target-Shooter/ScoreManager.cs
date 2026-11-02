using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;

    private int score;
    private int bonusChance;

    void Awake()
    {
        score = PlayerPrefs.GetInt("score");
        bonusChance = PlayerPrefs.GetInt("bonusChance");
        instance = this;
        //setScoreText();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs.SetInt("score", score);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if(bonusChance > 0)
        {
            int randomVal = Random.Range(0, 101);
            if(randomVal < bonusChance)
            {
                score += amount;
                Debug.Log("BONUS");
            }
        }
        setScoreText();
    }

    public void RemoveScore(int amount)
    {
        score -= amount;
        setScoreText();
    }

    void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetInt("score", score);
        //Debug.Log(bonusChance);
        //Debug.Log(score);
        //Debug.Log(PlayerPrefs.GetInt("score"));
    }

    public int getScore()
    {
        return score;
    }
}
