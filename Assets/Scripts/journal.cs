//Adrian Williams
//Journal
//Things to work on:
//	Could use GameObject.Find() to reduce number of public variables.
//  Suspect/weapon buttons should be created at run-time for variable amounts of each. Map buttons too. Requires scroll view.
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class journal : MonoBehaviour {

	private static journal instance;
	private static journal j;

	public static journal Instance {
		get {
			if (instance == null) {
				Debug.Log("JOURNAL: Instance null, creating new Journal");
				instance = new GameObject("journal").AddComponent<journal>();
			}
			return instance;
		}
	}
	
	public List<NPC> personsOfInterest;
	private List<CaseObject>weaponList;
	private List<string>roomList;

	//Defaults for non-visible NPC
	public static Sprite emptyPortrait;
	private string emptyName;

	//Grab view tab buttons. Will change to use gameobject find.
	public GameObject viewTab1;
	public GameObject viewTab2;
	public GameObject viewTab3;

	public GameObject poiObjectView;
	public GameObject mapView;

	//Grab two main journal activation buttons and boolean to check when in menu.
	public GameObject journalButton;
	public GameObject accusationRoomButton;
	private bool inMenu;
	public GameObject alleywayBelly, alleywayFin, janesRoom, plaza, yesButton, noButton;
	public UILabel whereLabel;
	private GameObject selectedLocation;

	//Grab buttons and textfield from view. Will change to use gameobject find. Three lists for three different types of buttons.
	private static List<GameObject> viewTabList;
	private static List<GameObject> poiButtonList;
	private static List<GameObject> objectButtonList;

	public GameObject poiButtonGrid;
	public GameObject objectButtonGrid;
	public UI2DSprite poiPortrait;

	public UILabel descriptionLabel, panelNameLabel, timeLabel;

	//Destroys duplicate UI Roots.
	void Awake () {
		//journalButton.transform.position = new Vector3(275, 20, 0);
		if(!j){
			j = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy (gameObject);
		}
	}
	
	void Start () {
		////----    Journal Panel Init    ----//////////////////////////////////////////////////////

		//Default name for "invisible" person of interest.
		emptyName = "?????";

		//Persons of interest list.
		personsOfInterest = GameManager.npcList;

		//Weapon list once that's ready.
		weaponList = GameManager.theCase.activeWeapons;

		roomList = GameManager.roomList;

		//Listens for tab button presses in journal and runs onClick with button clicked as parameter.
		UIEventListener.Get (viewTab1).onClick += this.onClick;
		UIEventListener.Get (viewTab2).onClick += this.onClick;
		UIEventListener.Get (viewTab3).onClick += this.onClick;

		//Listens for journal/accusation room buttons to make sure only one is active at a time.
		UIEventListener.Get (journalButton).onClick += this.journalAccusationPanelToggle;
		UIEventListener.Get (accusationRoomButton).onClick += this.journalAccusationPanelToggle;

		//Want to get rid of this too.
		viewTabList = new List<GameObject>();
		viewTabList.Add(viewTab1);
		viewTabList.Add(viewTab2);
		viewTabList.Add(viewTab3);

		//List of person of interest portrait buttons.
		poiButtonList = new List<GameObject>();
		objectButtonList = new List<GameObject>();

		UIEventListener.Get (alleywayFin).onClick += this.onClickMap;
		UIEventListener.Get (alleywayBelly).onClick += this.onClickMap;
		UIEventListener.Get (janesRoom).onClick += this.onClickMap;
		UIEventListener.Get (plaza).onClick += this.onClickMap;
		UIEventListener.Get (yesButton).onClick += this.onClickMap;
		UIEventListener.Get (noButton).onClick += this.onClickMap;
		selectedLocation = null;

		initPoIView();
		initObjView ();
		changeView(0);
		changePOI (0);
		StartCoroutine (UpdateTime ());
	}

	//Single onclick function for any button in the journal.
	void onClick(GameObject button){
		if(viewTabList != null && viewTabList.Contains(button)){
			changeView (viewTabList.IndexOf(button));
			//Debug.Log ("won't happen yet~!~!~!~!~");
		}
		else if(poiButtonList.Contains(button)){
			changePOI(poiButtonList.IndexOf(button));
			//Debug.Log ("poiButton!");
		}
		else if (objectButtonList.Contains (button)){
			changeObject(objectButtonList.IndexOf(button));
			//Debug.Log ("objectbutton!");
		}
	}
	//Bad way to do this.
	void onClickMap(GameObject button){
		if(button == alleywayFin){
			selectedLocation = alleywayFin;
			whereLabel.text = alleywayFin.name + "?";
		}
		else if(button == alleywayBelly){
			selectedLocation = alleywayBelly;
			whereLabel.text = alleywayBelly.name + "?";
		}
		else if(button == janesRoom){
			selectedLocation = janesRoom;
			whereLabel.text = janesRoom.name + "?";
		}
		else if(button == plaza){
			selectedLocation = plaza;
			whereLabel.text = plaza.name + "?";
		}
		else if(button == yesButton){
			if(selectedLocation != null){
				loadNewLocation();
			}
			else{
				whereLabel.text = "Select a location first.";
			}
		}
		else if(button == noButton){
			whereLabel.text = "";
			selectedLocation = null;
		}
	}
	//Bad way to do this.
	void loadNewLocation(){
		if(selectedLocation == alleywayFin){
			Application.LoadLevel("stage2");
		}
		else if(selectedLocation == alleywayBelly){
			Application.LoadLevel("stage4");
		}
		else if(selectedLocation == janesRoom){
			Application.LoadLevel("stage1");
		}
		else if(selectedLocation == plaza){
			Application.LoadLevel("stage3");
		}
		whereLabel.text = "";
		selectedLocation = null;
	}

	//Toggles either the journal or the accusation room panel.
	//Only one active at a time.
	void journalAccusationPanelToggle(GameObject button){
		if (button == journalButton){
			if (inMenu){
				Time.timeScale = 1f;
				accusationRoomButton.SetActive(true);
				inMenu = false;
			}
			else {
				Time.timeScale = 0f;
				accusationRoomButton.SetActive(false);
				inMenu = true;
			}
		}
		else if (button == accusationRoomButton){
			if (inMenu){
				Time.timeScale = 1f;
				journalButton.SetActive(true);
				inMenu = false;
			}
			else {
				Time.timeScale = 0f;
				journalButton.SetActive(false);
				inMenu = true;
			}
		}
	}

	//----- Button type functions
	//Changes view when view tab is clicked.
	//Will make helper function for grid/view SetActive. Use enum?
	void changeView(int viewNumber){
		clearLabels ();
		switch (viewNumber) {
			case 0://PoI
				//mapView.SetActive(false);
				//poiView.SetActive(true);
				poiObjectView.SetActive(true);
				mapView.SetActive(false);
				objectButtonGrid.SetActive(false);
				poiButtonGrid.SetActive(true);
				changePOI(0);
				break;
			case 1://Object
				//mapView.SetActive (false);
				//poiView.SetActive(true);
				poiObjectView.SetActive(true);
				mapView.SetActive(false);
				objectButtonGrid.SetActive(true);
				poiButtonGrid.SetActive(false);
				changeObject(0);
				break;
			case 2://Map
				//mapView.SetActive (true);
				//poiView.SetActive(false);
				poiObjectView.SetActive(false);
				mapView.SetActive(true);
				objectButtonGrid.SetActive(false);
				poiButtonGrid.SetActive(false);
				break;
		}
	}

	//Changes PoI when a PoI portrait is clicked.
	void changePOI(int poiNumber){
		//Sprint 2 change POI code.
		if (personsOfInterest [poiNumber].isVisible ()) {
			descriptionLabel.text = personsOfInterest[poiNumber].getDescription();
		}
		else {
			descriptionLabel.text = emptyName;
		}
		poiPortrait.sprite2D = personsOfInterest[poiNumber].getProfileImage();
		panelNameLabel.text = personsOfInterest [poiNumber].getElementName ();
	}

	//Changes object/weapon being viewed when portrait is clicked.
	void changeObject(int objectNumber){
		if (weaponList[objectNumber].isVisible ()) {
			descriptionLabel.text = weaponList[objectNumber].getDescription();
		}
		else {
			descriptionLabel.text = emptyName;
		}
		//poiPortrait.sprite2D = weaponList[objectNumber].getProfileImage();
		poiPortrait.sprite2D = weaponList[objectNumber].GetComponent<SpriteRenderer>().sprite;
		panelNameLabel.text = weaponList[objectNumber].getElementName ();
	}

	//Initialize PoI view. 
	//Code for sprint 2 journal.
	public void initPoIView(){
		Debug.LogWarning ("INIT JOURNAL");
		//Add buttons to poi button list and put them in UI event listener.
		foreach (Transform child in poiButtonGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.onClick;
			poiButtonList.Add(child.gameObject);
		}
		//Put suspect names on poi button labels.
		for (int i = 0; i < personsOfInterest.Count; i++) {
			if(personsOfInterest[i] != null){
				poiButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = personsOfInterest[i].getElementName();
			}
			else {
				poiButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = emptyName;
			}
		}
	}

	//Initialize obj view.
	public void initObjView(){
		//Add buttons to obj button list and put them in UI event listener.
		foreach (Transform child in objectButtonGrid.transform){
			UIEventListener.Get(child.gameObject).onClick += this.onClick;
			objectButtonList.Add(child.gameObject);
		}
		//Put suspect names on poi button labels.
		for (int i = 0; i < weaponList.Count; i++) {
			if(weaponList[i] != null){
				objectButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = weaponList[i].getElementName();
			}
			else {
				objectButtonList[i].gameObject.GetComponentInChildren<UILabel>().text = emptyName;
			}
		}
	}

	//Clear description labels. Might rename and add obj/poi grid on/off.
	void clearLabels(){
		panelNameLabel.text = "";
		descriptionLabel.text = "";
	}

	//Keeps time.
	IEnumerator UpdateTime(){
		while (true) {
			DateTime currentTime = System.DateTime.Now;
			timeLabel.text = currentTime.ToString("HH:mm");
			timeLabel.text += "//";
			yield return new WaitForSeconds(0.2f);
		}
	}

	public void updateKnowledge(DictEntry postState){
		//A stub because i don't know what this is.
	}

	public Dictionary getKnowledge(){
		//A stub because I dont' know what this is.
		Dictionary pk = new Dictionary();
		return pk;
	}

	public void updateNPCs(){
		//Persons of interest list.
		personsOfInterest = GameManager.npcList;
	}
}
