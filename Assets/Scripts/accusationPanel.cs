using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class accusationPanel : MonoBehaviour {
	////----    Accusation Panel    ----//////////////////////////////////////////////////////
	//Buttons for accusation panel.
	private static List<List<GameObject>> accusationButtonLists;
	private static List<GameObject> suspectButtonList, weaponButtonList, roomButtonList;
	
	public GameObject accusationGrid;
	public GameObject submitButton;

	public UILabel hintLabel;
	
	//Accusation variables.
	private int[] selectArray;
	private int progress;
	//private int selectSuspect, selectWeapon, selectRoom;
	private NPC answerSuspect; 
	private CaseObject answerWeapon; 
	private string answerRoom;

	private int attempts;
	public UILabel attemptsLabel;

	private string[] hints = {
		"You're wrong, sweetie...",
		"You're on the right track...",
		"You're almost there...",
		"You solved it!"
	};

	// Use this for initialization
	void Start () {
		selectArray = new int[3];
		UIEventListener.Get (submitButton).onClick += this.submitAccusation;
		accusationButtonLists = new List<List<GameObject>>(); 
		suspectButtonList = new List<GameObject>();
		weaponButtonList = new List<GameObject>();
		roomButtonList = new List<GameObject>();
		initAccusationPanel();



		answerSuspect = GameManager.theCase.getGuilty();
		answerWeapon = GameManager.theCase.getWeapon();
		answerRoom = GameManager.theCase.getRoom();

		Debug.LogWarning ("Cheater: " + answerSuspect + " " + answerWeapon + " " + answerRoom);

		attempts = 2;
		attemptsLabel.text = "Remaining attempts: " + attempts;
	}

	// Update is called once per frame
	void Update () {
	
	}

	////----    Accusation Panel    ----////////////////////////////////////////////////////
	void initAccusationPanel(){
		//Initialize selectArray with no selections.
		for(int i = 0; i < 3; i++) {
			selectArray[i] = -1;
		}

		//Add 3 types of buttons to respective buttong lists
		foreach(Transform child in accusationGrid.transform){
			if(child.name == "Suspect Selection Portrait"){
				UIEventListener.Get(child.gameObject).onClick += this.accusationOnClick;
				suspectButtonList.Add (child.gameObject);
			}
		}
		for (int i = 0; i < GameManager.npcList.Count; i++){
			suspectButtonList[i].GetComponentInChildren<UILabel>().text = GameManager.npcList[i].getElementName();
			suspectButtonList[i].transform.FindChild("Suspect Portrait").GetComponent<UI2DSprite>().sprite2D = GameManager.npcList[i].getProfileImage();
		}

		foreach (Transform child in accusationGrid.transform) {
			if(child.name == "Object Selection Portrait"){
				UIEventListener.Get(child.gameObject).onClick += this.accusationOnClick;
				weaponButtonList.Add (child.gameObject);
			}
		}
		for(int i = 0; i < GameManager.theCase.activeWeapons.Count; i++){
			weaponButtonList[i].GetComponentInChildren<UILabel>().text = GameManager.theCase.activeWeapons[i].getElementName();
			weaponButtonList[i].transform.FindChild("Object Portrait").GetComponent<UI2DSprite>().sprite2D = GameManager.theCase.activeWeapons[i].GetComponent<SpriteRenderer>().sprite;	
		}
		
		foreach (Transform child in accusationGrid.transform) {
			if(child.name == "Room Selection Portrait"){
				UIEventListener.Get(child.gameObject).onClick += this.accusationOnClick;
				roomButtonList.Add (child.gameObject);
			}
		}
		for (int i = 0; i < GameManager.roomList.Count; i++) {
			roomButtonList[i].GetComponentInChildren<UILabel>().text = GameManager.roomList[i];
			roomButtonList[i].transform.FindChild("Room Portrait").GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("Journal/" + (GameManager.roomList[i]));
		}
		
		accusationButtonLists.Add(suspectButtonList);
		accusationButtonLists.Add(weaponButtonList);
		accusationButtonLists.Add(roomButtonList);
	}
	
	//
	void accusationOnClick(GameObject button){
		if(suspectButtonList.Contains(button)){
			setSelected(accusationButtonLists.IndexOf(suspectButtonList), suspectButtonList.IndexOf(button));
		}
		else if(weaponButtonList.Contains(button)){
			setSelected(accusationButtonLists.IndexOf(weaponButtonList), weaponButtonList.IndexOf(button));
		}
		else if(roomButtonList.Contains(button)){
			setSelected(accusationButtonLists.IndexOf(roomButtonList), roomButtonList.IndexOf(button));
		}
	}
	
	
	void setSelected(int row, int column){
		if(selectArray[row] < 0){
			accusationButtonLists[row][column].GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("Journal/portrait-greenbutton");
			selectArray[row] = column;
		}
		else if(column == selectArray[row]){
			accusationButtonLists[row][column].GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("Journal/portrait-redbutton");
			selectArray[row] = -1;
		}
		else {
			accusationButtonLists[row][selectArray[row]].GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("Journal/portrait-redbutton");
			selectArray[row] = column;
			accusationButtonLists[row][column].GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>("Journal/portrait-greenbutton");
		}
	}

	void submitAccusation(GameObject button){
		if(selectArray[0] != -1 && selectArray[1] != -1 && selectArray[2] != -1){
			progress = 0;
			if(GameManager.npcList[selectArray[0]] == answerSuspect){
				progress++;
			}
			if(GameManager.theCase.activeWeapons[selectArray[1]] == answerWeapon){
				progress++;
			}
			if(GameManager.roomList[selectArray[2]] == answerRoom){
				progress++;
			}

			if(progress >= 3){
				hintLabel.text = hints[progress];
				Invoke ("endGame", 5f);
			}
			else {
				attempts--;
				if(attempts <= 0){
					hintLabel.text = "You got it wrong one too many times.";
					attemptsLabel.text = "Remaining attempts: 0";
					Invoke ("endGame", 5f);
				}
				else{
					hintLabel.text = hints[progress];
					attemptsLabel.text = "Remaining attempts: " + attempts;
				}
			}
		}
		else { 
			hintLabel.text = "You need to select one suspect, one weapon, and one location in order to submit.";
		}

	}

	void endGame(){
		Application.LoadLevel ("mainmenu");
		Destroy (gameObject);
	}
}
