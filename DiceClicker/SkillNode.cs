using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SkillNode : MonoBehaviour
{
    public GameObject previous; // for visual lines
    public GameObject next; // for visual lines
    public string skillName;
    public bool purchaseStatus = false;
    public int price; // price of skill
    public GameManager gameManager;
    public GameObject button; // for changing color of button after purchase/unlock
    public TextMeshProUGUI skillPrice;
    public Material lockedSkill;
    public Material unpurchasedSkill;
    public Material purchasedSkill;
    //public int quantity; 
    //private int quantityPurchased;

    public Renderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (previous == null)
            meshRenderer.material = unpurchasedSkill;
        else if (purchaseStatus == true)
            meshRenderer.material = purchasedSkill;
        else
            meshRenderer.material = lockedSkill;

        skillPrice.text = "Cost: " + price;
        int skillStatusSaved = PlayerPrefs.GetInt(skillName, 0);
        if (skillStatusSaved == 0)
            purchaseStatus = false;
        else
            purchaseStatus = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (purchaseStatus == true)
        {
            skillPrice.gameObject.SetActive(false);
            meshRenderer.material = purchasedSkill;
        }
        //PlayerPrefs.SetInt(skillName, 0); // ONLY UNCOMMENT THIS TO RESET SKILL. Will figure out better system later
    }

    private void OnMouseDown()
    {
        if(purchaseStatus == false && gameManager.getScore() >= price)
        {
            purchaseStatus = true;
            PlayerPrefs.SetInt(skillName, 1);
            gameManager.UpdateScore(-price);
        }
    }
}
