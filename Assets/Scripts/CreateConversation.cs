using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateConversation : MonoBehaviour {

	private List<string> convoList_player;
	private List<string> convoList_npc;
	private UILabel label;
	private UISprite spr;
	private bool visible = true;

	// Use this for initialization
	void Start () {
		label = GetComponent<UILabel> ();
		spr = GetComponent<UISprite> ();
		label.text = "Wow";

		convoList_npc.Add ("First");
		convoList_npc.Add ("Second");
		convoList_npc.Add ("Third");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.V))
		{
			visible = !visible;
		}

		// Gets the UISprite parent and toggles visibility
		if (visible)
		{
			transform.parent.gameObject.GetComponent<UISprite>().enabled = true;
		}
		else
		{
			transform.parent.gameObject.GetComponent<UISprite>().enabled = false;
		}


		foreach (var i in Conversations())
		{
			label.text = i.ToString();
		}


	}

	void CreateConvo()
	{
		//label.text = convoList_npc.n
	}

	private IEnumerable Conversations()
	{
		yield return 3;
		yield return 5;
		yield return "Yolo";
	}
}
