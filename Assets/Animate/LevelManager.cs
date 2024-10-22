using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure LevelManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance of LevelManager
        }
    }

    private void Start()
    {
        // Log LevelManager initialization
        Debug.Log("LevelManager initialized!");

        // Initialize Level 1 as Unlocked
        if (GetLevelStatus("Level1") == LevelStatus.Locked)
        {
            SetLevelStatus("Level1", LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string levelName)
    {
        return (LevelStatus)PlayerPrefs.GetInt(levelName, (int)LevelStatus.Locked); // Default to Locked
    }

    public void SetLevelStatus(string levelName, LevelStatus status)
    {
        PlayerPrefs.SetInt(levelName, (int)status);
        PlayerPrefs.Save();  // Ensure the status is saved
    }

    public void SetCurrentLevelComplete()
    {
        // Mark the current level as Completed
        string currentLevelName = SceneManager.GetActiveScene().name;
        SetLevelStatus(currentLevelName, LevelStatus.Completed);

        // Unlock the next level (if any)
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string nextLevelName = "Level" + nextLevelIndex;

        if (nextLevelIndex <= 5) // Assuming you have 5 levels
        {
            SetLevelStatus(nextLevelName, LevelStatus.Unlocked);
        }
    }
}
