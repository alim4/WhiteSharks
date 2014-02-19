using UnityEngine;
using System.Collections;

public class CharacterSetup : MonoBehaviour {
	public float scaleX, scaleY;
	private GameObject mainChar;

	// Use this for initialization
	void Start () {
		mainChar = GameObject.Find("player");
		if (mainChar != null)
			mainChar.transform.localScale = new Vector3(scaleX, scaleY, 1);
		else
			Start();

	}

}
