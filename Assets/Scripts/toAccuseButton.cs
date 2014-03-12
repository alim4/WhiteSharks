using UnityEngine;
using System.Collections;

public class toAccuseButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown() {
		if (Input.GetMouseButton(0)) {
			DontDestroyOnLoad(GameManager.Instance);
			Application.LoadLevel("accusation");
		}
	}
}
