using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    public float speed;
    public float jump;

    private Rigidbody2D rb2d;

    public ScoreController scoreController;
    public HealthController healthController;

    public GameOverController gameOverController;
    private bool isCrouching;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;
    public float maxJumpTime = 0.5f; 

    //Take a reference from inspector.

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }


    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        bool crouch = Input.GetKey(KeyCode.LeftControl); // Check for crouch key (Ctrl)
        isGrounded = Mathf.Abs(rb2d.velocity.y) < 0.1f;
        // If the player is crouching, they cannot jump or move horizontally
        if (!crouch)
        {
            MoveCharacter(horizontal, vertical);
        }
        HandleJump();
        PlayMovementAnimation(horizontal, vertical, crouch);




    }

    private void MoveCharacter(float horizontal , float vertical)
    {
        Vector3 position = transform.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;

        //if (vertical > 0)
        //{
        //    rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        //}

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        //}

    }

    private void HandleJump()
    {
        // Start jump if player is grounded and jump button is pressed
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jump); // Apply jump force
            playerAnimator.SetBool("Jump", true); // Trigger jump animation
        }

        // If holding jump button and within max jump time, allow continued jump
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jump);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // End jump if jump button is released
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        // Reset jump animation when grounded
        if (isGrounded && !isJumping)
        {
            playerAnimator.SetBool("Jump", false);
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



        //if (vertical >0 )
        //{
        //    playerAnimator.SetBool("Jump", true);
        //}
        //else
        //{
        //    playerAnimator.SetBool("Jump", false);
        //}

        playerAnimator.SetBool("Jump", !isGrounded);

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
