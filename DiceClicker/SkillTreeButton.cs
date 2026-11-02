using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SkillTreeButton : MonoBehaviour
{

    public Button sceneButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneButton.onClick.AddListener(() => NextScene()); 
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //    SceneManager.LoadScene("Purchase Tree");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Skill Tree");
    }
        
}
