using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public int cost = 0;
    public string skillName = null;
    public TextMeshProUGUI costText;
    public int numOfPurchases = 0; //Number of times this skill can be purchased
    public Material fullyPurchased;
    public Material unpurchased;
    public Renderer meshRenderer;
    public ScoreManager scoreManager;

    private int purchaseStatus = 0;
    private Collider thisCollider;

    void Awake()
    {
        thisCollider = gameObject.GetComponent<Collider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        costText.text = "$" + cost;
        if (skillName.Equals("reducedShotCooldown"))
        {
            purchaseStatus = (int)(PlayerPrefs.GetFloat(skillName) * 10);
        }
        else
        {
            purchaseStatus = PlayerPrefs.GetInt(skillName);
        }

        if (purchaseStatus >= numOfPurchases)
        {
            meshRenderer.material = fullyPurchased;
        }
        if(purchaseStatus < numOfPurchases)
        {
            meshRenderer.material = unpurchased;
            thisCollider.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skillName.Equals("reducedShotCooldown"))
        {
            purchaseStatus = (int)(PlayerPrefs.GetFloat(skillName) * 10);
        }
        else
        {
            purchaseStatus = PlayerPrefs.GetInt(skillName);
        }
        if (purchaseStatus >= numOfPurchases)
        {
            meshRenderer.material = fullyPurchased;
            thisCollider.isTrigger = false;
        }
        if (purchaseStatus < numOfPurchases)
        {
            meshRenderer.material = unpurchased;
            thisCollider.isTrigger = true;
        }
    }

    //private void OnMouseDown()
    //{
    //    PurchaseSkill();
    //    Debug.Log("Mouse down");
    //}

    void PurchaseSkill()
    {
        if(skillName.Equals("Reset"))
        {
            PlayerPrefs.SetInt("penSkill", 0);
            PlayerPrefs.SetFloat("reducedShotCooldown", 0f);
            PlayerPrefs.SetInt("bonusChance", 0);
            Debug.Log("Reset");
        }
        else if(purchaseStatus < numOfPurchases && scoreManager.getScore() >= cost)
        {
            purchaseStatus++;
            Debug.Log("Purchased skill");
            if (skillName.Equals("reducedShotCooldown"))
            {
                PlayerPrefs.SetFloat(skillName, (purchaseStatus / 10f));
                Debug.Log((purchaseStatus / 10f));
            }
            else
            {
                PlayerPrefs.SetInt(skillName, purchaseStatus);
            }
            scoreManager.RemoveScore(cost);
        }
        Debug.Log("Purchased this many times: " + purchaseStatus);
    }

    bool isPurchaseable()
    {
        return (purchaseStatus < numOfPurchases && scoreManager.getScore() >= cost);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log("Collided");
            if (isPurchaseable())
            {
                PurchaseSkill();
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Not enough points"); // fix later to have pop up text showing this
            }
        }
    }
}
