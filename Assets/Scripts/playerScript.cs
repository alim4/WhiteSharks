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
		DoorScript doorObj = collider.gameObject.GetComponent<DoorScript> ();

		if (doorObj != null) {
			string temp;
			int tempIndex = 0;
			if (doorObj.id == 0)
				tempIndex = GameManager.Instance.currentRoomIndex - 1;
			else if(doorObj.id ==1)
				tempIndex = GameManager.Instance.currentRoomIndex + 1;
			Debug.Log("Temp index:" + tempIndex);
			temp = (string) GameManager.Instance.roomIDList[tempIndex];
			Debug.Log("Room obtained:" + temp);
			GameManager.Instance.currentRoomIndex = tempIndex;
			Application.LoadLevel (temp);
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
