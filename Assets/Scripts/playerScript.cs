/*
player class. controls the movement of the main character.

changes: change scene depends on which door the player collide with. -John Mai 1/12/2014
 */ 
using UnityEngine;
using System.Collections;

public class playerScript : CaseElement {

	//public KeyCode moveLeft, moveRight, moveTop, moveBottom;
	public float maxSpeed = 8f;
	public Camera mainCam;
	public Transform mainChar;
	bool facingLeft = true;
	Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}
	////Move the camera to the next scene when collides with the door object.
	void OnTriggerEnter2D(Collider2D collider){
		doorScript doorObj = collider.gameObject.GetComponent<doorScript> ();
		if (doorObj != null) {

			float newX = doorObj.newX;
			float newY = doorObj.newY;
			float sceneX = doorObj.sceneX;
			float sceneY = doorObj.sceneY;
			float sceneZ = doorObj.sceneZ;

			//move the player to the next scene.
			Vector3 temp = transform.position; 
			temp.x = newX;
			temp.y = newY;
			mainChar.position = temp;
			//move the camera to the next scene
			Vector3 temp2 = new Vector3(sceneX,sceneY,sceneZ);
			mainCam.transform.position = temp2;
		}
	}

	void FixedUpdate(){
		float move = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
		anim.SetFloat("Speed",Mathf.Abs(move));

		if(move < 0 && !facingLeft)
			Flip();
		else if(move>0 && facingLeft)
			Flip();
	}

	//flips the sprite or animation
	void Flip(){
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
