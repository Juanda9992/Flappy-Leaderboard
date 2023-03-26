using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Moving_Spike : MonoBehaviour
{
    [SerializeField] private bool isLeft;
    private bool hasMoved = false;
    
    [Header("Spike Movement")]
    [SerializeField]private float moveSpeed;
    [SerializeField]private float initXPos;

    [Header("Spike Animation")]
    [SerializeField]private Ease animationType;

    private void Start() 
    {
        initXPos = transform.localPosition.x;   
    }
    private void MoveSpike()
    {
        if(CanMove())
        {
            if(isLeft)
            {
                if(!hasMoved)
                {
                    hasMoved = true;
                    transform.DOLocalMoveX(initXPos + 0.6f, moveSpeed).SetEase(animationType);
                    
                }
                else
                {
                    hasMoved = false;
                    transform.DOLocalMoveX(initXPos,moveSpeed).SetEase(animationType);
                }
            }
            else
            {
                if(!hasMoved)
                {
                    hasMoved = true;
                    transform.DOLocalMoveX(initXPos - 0.6f, moveSpeed).SetEase(animationType);
                    
                }
                else
                {
                    hasMoved = false;
                    transform.DOLocalMoveX(initXPos,moveSpeed).SetEase(animationType);
                }
            }

        }
    } 

    private void ResetSpike()
    {
        hasMoved = false;
        transform.DOLocalMoveX(initXPos,moveSpeed).SetEase(animationType);
    }

    private bool CanMove()
    {
        return Random.Range(0,100) >= 35;
    }

    private void OnEnable() 
    {
        Player_Movement.OnPlayerTouchWall += MoveSpike;
        Player_Movement.OnPlayerDeath += ResetSpike;
    }
    private void OnDisable()
    {
        Player_Movement.OnPlayerTouchWall -= MoveSpike;
        Player_Movement.OnPlayerDeath -= ResetSpike;    
    }
}
