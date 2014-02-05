using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/*
	 * createConversation (ArrayList alibi)
	 * This function works in the following way:
	 * Since the main premise of what makes a NPC guilty, suspect or witness is the number of arguments his alibi has,
	 * it sets up a three way switch/case so that when it's guilty (it contains one element) it only uses that element and
	 * gets a random room from the game manager (so it could be a lie or not).
	 * If it's suspect or witness, it uses all the elements in his/her alibi (and returns them as string).
	 */
	public static string createConversation (List<string> alibi){
		switch (alibi.Count) {
		case 1:
			return alibi[0] + ", I was in " + alibi[1] + ".";
			break;
		case 2:
			return alibi[0] + ", I was in " + alibi[1] + ".";
			break;
		case 3:
			return alibi[0] + ", I was in " + alibi[1] + " with " + alibi[2] + ".";
			break;
		}
		return "";
	}


	string getInformation (CaseObject c){
		if (c.guilt == GuiltLevel.guilty) {
			return (string) c.infoGuilty[Random.Range(0, c.infoGuilty.Count)];		
		} else {
			return (string) c.infoNotGuilty[Random.Range(0, c.infoNotGuilty.Count)];				
		}
	}
}
