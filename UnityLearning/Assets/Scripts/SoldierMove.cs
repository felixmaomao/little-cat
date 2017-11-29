using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMove : MonoBehaviour {

	// RigidBody component instance for the player
	private Rigidbody2D playerRigidBody2D;
	//Variable to track how much movement is needed from input
	private float movePlayerVector;
	// For determining which way the player is currently facing.
	private bool facingRight=true;
	// Speed modifier for player movement
	public float speed = 400f;
	public float bombSpeed=1000f;
	public float jumpSpeed = 400f;
	//是否在地面上
	private bool grounded = true;
	//animation
	private Animator anim;
	//Initialize any component references
	private AudioClip slideAudioClip;
	private AudioClip jumpAudioClip;
	public GameObject fish;
	void Awake()
	{
		playerRigidBody2D = (Rigidbody2D)GetComponent
			(typeof(Rigidbody2D));
		anim = (Animator)GetComponent (typeof(Animator));
		slideAudioClip = Resources.Load ("slide") as AudioClip;
		jumpAudioClip = Resources.Load ("jump") as AudioClip;
	}


	// Update is called once per frame
	void Update () {
		backToIdle ();
		// Get the horizontal input.
		movePlayerVector = Input.GetAxis("Horizontal");
		anim.SetFloat ("speed",Mathf.Abs(movePlayerVector));
		playerRigidBody2D.AddForce(new Vector2(movePlayerVector*speed,0));
		if (movePlayerVector > 0 && !facingRight)
		{
			Flip();
		}
		else if (movePlayerVector < 0 && facingRight)
		{
			Flip();
		}
		if(Input.GetKeyDown(KeyCode.C)){
			Jump ();		
		}
		if(Input.GetKeyDown(KeyCode.Z)){
			Slide ();		
		}
		if(Input.GetKeyDown(KeyCode.X)){
			fire ();
		}
	}

	void backToIdle(){
		if(!Input.anyKey){
			anim.SetFloat ("speed",0.0f);
			//anim.SetBool ("jump",false);
			anim.SetBool ("slide",false);
			//anim.SetBool ("bombAttack",false);
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

	void Jump(){
		if(this.grounded){
			playerRigidBody2D.AddForce (new Vector2(0,jumpSpeed));	
			grounded = false;
			//music
			anim.SetBool("jump",true);
			if(jumpAudioClip!=null){
				AudioSource audioSource = GetComponent<AudioSource>();
				audioSource.clip = jumpAudioClip;
				audioSource.time = 0;
				audioSource.Play ();
			}
		}
	}

	void Slide(){
		if(this.grounded){
			if (facingRight) {
				playerRigidBody2D.AddForce (new Vector2 (speed * 5, 0));
			} else {
				playerRigidBody2D.AddForce (new Vector2(-speed * 5,0));
			}
			anim.SetBool ("slide",true);
			if(slideAudioClip!=null){
				AudioSource audioSource = GetComponent<AudioSource>();
				audioSource.clip = slideAudioClip;
				audioSource.time = 0;
				audioSource.Play ();
			}
		}
	}

	void fire(){
		GameObject player = GameObject.Find ("player");
		GameObject fishBomb = Instantiate (fish,player.transform.localPosition,player.transform.localRotation);
		if (facingRight) {
			fishBomb.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (bombSpeed, 0));
			Vector3 theScale = fishBomb.transform.localScale;
			theScale.x *= -1;
			fishBomb.transform.localScale = theScale;
		} else {
			fishBomb.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-bombSpeed, 0));
		}
	}
		
}
