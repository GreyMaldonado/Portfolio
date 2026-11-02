using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentState = GameState.Playing;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public enum GameState
    {
        Playing,
        Paused
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(currentState == GameState.Playing)
            {
                PauseGame();
                Debug.Log("Game State: " + currentState);
            }
            else if(currentState == GameState.Paused)
            {
                ResumeGame();
                Debug.Log("Game State: " + currentState);
            }
        }

        if(Keyboard.current.rKey.wasPressedThisFrame && currentState == GameState.Paused)
        {
            Debug.Log("Restarting game...");
            RestartGame();
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("bonusChance", 0);
        PlayerPrefs.SetInt("penSkill", 0);
        PlayerPrefs.SetInt("reducedShotCooldown", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
