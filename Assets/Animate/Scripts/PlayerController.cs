using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private Rigidbody2D rb2d;

    public ScoreController scoreController;
    public HealthController healthController;

    public GameOverController gameOverController;
    private bool isCrouching;

    

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }


    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        bool crouch = Input.GetKey(KeyCode.LeftControl); // Check for crouch key (Ctrl)

        // If the player is crouching, they cannot jump or move horizontally
        if (!crouch)
        {
            MoveCharacter(horizontal, vertical);
        }

        PlayMovementAnimation(horizontal, vertical, crouch);




    }

    private void MoveCharacter(float horizontal , float vertical)
    {
        Vector3 position = transform.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;

        if (vertical > 0)
        {
            rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }

    }

    private void PlayMovementAnimation(float horizontal , float vertical, bool crouch)
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));


        //Flipping the player

        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        

        if (vertical >0 )
        {
            playerAnimator.SetBool("Jump", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }

        // Handle crouch animation
        if (crouch)
        {
            isCrouching = true;
            playerAnimator.SetBool("Crouch", true);
        }
        else
        {
            isCrouching = false;
            playerAnimator.SetBool("Crouch", false);
        }
    }

    public void pickUpKey()
    {
        Debug.Log("Key is pickedup");
        ScoreController.Instance.IncreaseScore(10);
        //scoreController.IncreaseScore(10);
    }

    public void KillPlayer()
    {
        Debug.Log("Player hit by enemy! Reducing health.");

        if (healthController != null)
        {
            healthController.TakeDamage(1); // Reduce health by 1
        }
        else
        {
            Debug.LogError("HealthController reference is missing in PlayerController.");
        }

    }
}
