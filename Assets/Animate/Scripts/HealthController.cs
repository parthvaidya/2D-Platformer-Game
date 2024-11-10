using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    private static HealthController instance;
    public static HealthController Instance { get { return instance; } }

    private TextMeshProUGUI healthText;
    private int maxHealth = 3;
    private int currentHealth;
    private const string HealthKey = "PlayerHealth";

    public GameOverController gameOverController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
            LoadHealth(); // Load health from PlayerPrefs
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }

        healthText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
        else
        {
            SaveHealth(); // Save health to PlayerPrefs
            RefreshUI();
        }
    }

    private void SaveHealth()
    {
        PlayerPrefs.SetInt(HealthKey, currentHealth);
        PlayerPrefs.Save();
    }

    private void LoadHealth()
    {
        currentHealth = PlayerPrefs.GetInt(HealthKey, maxHealth); // Load current health
    }

    private void PlayerDies()
    {
        Debug.Log("Player died! Reloading scene...");
        gameOverController.ShowGameOver(); // Show Game Over UI
        gameObject.SetActive(false);
    }

    private void RefreshUI()
    {
        healthText.text = "Health: " + currentHealth; // Update the health text
    }


    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteKey("PlayerScore");
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.Save();
    }
    //private TextMeshProUGUI healthText;
    //private int maxHealth = 3;
    //private int currentHealth;

    //public GameOverController gameOverController;

    //private void Awake()
    //{
    //    healthText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component
    //}

    //private void Start()
    //{
    //    currentHealth = maxHealth; // Initialize the player's health to maxHealth
    //    RefreshUI();
    //}

    //// Call this method to reduce the player's health
    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage; // Decrease health by damage amount

    //    if (currentHealth <= 0)
    //    {
    //        PlayerDies();
    //    }
    //    else
    //    {
    //        RefreshUI(); // Update the health display if the player is still alive
    //    }
    //}

    //private void PlayerDies()
    //{
    //    Debug.Log("Player died! Reloading scene...");
    //    gameOverController.ShowGameOver(); // Show the Game Over UI
    //    gameObject.SetActive(false);
    //}

    //private void RefreshUI()
    //{
    //    healthText.text = "Health: " + currentHealth; // Update the health text
    //}
}
