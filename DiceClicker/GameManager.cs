using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static GameManager instance;

    private int[] AutoClick = new int[5];
    private int[] EnchantDice = new int[5];
    private int[] DiceSets = new int[5];

    private int score;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        score = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScore(0);

        AutoClick[0] = PlayerPrefs.GetInt("AutoClick1", 0);
        EnchantDice[0] = PlayerPrefs.GetInt("EnchantDice1", 0);
        DiceSets[0] = PlayerPrefs.GetInt("DiceSet2", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        PlayerPrefs.SetInt("HighScore", score);
    }

    public int getScore()
    {
        return score;
    }

    public int getAutoClickSkills(int diceSetNumber)
    {
        return AutoClick[diceSetNumber];
    }

    public int getEnchantSkill(int diceSetNumber)
    {
        return EnchantDice[diceSetNumber];
    }

    public int getDiceSets(int diceSetNumber)
    {
        return DiceSets[diceSetNumber];
    }
}
