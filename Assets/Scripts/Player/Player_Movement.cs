using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player_Movement : MonoBehaviour
{
    public static Action OnPlayerTouchWall;
    public static Action OnPlayerDeath;
    private int Axis = 1;
    [SerializeField] private float playerSpeed;
    private Rigidbody2D rb;
    private bool canMove = false;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();    
        rb.isKinematic = true;
    }
    private void FixedUpdate() 
    {
        if(canMove)
        {
            rb.velocity = new Vector2(playerSpeed * Axis,rb.velocity.y);
        }
    }

    private void Update() 
    {
        if(!canMove && Input.GetMouseButtonDown(0) && Match_State.GameRunning)
        {
            canMove = true;
            rb.isKinematic = false;
        }    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.CompareTag("Spike"))
        {
            OnPlayerDeath?.Invoke();
            Death();
            return;
        }
        OnPlayerTouchWall?.Invoke();
        Axis *= -1;    
    }
    private void Death()
    {
        canMove = false;
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
}
