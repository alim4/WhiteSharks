using UnityEngine;
using System.Collections;

public class DialogueGUI : MonoBehaviour {

	private bool _showing;

	private string _text;
	private string[] _choices;
	
	// Use this for initialization
	void Start () {
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (!_showing)
			return;

		GUI.Box (new Rect (30, 10, 300, 180), _text);

		if (_choices == null)
		{
			if (GUI.Button (new Rect(30, 220, 200, 30), "Continue"))
			{
				Dialoguer.ContinueDialogue();
			}
		}
		else
		{
			for (int i = 0; i < _choices.Length; ++i)
			{
				if (GUI.Button (new Rect(30, 220 + (40*i), 200, 30), _choices[i]))
				{
					Dialoguer.ContinueDialogue(i);
				}
			}
		}
	}

	private void onStarted()
	{
		_showing = true;
	}

	private void onEnded()
	{
		_showing = false;
	}

	private void onTextPhase(DialoguerTextData data)
	{
		_text = data.text;
		_choices = data.choices;
	}
}
