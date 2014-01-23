using UnityEngine;
using System.Collections;

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
	string createConversation (ArrayList alibi){
		switch (alibi.Count) {
		case 1:
			int roomListSize = GameManager.Instance.roomList.Count;
			return alibi[0] + ", I was in " + (string)GameManager.Instance.roomList[Random.Range(0,roomListSize)] + ".";
			break;
		case 2:
			//getRoomName()
			return alibi[0] + ", I was in " + alibi[1] + ".";
			break;
		case 3:
			//getRoomName()
			NPC n = (NPC)alibi[2];
			return alibi[0] + ", I was in " + alibi[1] + " with " + alibi[2] + ".";
			break;
		}
		return "";
	}


	string createInformation (GuiltLevel guilt){
		if (guilt == GuiltLevel.guilty) {
			return "This weapon was used.";		
		} else {
			return "This weapon wasn't used.";				
		}
	}
}
