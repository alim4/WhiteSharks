/*
Loads the main character when the scene is created.
*/
using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	public static GameObject player;
	public float scaleX, scaleY;
	// Use this for initialization
	void Start () {
		string temp = (string)GameManager.Instance.GetMainCharacter ();
		player = (GameObject)Instantiate(Resources.Load((temp)));
		Vector2 tempVec = transform.position;
		tempVec.x = GameManager.Instance.GetNextX ();
		tempVec.y = GameManager.Instance.GetNextY ();
		player.transform.position = tempVec;
		//scale the player sprite
		if (scaleX != 0 && scaleY != 0) {
			player.transform.localScale = new Vector3(scaleX, scaleY, 1);	
		}
	}
}
