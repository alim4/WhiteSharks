// @author Anthony Lim
// Manages core gameplay elements such as scenes and states

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {
	// Declare properties
	private static GameManager instance;
	private gameStates _currentState;
	private string _currLevel;			// Current level
	private string _name;				// Character name
	public static List<string> roomList = new List<string>();
	public static List<NPC> npcList = new List<NPC>();
	public static List<NPC> witnessList = new List<NPC>();
	public static List<CaseObject> weaponList= new List<CaseObject>();
	public ArrayList roomIDList;
	public string[] rooms;
	public int currentRoomIndex;
	private string currentMainCharacter;
	public CaseGenerator generator;
	public static Case theCase = new Case(); //Generate this!
	public float nextX, nextY;
	public static NPC guilty;
	public static CaseObject weapon;
	public static string room;

	//This is the target state the player wishes to reach for maximum score
	public static Dictionary idealGameState;

	//Handles mouse cursor information
	public static int cursorSize = 32;
	public static List<Texture2D> mouseSprites;
	public static string[] spriteIndex;
	public static Texture2D currMouse;


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
	// get player script of main character
 	public playerScript getPlayerScript(){
 		return (playerScript)GameObject.Find (this.GetMainCharacter() + "(Clone)").GetComponent<playerScript> ();
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
		idealGameState = new Dictionary ();

		// Load character select screen
		Application.LoadLevel ("CharacterSele");
	}

	/// <summary>
	/// Generates the case
	/// </summary>
	public void generateCase() {
		//Debug.Log (theCase.getRoom());
		theCase = generator.generateCase();
		//Debug.Log ("the case in GM " + guilty + " " + weapon + " " + room);
		//Debug.Log (theCase.getRoom());
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
		//GUI.TextArea(new Rect(1, 1, 100, 20), _currentState.ToString());

		//Handle mouse updates here

		GUI.DrawTexture (new Rect (Input.mousePosition.x - cursorSize / 16, (Screen.height - Input.mousePosition.y) - cursorSize / 16,
		                           cursorSize, cursorSize), currMouse);


	}

	//Access function for updating the GameManager's dictionary
	public void addEntry(DictEntry newEntry){
		idealGameState.addNewEntry (newEntry);
	}
	
	//Access function for adding an entry to the GameManager's dictionary
	public void updateDict(DictEntry newEntry){
		idealGameState.updateDictionary (newEntry);
	}
	
	//Access function for printing the GameManager's dictionary
	public void printGoal(){
		idealGameState.printEntries ();
	}

	//Access function for copying the dictionary
	public Dictionary getDict(){
		return idealGameState;
	}


	//Initialize the sprite array for the mouse to draw
	//Loads in from Sprites/Mouse Icons
	//Sets the initial icon to Walk
	void setIcons(){

		mouseSprites = new List<Texture2D>();
		Screen.showCursor = false;

		foreach (object o in Resources.LoadAll("MouseIcons", typeof(Texture2D))) {
			mouseSprites.Add(o as Texture2D);
		}

		spriteIndex = new string[mouseSprites.Count];
		
		for(int i=0; i< spriteIndex.Length; i++) {
			spriteIndex[i] = mouseSprites[i].name;
		}

		currMouse = (Texture2D) mouseSprites[Array.IndexOf(spriteIndex, "Walk_Icon")];
	}

	//Updates the current sprite when called by another game object
	//Takes in a string based on what kind of object it is that signifies the icon the cursor should be
	public void updateMouseIcon(string whichSprite){
		currMouse = (Texture2D)mouseSprites [Array.IndexOf (spriteIndex, whichSprite)];

		print (currMouse.ToString () + " WHEEEE");
	}

	private void addWitnesses(){

		foreach (NPC n in witnessList) {

			idealGameState.addNewEntry(new DictEntry(n.getEnumName(), GuiltLevel.witness,
			                                         n.getWeaponProf(), n.getAlibi(), n.getTrust()));

		}

	}

	//Testing purposes
	void Start(){
		//start location
		nextX = -6.440672f;
		nextY = -5.890769f;
		//For a changing cursor, load in all of its sprites into the list
		setIcons ();

		// Used for Dialoguer components
		Debug.Log ("Persocets, Adderall, Ecstasy, PMW");
		DialogueGUI dGUI = gameObject.AddComponent<DialogueGUI> ();
		dGUI.setSkin(Resources.Load ("OldSchool") as GUISkin);
		dGUI.setTexture(Resources.Load ("DialogueBoxDiagonalLines") as Texture2D);


		roomIDList = new ArrayList ();
		rooms = new string[5];
		rooms[2] = "bar";
		rooms[3] = "bellyRoom";
		roomIDList.Add("stage1");
		roomIDList.Add("stage2");
		roomIDList.Add("stage3");
		roomIDList.Add("bar");
		roomIDList.Add("stage4");
		roomList.Add ("Office");
		roomList.Add ("Cafe");
		roomList.Add ("Gym");
		npcList.Add(Resources.Load<NPC>("LiamOShea"));
		npcList.Add(Resources.Load<NPC>("NinaWalker"));
		npcList.Add(Resources.Load<NPC>("JoshSusach"));
		witnessList.Add(Resources.Load<NPC>("NoelAlt"));
		witnessList.Add(Resources.Load<NPC>("PeijunShi"));
		witnessList.Add(Resources.Load<NPC>("CarlosFranco"));
		weaponList.Add(Resources.Load<CaseObject>("LaserPistol"));
		weaponList.Add(Resources.Load<CaseObject>("eSword"));
		weaponList.Add(Resources.Load<CaseObject>("MetalPipe"));
		weaponList.Add(Resources.Load<CaseObject>("RadioactiveIceCubes"));
		weaponList.Add(Resources.Load<CaseObject>("VSs"));
		generator = new CaseGenerator ();
		//generateCase ();
		theCase = generator.demo ();
		addWitnesses ();

		printGoal ();
	}

	public static List<NPC> getSceneNPCList(int sceneID){ 
		List<NPC> temp = new List<NPC>();
		foreach (NPC n in npcList) {
			Debug.Log (n.location + " " + n.name);
			Debug.Log (sceneID);
			if (n.location == sceneID){
				//Debug.Log("Match found");
				temp.Add(n);
			}
				}
		return temp;
	}

	public static List<NPC> getSceneWitnessList(int sceneID){ 
		List<NPC> temp = new List<NPC>();
		foreach (NPC n in witnessList) {
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