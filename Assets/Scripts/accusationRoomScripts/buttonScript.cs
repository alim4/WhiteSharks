using UnityEngine;
using System.Collections;

public class buttonScript : MonoBehaviour {

	private int type;
	private int number;

	public int currentSuspect = 0;
	private int currentWeapon = 0;
	private int currentRoom = 0;

	private Color original;

	public GameObject buttonAccuse, backButton1, backButton2, backButton3;

	// Use this for initialization
	void Start () {
		buttonAccuse = GameObject.Find("accusebutton");
		backButton1 = GameObject.Find("buttonBack1");
		backButton2 = GameObject.Find("buttonBack2");
		backButton3 = GameObject.Find("buttonBack3");
		original = GetComponent<SpriteRenderer>().color;
	}

	// Update is called once per frame
	void Update () {
	}

	public void setTypeNum(int typ, int numb) {
		type = typ;
		number = numb;
	}

	/*public Vector3 buttonPos() {
		return transform.position;
	}*/

	public void OnMouseEnter() {
		GetComponent<SpriteRenderer>().color = Color.gray;
	}
	public void OnMouseExit() {
		GetComponent<SpriteRenderer>().color = original;
	}


	public void OnMouseDown(){
		if(Input.GetMouseButton(0)){
			if (type < 3) {
				buttonAccuse.GetComponent<accusationScript>().setSelected(number, type);
				//printTest();
				if (type == 0) {
					backButton1.GetComponent<backButtonScript>().setMotion(transform.position);
				}
				else if (type == 1) {
					backButton2.GetComponent<backButtonScript>().setMotion(transform.position);
				}
				else {
					backButton3.GetComponent<backButtonScript>().setMotion(transform.position);
				}
			}
			else {
				goBack();
			}
		}
	}

	//this is only for testing purposes
	/*void printTest(){
		string test;
		if (type == 0) {
				test = "suspect!";
			}
			else if (type == 1) {
				test = "weapon!";
			}
			else test = "location!";
			print(test+" #"+number);
			getSuspicions();
			print("currently selected SUSPECT:"+currentSuspect+" WEAPON:"+currentWeapon+" ROOM:"+currentRoom);
	}

	//this too is just for testing purposes
	/*void getSuspicions() {
		currentSuspect = buttonAccuse.GetComponent<accusationScript>().showSuspect();
		currentWeapon = buttonAccuse.GetComponent<accusationScript>().showWeapon();
		currentRoom = buttonAccuse.GetComponent<accusationScript>().showRoom();
	}*/

	//to go back to the case without accusing anything
	void goBack() {
		//it goes back to stage1 for now
		DontDestroyOnLoad(GameManager.Instance);
		Application.LoadLevel("stage1");
	}

}
