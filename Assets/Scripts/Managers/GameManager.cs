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
	public ArrayList roomList;
	public static List<NPC> npcList = new List<NPC>();
	public ArrayList weaponList;
	public ArrayList roomIDList;
	public int currentRoomIndex;
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

		// Load Level 1
		Application.LoadLevel ("stage1");
	}

	/// <summary>
	/// Generates the case
	/// </summary>
	public void generateCase() {
		// case generation
		CaseGenerator c = new CaseGenerator ();

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
		npcList.Add(Resources.Load<NPC>("NoelAlt"));
		npcList.Add(Resources.Load<NPC>("NPC1"));
		//Debug.Log ("GM NPClist count: " + npcList.Count);
		roomIDList = new ArrayList ();
		roomIDList.Add("stage1");
		roomIDList.Add("stage2");
		Debug.Log ("Room ID list:" + roomIDList);

	}

	public static List<NPC> getSceneNPCList(int sceneID){ 
		List<NPC> temp = new List<NPC>();
		foreach (NPC n in npcList) {
			if (n.location == sceneID){
				Debug.Log("Match found");
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