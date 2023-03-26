using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float playerJumpHeight;
    private bool isJumping = false;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0) && Match_State.GameRunning)
        {
            isJumping = true;
        }    
    }

    private void FixedUpdate() 
    {
        if(isJumping)
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x,playerJumpHeight);
        }    
    }
}
