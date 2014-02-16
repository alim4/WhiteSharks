/*
Loads the main character when the scene is created.
*/
using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	public static GameObject player;
	public int id;
	// Use this for initialization
	void Start () {
		string temp = (string)GameManager.Instance.GetMainCharacter ();
		player = (GameObject)Instantiate(Resources.Load((temp)));
		Vector2 tempVec = transform.position;
		tempVec.x = GameManager.Instance.GetNextX ();
		tempVec.y = GameManager.Instance.GetNextY ();
		player.transform.position = tempVec;
		switch (id) {
			case 2:
				player.transform.localScale = new Vector3(0.37f,0.37f,0.37f);
				break;
			case 3:
				player.transform.localScale = new Vector3(0.48f,0.48f,0.48f);
				break;
			case 4:
				player.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
				break;
			case 5:
				player.transform.localScale = new Vector3(1f,1f,1f);
				break;
			case 6:
				player.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
				break;
		}
	}
}
