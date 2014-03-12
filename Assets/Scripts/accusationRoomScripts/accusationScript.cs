// @author John Cotrel
// Accusation Scene

using UnityEngine;
using System.Collections;

public class accusationScript : MonoBehaviour {
	//currently selected nouns
	public NPC selectSuspect = null;
	public CaseObject selectWeapon = null;
	private string selectRoom = "";

	public GameObject mapButton1, mapButton2, suspectButton1, suspectButton2,
	weaponButton1, weaponButton2, backButton;// alexiaChat;

	private Color clear = Color.clear;
	private Color original;

	private string hint = "";
	private string[] hints = {
				"you're wrong, sweetie...",
				"you're on the right track...",
				"you're almost there...",
				"you solved it."
		};
	private string hint0 = "you're wrong, sweetie...";
	private string hint1 = "you're on the right track...";
	private string hint2 = "you're almost there...";
	private string hint3 = "you solved it.";

	//gonna need to do some calls to get this
	//but for now default to 1
	private NPC answerSuspect;
	private CaseObject answerWeapon;
	private string answerRoom;

	
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
		answerSuspect = GameManager.guilty;
		answerWeapon = GameManager.weapon;
		answerRoom = GameManager.room;
		Debug.Log(GameManager.theCase);

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
			selectSuspect = GameManager.npcList[selected];
			Debug.Log ("selected suspect " + selectSuspect.elementName);
		}
		else if (type == 1) {
			selectWeapon = GameManager.weaponList[selected];
			Debug.Log ("selected weapon " + selectWeapon.elementName);
		}
		else selectRoom = GameManager.roomList[selected];
		Debug.Log ("selected room " + selectRoom);
	}

	public NPC showSuspect() {
		return selectSuspect;
	}

	public CaseObject showWeapon() {
		return selectWeapon;
	}

	public string showRoom() {
		return selectRoom;
	}

	public void OnMouseDown() {
		if (Input.GetMouseButton(0)) {
			//alexiaChat.GetComponent<SpriteRenderer>().color = original;
			//hint = "you're wrong!";

			//we have to generate a hint and set the chat to show
			int progress = 0;

			if (selectSuspect.elementName.CompareTo(answerSuspect.elementName) == 0) progress++;
			if (selectRoom.CompareTo(answerRoom) == 0) progress++;
			if (selectWeapon.elementName.CompareTo(answerWeapon.elementName) == 0) progress++;
			Debug.Log("Progress: " + progress);
			hint = hints[progress];
		}
	}

	void OnGUI() {
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(365, 10, 100, 100), hint);
	}
}
