using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;
    public Rigidbody2D myRigidbody2D;
    public HealthBase healthBase;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public Physics2D physics2D;
    public ParticleSystem jumpVFX;
    public float distToGround;
    public float spaceToGround = 0.1f;

    private float _currentspeed;
    private Animator _currentPlayer;

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if(collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
    }

    private void Update()
    {
        CheckPlayerIsGrounded();
        HandlePlayerMovement();
        HandlePlayerJump();
    }

    private bool CheckPlayerIsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill += OnPlayerKill;

        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath); 
    }

    private void HandlePlayerMovement()
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
            myRigidbody2D.transform.DOScaleX(-1, .1f);
            
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidbody2D.MovePosition(myRigidbody2D.position + velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(_currentspeed, myRigidbody2D.velocity.y);
            myRigidbody2D.transform.DOScaleX(1, .1f);
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

    private void HandlePlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckPlayerIsGrounded())
        {
            myRigidbody2D.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidbody2D.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidbody2D.transform);

            HandlePlayerJumpScale();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
        //if(jumpVFX != null) jumpVFX.Play();
    }

    private void HandlePlayerJumpScale()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.transform.DOScaleX(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}