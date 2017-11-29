using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // RigidBody component instance for the player
    private Rigidbody2D playerRigidBody2D;
    //Variable to track how much movement is needed from input
    private float movePlayerVector;
    // For determining which way the player is currently facing.
    private bool facingRight;
    // Speed modifier for player movement
    public float speed = 4.0f;
    //Reference to the player's animator component.
    private Animator anim;

    //Initialize any component references
    void Awake()
    {
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
		anim = (Animator)GetComponent (typeof(Animator));
    }
    // Update is called once per frame
    void Update()
    {
        // Get the horizontal input.
        movePlayerVector = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(movePlayerVector));
        playerRigidBody2D.velocity = new Vector2(movePlayerVector *
        speed, playerRigidBody2D.velocity.y);
        if (movePlayerVector > 0 && !facingRight)
        {
            Flip();
        }
        else if (movePlayerVector < 0 && facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        // Switch the way the player is labeled as facing.
        facingRight = !facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
