using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    //public Animator animator;

    private float _currentspeed;
    private bool _isRunning = false;

    private Animator _currentPlayer;

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill += OnPlayerKill;

        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath); 
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            _currentspeed = soPlayerSetup.speedRun; 
        }
        else
        {
            _currentspeed = soPlayerSetup.speed;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidbody2D.MovePosition(myRigidbody2D.position - velocity * Time.deltaTime
            myRigidbody2D.velocity = new Vector2(-_currentspeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.transform.localScale.x != -1);
            {
                myRigidbody2D.transform.DOScaleX(-1, .1f);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidbody2D.MovePosition(myRigidbody2D.position + velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(_currentspeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.transform.localScale.x != -1)
            {
                myRigidbody2D.transform.DOScaleX(1, .1f);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }


        if(myRigidbody2D.velocity.x > 0)
        {
            myRigidbody2D.velocity += soPlayerSetup.friction;
        }

        else if(myRigidbody2D.velocity.x < 0)
        {
            myRigidbody2D.velocity -= soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidbody2D.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidbody2D.transform);

            HandleJumpScale();
        }
    }

    private void HandleJumpScale()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.transform.DOScaleY(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
            myRigidbody2D.transform.DOScaleX(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}