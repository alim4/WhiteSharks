// @author John Cotrel
// Accusation Scene

using UnityEngine;
using System.Collections;

public class accusationScript : MonoBehaviour {
	//currently selected nouns
	private int selectSuspect = -1;
	private int selectWeapon = -1;
	private int selectRoom = -1;

	public GameObject mapButton1, mapButton2, suspectButton1, suspectButton2,
	weaponButton1, weaponButton2, backButton;// alexiaChat;

	private Color clear = Color.clear;
	private Color original;

	private string hint = "";
	private string hint0 = "you're wrong, sweetie...";
	private string hint1 = "you're on the right track...";
	private string hint2 = "you're almost there...";
	private string hint3 = "you solved it.";

	//gonna need to do some calls to get this
	//but for now default to 1
	private int answerSuspect = 1;
	private int answerWeapon = 1;
	private int answerRoom = 1;

	
	/* FUNCTIONS */

	// Use this for initialization
	void Start () {
		//declaring buttons and their values
		mapButton1 = GameObject.Find("buttonMap1");
		mapButton1.GetComponent<buttonScript>().setTypeNum(2, 0);
		mapButton2 = GameObject.Find("buttonMap2");
		mapButton2.GetComponent<buttonScript>().setTypeNum(2, 1);
		suspectButton1 = GameObject.Find("buttonSuspect1");
		suspectButton1.GetComponent<buttonScript>().setTypeNum(0, 0);
		suspectButton2 = GameObject.Find("buttonSuspect2");
		suspectButton2.GetComponent<buttonScript>().setTypeNum(0, 1);
		weaponButton1 = GameObject.Find("buttonWeapon1");
		weaponButton1.GetComponent<buttonScript>().setTypeNum(1, 0);
		weaponButton2 = GameObject.Find("buttonWeapon2");
		weaponButton2.GetComponent<buttonScript>().setTypeNum(1, 1);
		backButton = GameObject.Find("goBackButton");
		backButton.GetComponent<buttonScript>().setTypeNum(3, 666);
		//alexiaChat = GameObject.Find("alexiaSays");
		//original = alexiaChat.GetComponent<SpriteRenderer>().color;
		//alexiaChat.GetComponent<SpriteRenderer>().color = clear;

		//guiText.font.material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//use this later when dealing with case generation
	void getSuspects() {

	}
	void getWeapons() {

	}
	void getRooms() {

	}

	//changes selected noun
	public void setSelected(int selected, int type) {
		if (type == 0) {
			selectSuspect = selected;
		}
		else if (type == 1) {
			selectWeapon = selected;
		}
		else selectRoom = selected;
	}

	public int showSuspect() {
		return selectSuspect;
	}

	public int showWeapon() {
		return selectWeapon;
	}

	public int showRoom() {
		return selectRoom;
	}

	public void OnMouseDown() {
		if (Input.GetMouseButton(0)) {
			//alexiaChat.GetComponent<SpriteRenderer>().color = original;
			//hint = "you're wrong!";

			//we have to generate a hint and set the chat to show
			int progress = 0;
			if (selectSuspect == answerSuspect) progress++;
			if (selectRoom == answerRoom) progress++;
			if (selectWeapon == answerWeapon) progress++;
			if (progress == 0) {
				hint = hint0;
			}
			else if (progress == 1) {
				hint = hint1;
			}
			else if (progress == 2) {
				hint = hint2;
			}
			else if (progress == 3) {
				hint = hint3;
			}
			else {
				print("shouldn't be reaching progress="+progress);
			}
		}
	}

	void OnGUI() {
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(365, 10, 100, 100), hint);
	}
}
