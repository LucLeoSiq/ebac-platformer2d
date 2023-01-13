using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 10.0f;
    private float _currentspeed;

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            _currentspeed = speedRun; 
        }
        else
        {
            _currentspeed = speed;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidbody2D.MovePosition(myRigidbody2D.position - velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(-_currentspeed, myRigidbody2D.velocity.y);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidbody2D.MovePosition(myRigidbody2D.position + velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(_currentspeed, myRigidbody2D.velocity.y);
        }

        if(myRigidbody2D.velocity.x > 0)
        {
            myRigidbody2D.velocity += friction;
        }

        else if(myRigidbody2D.velocity.x < 0)
        {
            myRigidbody2D.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * forceJump;
        }
    }
}