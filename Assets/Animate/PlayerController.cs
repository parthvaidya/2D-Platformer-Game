using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    public float speed;
    public float jumpForce = 7f; // Adjust jump force as needed
    public bool isGrounded;
    //Take a reference from inspector.
   

   

    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        MoveCharacter(horizontal);
        PlayMovementAnimation(horizontal);

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            Jump();
        }


    }

    private void MoveCharacter(float horizontal)
    {
        Vector3 position = transform.position;
        position.x = position.x + horizontal * speed * Time.deltaTime;
        transform.position = position;

    }

    private void PlayMovementAnimation(float horizontal)
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

        Input.GetAxisRaw("Vertical");
        Input.GetKeyDown(KeyCode.Space);
    }

    private void Jump()
    {
        // Trigger the jump animation
        playerAnimator.SetBool("Jump", true);

       

        
        isGrounded = false;

        
    }

    


}
