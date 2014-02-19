/*
player class. controls the movement of the main character.

changes: change scene depends on which door the player collide with. -John Mai 1/12/2014
 */ 

/*
note: add id 3 and another list for rooms


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
	Vector2 targetPosition;
	Vector2 direction;
	Vector2 closestColl;
	public bool canWalk;



	void Start(){
		anim = GetComponent<Animator>();
		canWalk = true;
		mainCam = GameObject.Find("Main Camera").camera;
		GameObject moveCam = GameObject.Find ("moveCam");
			if(moveCam != null && transform.position.x < 1){
				mainCam.transform.Translate(new Vector3(-9.273237f, 0, 0));
				MoveCam.right = false;
			}
	}

	void FixedUpdate(){	
		if(canWalk){
			float distance;
			if(Input.GetMouseButton(0)){
				//get mouse clicked location and convert them to world point.
				targetPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, camera.nearClipPlane));
				targetPosition.x = mousePosition.x;
				targetPosition.y = mousePosition.y;
			}

			if (targetPosition.x != 0){
				//if something is on the way, use find path
				int layerMask = 1 << 10;
				layerMask = ~layerMask;
				if (Physics2D.Linecast(transform.position, targetPosition,layerMask)){	
					if(objectOnWay(targetPosition)){
						Vector2 toPoint = FindClosestPoint(targetPosition).transform.position;
						distance = Vector2.Distance (transform.position, toPoint);
						transform.position = Vector2.Lerp (transform.position, toPoint,Time.deltaTime* (maxSpeed/distance));
					}
				}
				//else go straight to that location
				else{
					distance = Vector2.Distance (transform.position, targetPosition);
					if(distance > 0){
						transform.position = Vector2.Lerp (transform.position, targetPosition,Time.deltaTime* (maxSpeed/distance));
					}
				}
			}
		}
	}

	//returns true if the collide object is type PolygonCollider2D
	public bool objectOnWay(Vector2 target){
		bool result = false;
		Vector2 direct;
		direct.x = target.x - transform.position.x;
		direct.y = target.y - transform.position.y;
		float dis = Vector2.Distance (transform.position, target);
		// Bit shift the index of the layer (8) to get a bit mask
		int layerMask = 1 << 10;
		// This would cast rays only against colliders in layer 8.
		// But instead we want to collide against everything except layer 8. 
		// The ~ operator does this, it inverts a bitmask.
		layerMask = ~layerMask;
		//Physics2D.IgnoreRaycastLayer
		RaycastHit2D tempHit = Physics2D.Raycast(transform.position, direct,dis,layerMask);
		if (tempHit.collider.GetType() == typeof(PolygonCollider2D) &&tempHit.collider.GetType()!= null){
			result = true;
		}
		return result;		 
	}
	//return closest pathPoint near player.
	GameObject FindClosestPoint(Vector2 target) {
		GameObject[] points;
		points = GameObject.FindGameObjectsWithTag("point");
		GameObject closest= null;
		float distance = Mathf.Infinity;
		Vector2 position = target;
		foreach (GameObject point in points) {
			if(!(point.transform.position.Equals(target))){
				float curDistance = Vector2.Distance(point.transform.position,position);
				if (curDistance < distance) {
					closest = point;
					distance = curDistance;
				}
			}
		}

		Debug.Log ("closet point: " + closest.transform.position);
		return closest;
	}
	//change scene when collide with door
	void OnTriggerEnter2D(Collider2D collider){
		DoorScript doorObj = collider.gameObject.GetComponent<DoorScript> ();
		SceneDoor doorObj2 = collider.gameObject.GetComponent<SceneDoor> ();
		string temp;
		int tempIndex;

		if (doorObj != null) {
			tempIndex = 0;
			if (doorObj.id == 0)
				tempIndex = GameManager.Instance.currentRoomIndex - 1;
			else if(doorObj.id ==1)
				tempIndex = GameManager.Instance.currentRoomIndex + 1;
			Debug.Log("Temp index:" + tempIndex);
			temp = (string) GameManager.Instance.roomIDList[tempIndex];
			Debug.Log("Room obtained:" + temp);
			GameManager.Instance.currentRoomIndex = tempIndex;
			GameManager.Instance.SetNextX(doorObj.x);
			GameManager.Instance.SetNextY(doorObj.y);
			DestoryPlayer();
			Application.LoadLevel (temp);
		}
		else if(doorObj2 != null) {
			if (doorObj2.id >1){
				tempIndex = doorObj2.id;
				temp = (string) GameManager.Instance.rooms[tempIndex];
			}
			else{
				tempIndex = GameManager.Instance.currentRoomIndex;
				temp = (string) GameManager.Instance.roomIDList[tempIndex];
			}
			GameManager.Instance.SetNextX(doorObj2.x);
			GameManager.Instance.SetNextY(doorObj2.y);
			DestoryPlayer();
			Application.LoadLevel (temp);
		}

	}

	//flips the sprite or animation
	void Flip(){
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	public void DestoryPlayer(){
		Destroy (Scene.player);
	}
}
	


/*
//old update function
void FixedUpdate(){
	float move = Input.GetAxis ("Horizontal");
	float moveVer = Input.GetAxis ("Vertical");
	rigidbody2D.velocity = new Vector2 (move * maxSpeed, moveVer * maxSpeed);
	anim.SetFloat("Speed",Mathf.Abs(move));
	
	if(move < 0 && !facingLeft)
		Flip();
	else if(move>0 && facingLeft)
		Flip();
}
*/