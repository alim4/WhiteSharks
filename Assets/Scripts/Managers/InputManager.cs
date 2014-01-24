// @author Anthony Lim
// Manager for the game input, handles pause

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	// Declare properties
	private static InputManager instance;
	private KeyCode _prevKeyPressed;
	private PlayerState _currState;
	private float _buttonWidth = 150;

	// Movement components
	private KeyCode _moveLeft;
	private KeyCode _moveRight;
	private KeyCode _moveUp;
	private KeyCode _moveDown;

	/// <summary>
	/// The name of the game paused event.
	/// </summary>
	private const string ON_GAME_PAUSED_EVENT = "OnGamePaused";
	/// <summary>
	/// The name of the game resumed event.
	/// </summary>
	private const string ON_GAME_RESUMED_EVENT = "OnGameResumed";
	/// <summary>
	/// The previous Time.timeScale. This value is used to resume the game at the same pace it was paused at.
	/// </summary>
	private float _previousTimeScale;
	/// <summary>
	/// Indicates if the game is currently paused.
	/// </summary>
	private bool _isGamePaused;
	
	public static InputManager Instance {
		get {
			if (instance == null) {
				Debug.Log("Instance null, creating new InputManager");
				instance = new GameObject("InputManager").AddComponent<InputManager>();
			}
			return instance;
		}
	}

	/// <summary>
	/// Sets the movement components.
	/// </summary>
	/// <param name="L">Move Left component</param>
	/// <param name="R">Move Right component</param>
	/// <param name="U">Move Up component</param>
	/// <param name="D">Move Down component</param>
	public void SetMovementComponents(KeyCode L, KeyCode R, KeyCode U, KeyCode D) {
		_moveLeft = L;
		_moveRight = R;
		_moveUp = U;
		_moveDown = D;
	}

	KeyCode FetchKey() {
		// Gets the current singular key pressed
		int e = System.Enum.GetNames (typeof(KeyCode)).Length;
		for (int i = 0; i < e; ++i) {
			if (Input.GetKey ((KeyCode)i)) {
				return (KeyCode)i;
			}
		}
		return KeyCode.None;
	}

	/// <summary>
	/// Gets a value that indicates if the game is currently paused.
	/// </summary>
	public bool IsGamePaused {
		get { return _isGamePaused; }
	}

	/// <summary>
	/// Pauses the game. The game paused event is fired for all game objects in the world object.
	/// </summary>
	public void Pause() {
		Debug.Log("Game pausing.");
		
		// Pause the game and indicate that the game is actually paused.
		_previousTimeScale = Time.timeScale;
		Time.timeScale = 0.0f;
		_isGamePaused = true;
		
		Debug.Log(string.Format("Game paused on Time.TimeScale = {0}.", Time.timeScale));
	}
	
	/// <summary>
	/// Resumes the game. The game resumed event is fired for all game objects in the world object.
	/// </summary>
	public void Resume() {
		Debug.Log( "Game resuming." );
		
		// Indicate that the game is unpaused. Should anyone check this value, it would be in the game resume event, which is called just before the game is
		// actually resumed.
		_isGamePaused = false;
	
		// Unpause the game.
		Time.timeScale = _previousTimeScale;
		
		Debug.Log(string.Format("Game resumed on Time.TimeScale = {0}.", Time.timeScale));
	}

	void OnGUI() {
		// This condition used for triggering pause menu
		if (_isGamePaused) {
			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2, _buttonWidth, 30), "Unpause")) {
				_isGamePaused = !_isGamePaused;
				GameManager.Instance.setState(gameStates.INGAME);
				Resume();
			}

			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2 + 40, _buttonWidth, 30), "Exit to Main Menu")) {
				_isGamePaused = !_isGamePaused;
				GameManager.Instance.setState(gameStates.MAINMENU);
				Application.LoadLevel ("mainmenu");
				Resume();
			}
		}
	}

	// Use this for initialization
	void Start () {
		_previousTimeScale = Time.timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		// Gets the last key pressed
		if (Input.anyKeyDown) {
			_prevKeyPressed = FetchKey ();
		}

		// Input to open pause menu
		if (Input.GetKeyDown(KeyCode.Escape) && Application.loadedLevelName != "mainmenu" && !_isGamePaused) {
			Pause();
		} else if (Input.GetKeyDown(KeyCode.Escape) && Application.loadedLevelName != "mainmenu" && _isGamePaused) {
			Resume();
		}
	}
}

public enum PlayerState {
	MOVELEFT,
	MOVERIGHT,
	MOVEDOWN,
	MOVEUP,
	OPENJOURNAL,
	OPENMENU
}
