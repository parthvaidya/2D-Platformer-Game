using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private bool[] unlockedLevels;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeLevels();
    }

    // Initialize level unlocking system (level 1 is unlocked by default)
    private void InitializeLevels()
    {
        int totalLevels = 3; // Assume 3 levels including the lobby
        unlockedLevels = new bool[totalLevels];

        // Load from PlayerPrefs
        for (int i = 0; i < totalLevels; i++)
        {
            unlockedLevels[i] = PlayerPrefs.GetInt("LevelUnlocked_" + i, i == 1 ? 1 : 0) == 1;
        }
    }

    // Method to check if a level is unlocked
    public bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= unlockedLevels.Length)
        {
            return false;
        }
        return unlockedLevels[levelIndex];
    }

    // Method to unlock a level
    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex < unlockedLevels.Length)
        {
            unlockedLevels[levelIndex] = true;
            PlayerPrefs.SetInt("LevelUnlocked_" + levelIndex, 1);
            PlayerPrefs.Save();
        }
    }

    // Load any level by index, but ensure it's unlocked first
    public void LoadAnyLevel(int levelIndex)
    {
        if (IsLevelUnlocked(levelIndex))
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogError("Level " + levelIndex + " is locked!");
        }
    }

    // Call this after finishing a level to unlock the next one
    public void CompleteLevel(int currentLevelIndex)
    {
        if (currentLevelIndex + 1 < unlockedLevels.Length)
        {
            UnlockLevel(currentLevelIndex + 1);
        }
    }
}
