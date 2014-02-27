using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public GameObject contBtn;
	// Use this for initialization
	void Start () {
		UIEventListener.Get (contBtn).onClick += this.onClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onClick(GameObject button){
		Application.LoadLevel ("stage1");
	}
}
