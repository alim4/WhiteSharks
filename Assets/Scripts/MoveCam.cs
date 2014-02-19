using UnityEngine;
using System.Collections;

public class MoveCam : MonoBehaviour {
	public Camera mainCam;
	// Use this for initialization
	public static bool right;

	void Start(){
		right = true;
	}

	void OnTriggerEnter2D(Collider2D collider){
		playerScript playerObj = collider.gameObject.GetComponent<playerScript> ();

		if (playerObj != null) {
			if(right){
				mainCam.transform.Translate(new Vector3(-9.273237f, 0, 0));
				right = false;
			}
			else{
				mainCam.transform.Translate(new Vector3(9.273237f, 0, 0));
				right = true;
			}
		}
	}


}
