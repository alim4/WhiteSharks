// @author Anthony Lim
// Manager for the game input

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	// Declare properties
	private static InputManager instance;
	private KeyCode _prevKeyPressed;
	private PlayerState _currState;
	private bool _isPause = false;
	private float _buttonWidth = 150;

	// Movement components
	private KeyCode _moveLeft;
	private KeyCode _moveRight;
	private KeyCode _moveUp;
	private KeyCode _moveDown;
	
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

	void PauseMenu() {
		_isPause = !_isPause;
		if (_isPause) {
			GameManager.Instance.setState(gameStates.JOURNAL);
			Time.timeScale = 0;
		} else {
			GameManager.Instance.setState(gameStates.INGAME);
			Time.timeScale = 1;
		}
	}

	void OnGUI() {
		// This condition used for triggering pause menu
		if (_isPause) {
			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2, _buttonWidth, 30), "Unpause")) {
				_isPause = !_isPause;
				GameManager.Instance.setState(gameStates.INGAME);
				Time.timeScale = 1;
			}

			if (GUI.Button (new Rect (Screen.width/2 - _buttonWidth, Screen.height/2 + 40, _buttonWidth, 30), "Exit to Main Menu")) {
				_isPause = !_isPause;
				GameManager.Instance.setState(gameStates.MAINMENU);
				Application.LoadLevel ("mainmenu");
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Gets the last key pressed
		if (Input.anyKeyDown) {
			_prevKeyPressed = FetchKey ();
		}

		// Input to open pause menu
		if (Input.GetKeyDown(KeyCode.Escape) && Application.loadedLevelName != "mainmenu") {
			PauseMenu ();
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
