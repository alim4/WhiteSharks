﻿/*
Loads the main character when the scene is created.
*/
using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	public static GameObject player, backEffect;
	public float scaleX, scaleY;
	public float maxY, minY;
	public float minScale, maxScale;
	public float baseSpeed;

	public int id;
	// Use this for initialization
	void Start () {
		backEffect = (GameObject)Instantiate(Resources.Load("effect"));
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
		player.GetComponent<playerScript> ().currentRoom = this.id;
		player.GetComponent<playerScript> ().scaleInfo = new float[4]{minScale, maxScale, minY, maxY};
		player.GetComponent<playerScript> ().baseSpeed = baseSpeed;
		player.GetComponent<playerScript> ().minSpeed = baseSpeed;

		GameManager.Instance.updateMouseIcon ("Walk_Icon");
	}
}
