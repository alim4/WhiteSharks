using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject start;
	public GameObject options;
	public GameObject quit;
	private float _buttonWidth = 150;
	private bool _mainMenu = true;
	private string _wo;

	// Use this for initialization
	void Start () {
		UIEventListener.Get (start).onClick += this.onClick;
		UIEventListener.Get (options).onClick += this.onClick;
		UIEventListener.Get (quit).onClick += this.onClick;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void onClick(GameObject button){
		if (button == start) {
			Debug.Log ("START");	
			startGame();
		}
		if (button == options) {
			Debug.Log ("OPTIONS");		
			optionsMenu();
		}
		if (button == quit) {
			Debug.Log ("QUIT");		
			quitGame();
		}
	}

	private void startGame() {
		Debug.Log("Starting gameadsjbadsjbfdajbkadfj");
		Debug.Log (GameManager.episodeDialogues [GameManager.currentEpisode]); 
		Dialoguer.Initialize (GameManager.episodeDialogues[GameManager.currentEpisode]);
		// Initialize various managers for the game
		// Singleton pattern

		DontDestroyOnLoad(GameManager.Instance);
		DontDestroyOnLoad(InputManager.Instance);
		GameManager.Instance.startState();

		switch (GameManager.currentEpisode) {
		case 0:
			GameManager.npcConversations[(int)NPCNames.Shammy] = 1;
			GameManager.npcConversations[(int)NPCNames.CarlosFranco] = 4;
			GameManager.npcConversations[(int)NPCNames.NoelAlt] = 3;
			break;
		}

	}
	
	private void optionsMenu() {
		print ("Entering Options menu");
		
		//_mainMenu = false;
		DontDestroyOnLoad (InputManager.Instance);
	}
	
	private void quitGame() {
		print ("Quitting game");
		
		DontDestroyOnLoad(GameManager.Instance);
		GameManager.Instance.quitGame();  
	}
}
