using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatformDeath : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the GameOverController

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameOverUI.SetActive(true);
    }
}
