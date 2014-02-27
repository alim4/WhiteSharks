using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DialoguerCore;
using System;


public class Conversation {


	public Dictionary convTopics;	//This should contain some number of entries, one for the NPC you are talking to, the others for the
	//a different NPC you could talk about
	
	private string archeType;		//Keeps track of which player archeType was chosen for inserting into strings		
	
	private List<NPCNames> discTree; //Indices to check the Dictionary with
	private NPCNames whoAmI;
	
	//This method sets up the global variables in Dialoguer.  It translates from the DictEntry's in 
	//convTopics to get the required strings and booleans that are the only types Dialoguer accepts
	//Most of these values should be set by game manager, but if somehow need change, this method
	//will update them 
	public void generateDialoguer(){

		Dialoguer.StartDialogue ((int)whoAmI);


		DictEntry temp = convTopics.retrieveEntry (discTree[0]);
		bool guilty = false;
		if (temp.getGuilt () == GuiltLevel.guilty)
			guilty = true;
		
		Dialoguer.SetGlobalBoolean (0, guilty);
		Dialoguer.SetGlobalString (0, archeType);
		
		/*
		 * 1 Liam_Witness_Category
		 * 2 Liam_Alibi
		 * 3 Nina_Witness_Category
		 * 4 Nina_Alibi
		 * 5 Josh_Witness_Category
		 * 6 Josh_Alibi
		 * 7 Noel_Witness_Category 
		 * 8 Carlos_Witness_Category
		 * 9 Peijun_Witness_Category
		 */
		
		foreach (NPCNames i in discTree) {

			//Debug.LogError (i.ToString ());

			DictEntry curr = convTopics.retrieveEntry(i);

			int idNum = (int)curr.getIndex ();
			int idConversion = idNum;
			idConversion = (2 * idConversion) - 7; //Some formula goes here

			//Debug.LogWarning (idConversion);

			Dialoguer.SetGlobalFloat(idNum - 3, curr.getTrust ());
			
			if ((int)curr.getGuilt() <= (int) GuiltLevel.suspect){
				Dialoguer.SetGlobalString(idConversion, curr.getWeapon().ToString());
				Dialoguer.SetGlobalString(idConversion + 1, curr.getLocation());
			} else {
				Dialoguer.SetGlobalString(idNum, curr.getWeapon().ToString());
			}
		}
		
		
	}
	
	//This method takes global variables from Dialoguer
	public DictEntry getDialogResults(){
				return null;
		}
		
		//Update the player's knowledge base with the new info
		public void updatePlayerKnow(DictEntry newInfo){
			journal.Instance.updateKnowledge (newInfo);
		}
		
		public Conversation(string avatar, List<NPCNames> theIndex, NPCNames self){
			
			discTree = theIndex;
			archeType = avatar;
			convTopics = new Dictionary(GameManager.Instance.getDict ());
			whoAmI = self;
		}


	/*
	 * createConversation (ArrayList alibi)
	 * This function works in the following way:
	 * Since the main premise of what makes a NPC guilty, suspect or witness is the number of arguments his alibi has,
	 * it sets up a three way switch/case so that when it's guilty (it contains one element) it only uses that element and
	 * gets a random room from the game manager (so it could be a lie or not).
	 * If it's suspect or witness, it uses all the elements in his/her alibi (and returns them as string).
	 *
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
	}*/
}
