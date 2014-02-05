// @author Anthony Lim
// Manages core gameplay elements such as scenes and states

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	// Declare properties
	private static GameManager instance;
	private gameStates _currentState;
	private string _currLevel;			// Current level
	private string _name;				// Character name
	public static List<string> roomList = new List<string>();
	public static List<NPC> npcList = new List<NPC>();
	public static List<CaseObject> weaponList= new List<CaseObject>();
	public ArrayList roomIDList;
	public int currentRoomIndex;
	private string currentMainCharacter;
	public static Case theCase = new Case(); //Generate this!
	public float nextX, nextY;
	public static NPC guilty;
	public static CaseObject weapon;
	public static string room;

	// + case: ArrayList<CaseElement>
	// + npcs: ArrayList<CaseElement>
	// + objects: ArrayList<CaseElement>

	public static GameManager Instance {
		get {
			if (instance == null) {
				Debug.Log("Instance null, creating new GameManager");
				instance = new GameObject("GameManager").AddComponent<GameManager>();
			}
			return instance;
		}
	}

	// Sets instance to null when the application quits
	public void OnApplicationQuit() {
		instance = null;
	}
	//set the next x value
	public void SetNextX(float x){
		nextX = x;
	}
	//return the nextX value
	public float GetNextX(){
		return nextX;
	}
	//set the nextY value
	public void SetNextY(float y){
		nextY = y;
	}
	//return the nextY value
	public float GetNextY(){
		return nextY;
	}
	//set the main character that the player is currently playing
	public void SetMainCharacter(string main){
		currentMainCharacter = main;
	}
	//return the main character.
	public string GetMainCharacter(){
		return currentMainCharacter;
	}

	/// <summary>
	/// Starts the game state and sets initial values
	/// Should be called during gameStart
	/// </summary>
	public void startState() {
		Debug.Log("Creating a new game state");
		//CaseGenerator c = new CaseGenerator("","");
		//GameManager.setNPCS = 

		// Set default properties
		_currLevel = "Level 1";
		_name = "My Character";
		_currentState = gameStates.INGAME;

		// Load character select screen
		Application.LoadLevel ("CharacterSele");
	}

	/// <summary>
	/// Generates the case
	/// </summary>
	public void generateCase() {
		// case generation

		CaseGenerator c = new CaseGenerator ();
		c.generateCase();
		Debug.Log ("the case in GM " + guilty + " " + weapon + " " + room);

	}

	/// <summary>
	/// Calculates the score
	/// </summary>
	/// <returns>The score.</returns>
	public int calculateScore() {
		// calcs score
		return 0;
	}

	/// <summary>
	/// Draws the score
	/// </summary>
	public void drawScore() {
		// draws the score on the screen
	}

//	public void updateJNPC(NPC n) {
//
//	}
//
//	public void updateJObject(CaseObject o) {
//
//	}


	/// <summary>
	/// Quits the game
	/// </summary>
	public void quitGame() {
		Debug.Log("Qutting the game");
		Application.Quit ();
	}

	/// <summary>
	/// Get the current level
	/// </summary>
	/// <returns>The level.</returns>
	public string getLevel() {
		if (_currLevel != null)
			return _currLevel;
		else
			return "currLevel is null!";
	}

	/// <summary>
	/// Set the currLevel
	/// </summary>
	/// <param name="level">Level.</param>
	public void setLevel(string level) {
		_currLevel = level;
	}

	/// <summary>
	/// Gets the name
	/// </summary>
	/// <returns>The name.</returns>
	public string getName() {
		if (_name != null)
			return _name;
		else
			return "name is null!";
	}

	/// <summary>
	/// Sets the name
	/// </summary>
	/// <param name="newName">New name.</param>
	public void setName(string newName) {
		_name = newName;
	}

	public void setState(gameStates state) {
		_currentState = state;
	}

	public void OnGUI() {
		GUI.TextArea(new Rect(1, 1, 100, 20), _currentState.ToString());
	}

	void Update() {
		//print ("Test??");
	}

	//Testing purposes
	void Start(){
		roomIDList = new ArrayList ();
		roomIDList.Add("stage1");
		roomIDList.Add("stage2");
		roomList.Add ("Finn");
		roomList.Add ("Belly");
		npcList.Add(Resources.Load<NPC>("NoelAlt"));
		npcList.Add(Resources.Load<NPC>("NPC1"));
		npcList.Add(Resources.Load<NPC>("RandomNPC"));
		weaponList.Add(Resources.Load<CaseObject>("Weapon1"));
		weaponList.Add(Resources.Load<CaseObject>("Weapon2"));
		Debug.LogError ("Generating case");
		generateCase ();
		//Debug.Log ("GM NPClist count: " + npcList.Count);
		//Debug.Log ("Room ID list count:" + roomIDList.Count);

	}

	public static List<NPC> getSceneNPCList(int sceneID){ 
		List<NPC> temp = new List<NPC>();
		foreach (NPC n in npcList) {
			if (n.location == sceneID){
				//Debug.Log("Match found");
				temp.Add(n);
			}
		}
		return temp;
	}
}

public enum gameStates {
	INTRO,
	MAINMENU,
	INGAME,
	JOURNAL,
	CONVERSATION
}