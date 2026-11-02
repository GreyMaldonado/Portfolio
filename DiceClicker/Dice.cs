using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    public Button buyButton;
    public float rollCooldown;
    public float timeToRoll;
    public List<GameObject> diceSet; // a list of all the dice in the set for unlocking purposes
    public int numOfDice; // number of dice in the set
    public int priceToBuy;
    public int diceSetNumber;

    private GameManager gameManager;
    private float rollCooldownTimer; // how long the program must wait to roll dice again, timer
    private float timeToRollTimer; // how long the program takes to roll all the dice in the set
    private int totalValue; // total value of all dice rolls
    private int[] valueSet; // the values of all the dice rolls
    private TextMeshProUGUI buttonText;
    private float autoClickCooldown; // time for the cooldown between each autoclick, used to set timer
    private float autoClickCooldownTimer; // time between each autoClick roll, timer


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //button = GetComponent<Button>();
        //button.onClick.AddListener(RollDiceD6);
        numOfDice = PlayerPrefs.GetInt("NumDice1", 1); // Get number of dice in dice set 1 from player prefs
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        valueSet = new int[numOfDice];
        buyButton.onClick.AddListener(() => BuyDice());
        buttonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
        autoClickCooldown = 15.0f;
        //priceToBuy = 5;
        if (numOfDice > 1)
        {
            for (int i = 1; i < numOfDice; i++)
            {
                diceSet[i].SetActive(true);
                priceToBuy *= 2;
            }
        }
        UpdateBuyText(priceToBuy);
    }

    // Update is called once per frame
    void Update()
    {
        if (rollCooldownTimer > 0.0f)
        {
            rollCooldownTimer -= Time.deltaTime;
        }
        if(numOfDice > valueSet.Length)
        {
            valueSet = new int[numOfDice]; 
        }
        if(numOfDice == 5)
        {
            buyButton.gameObject.SetActive(false);
        }

        if (gameManager.getAutoClickSkills(diceSetNumber) > 0)
        {
            //Debug.Log("Auto click active");
            if(autoClickCooldownTimer > 0.0f)
            {
                autoClickCooldownTimer -= Time.deltaTime;
            }
            else if(rollCooldownTimer <= rollCooldown)
            {
                AutoClickRoll();
            }
        }

        //PlayerPrefs.SetInt("NumDice1", 1); // ONLY UNCOMMENT THIS TO RESET NUM OF DICE. Will figure out better system later
    }

    
    private void OnMouseDown()
    {
        if (rollCooldownTimer > 0.0f || autoClickCooldownTimer > autoClickCooldown)
        {
            Debug.Log("Die is on cooldown");
        }
        else
        {
            for (int i = 0; i < numOfDice; i++)
            {
                valueSet[i] = Random.Range(1, 7);
            }
            rollCooldownTimer = rollCooldown + timeToRoll;
            StartCoroutine(roll());
            Debug.Log("Manually clicked dice");
        }
    }

    IEnumerator roll()
    {
        yield return new WaitForSeconds(timeToRoll);
        totalValue = 0;
        for(int i = 0; i < numOfDice;i++)
        {
            totalValue += valueSet[i];
            Debug.Log("Die " + (i + 1) + "'s value is " + valueSet[i]);
        }
        //Debug.Log("Clicked Dice Set Object and total value is: " + totalValue);
        gameManager.UpdateScore(totalValue);
    }

    public void BuyDice()
    {
        if(gameManager.getScore() >= priceToBuy)
        {
            diceSet[numOfDice].SetActive(true);
            numOfDice++;
            PlayerPrefs.SetInt("NumDice1", numOfDice);
            gameManager.UpdateScore(-priceToBuy);
            priceToBuy *= 2;
            UpdateBuyText(priceToBuy);
        }
        else
        {
            Debug.Log("Not enough points to buy");
        }
    }

    void UpdateBuyText(int price)
    {
        buttonText.text = "Buy Dice\n" + priceToBuy + " Points";
    }

    void AutoClickRoll()
    {
        for(int i = 0; i < numOfDice; i++)
        {
            valueSet[i] = Random.Range(1, 7);
        }
        autoClickCooldownTimer = autoClickCooldown + timeToRoll;
        StartCoroutine(roll());
        Debug.Log("Auto clicked the dice");
    }
}
