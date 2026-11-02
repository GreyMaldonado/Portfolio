using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public static LevelManager instance;
    public SpawnManager[] spawners;
    public Button levelButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelButton.onClick.AddListener(() => NextLevel());
        level = PlayerPrefs.GetInt("level"); // Comment out to start a new game for testing
    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawners[0].levelStatus() && spawners[1].levelStatus() && spawners[2].levelStatus() && GameObject.FindGameObjectsWithTag("Target").Length == 0)
        {
            LevelEnd();
        }
        PlayerPrefs.SetInt("level", level);
    }

    public int getLevel()
    {
        return level;
    }

    public void incLevel()
    {
        level++;
    }

    public void NextLevel()
    {
        incLevel();
        for (int i = spawners.Length - 1; i >= 0; i--)
        {
            spawners[i].levelStart(level);
            
        }

        levelButton.gameObject.SetActive(false);
    }

    void LevelEnd()
    {
        levelButton.gameObject.SetActive(true);

    }
}
