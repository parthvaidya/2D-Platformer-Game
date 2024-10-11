using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        Debug.Log("Player is awake");
    }
    private void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed" , Mathf.Abs(speed));
        Vector3 scale = transform.localScale;
        if (speed <0 )
        {
            
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (speed > 0) 
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}