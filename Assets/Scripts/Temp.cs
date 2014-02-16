using UnityEngine;
using System.Collections;

public class Temp : MonoBehaviour {

	public ConvoTheme convoTheme;
	
	private readonly string GLOBAL_VARIABLE_SAVE_KEY = "serialized_global_variable_state";
	
	//private string returnedString = string.Empty;
	
	void Start () {
		
		// Initialize the Dialoguer
		// Dialoguer.Initialize();
		
		// If the Global Variables state already exists, LOAD it into Dialoguer
		if(PlayerPrefs.HasKey(GLOBAL_VARIABLE_SAVE_KEY)){
			Dialoguer.SetGlobalVariablesState(PlayerPrefs.GetString(GLOBAL_VARIABLE_SAVE_KEY));
			//returnedString = PlayerPrefs.GetString(GLOBAL_VARIABLE_SAVE_KEY);
		}
		//		This can be saved anywhere, and loaded from anywhere the user wishes
		//		To save the Global Variable State, get it with Dialoguer.GetGlobalVariableState() and save it where you wish
	}
	
	void Update () {
		//returnedString = Dialoguer.GetGlobalVariablesState();
	}
	
	void OnGUI(){
		GUI.depth = 10;

		if(GUI.Button (new Rect(25, 25 + 30 + 10, 125, 30), "Start Dialogue")){
			Dialoguer.events.ClearAll();
			convoTheme.addDialoguerEvents();
			Dialoguer.StartDialogue(1);	// Old SchoolRPG
		}
	}
}

