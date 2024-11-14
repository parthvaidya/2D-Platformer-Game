using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem bombBlast;

    private float speed = 1.5f; // Speed of the enemy's movement
    //public float leftLimitX = -5f; // X position of the left boundary
    //public float rightLimitX = 5f;
    private bool movingRight = true; // Direction of movement
    [SerializeField] private Transform patrolPoint1; // Left boundary patrol point
    [SerializeField] private Transform patrolPoint2; 
    private Animator animator;

   private void Update()
    {
        Patrol();
        Debug.Log("Patrol is running");

    }

    private void Patrol()
    {
        // Move enemy in the current direction
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoint2.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoint2.position) < 0.1f) // Check if near the target
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoint1.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoint1.position) < 0.1f) // Check if near the target
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Flip the enemy's direction
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


    //private void Patrol()
    //{
    //    // Move enemy in the current direction
    //    if (movingRight)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightLimitX, transform.position.y), speed * Time.deltaTime);
    //        if (transform.position.x >= rightLimitX)
    //        {
    //            movingRight = false;
    //            Flip();
    //        }
    //    }
    //    else
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftLimitX, transform.position.y), speed * Time.deltaTime);
    //        if (transform.position.x <= leftLimitX)
    //        {
    //            movingRight = true;
    //            Flip();
    //        }
    //    }
    //}

    //private void Flip()
    //{
    //    // Flip the enemy's direction
    //    Vector3 localScale = transform.localScale;
    //    localScale.x *= -1;
    //    transform.localScale = localScale;
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.KillPlayer();
            SoundController.Instance.Play(Sounds.BombBlast);
            bombBlast.Play();
        }
    }


    
}
