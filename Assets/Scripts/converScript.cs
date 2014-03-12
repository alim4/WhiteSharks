/*
conversation class. display the conversation sprite.

*/

using UnityEngine;
using System.Collections;

public class converScript : MonoBehaviour {
	//close the conversation sprite.
	void OnMouseDown(){
		if (Input.GetMouseButton (0)) {
			renderer.enabled = false;
			collider2D.enabled = false;
		}
	}
}
