using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelButtonController : MonoBehaviour
{

    public GameObject level1Button; // Assign the Level 1 Button GameObject in the Unity editor
    public GameObject level2Button; // Assign the Level 2 Button GameObject in the Unity editor

    private void Start()
    {
        // Enable/Disable buttons based on whether the levels are unlocked
        level1Button.SetActive(LevelManager.instance.IsLevelUnlocked(1));
        level2Button.SetActive(LevelManager.instance.IsLevelUnlocked(2));
    }

    // Method for loading Level 1 (build index 1)
    public void LoadLevel1()
    {
        if (LevelManager.instance.IsLevelUnlocked(1))
        {
            SceneManager.LoadScene(1); // Load Level 1 if unlocked
        }
        else
        {
            Debug.Log("Level 1 is locked!");
        }
    }

    // Method for loading Level 2 (build index 2)
    public void LoadLevel2()
    {
        if (LevelManager.instance.IsLevelUnlocked(2))
        {
            SceneManager.LoadScene(2); // Load Level 2 if unlocked
        }
        else
        {
            Debug.Log("Level 2 is locked!");
        }
    }

    // Method for loading Level 1 (build index 1)
    //public void LoadLevel1()
    //{

    //    LevelManager.instance.LoadAnyLevel(1);
    //    //SceneManager.LoadScene(1); // Load Level 1 (build index 1)
    //}

    //// Method for loading Level 2 (build index 2)
    //public void LoadLevel2()
    //{
    //    LevelManager.instance.LoadAnyLevel(2);
    //    //SceneManager.LoadScene(2); // Load Level 2 (build index 2)
    //}
}
