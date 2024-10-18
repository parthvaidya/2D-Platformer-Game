using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("Level finished");
            //LevelManager.instance.CompleteLevel(SceneManager.GetActiveScene().buildIndex);
            SceneController.instance.nextLevel();
        }
    }
}
