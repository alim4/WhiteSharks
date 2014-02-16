using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class playerAction : MonoBehaviour {

	public List<DictEntry> preCondition;	//In order for a playerAction to appear, its preCondition must be satisfied
	public DictEntry postState;				//Once the action is completed, apply a change to the player knowledge

	public void setPostState(DictEntry post){
		postState = post;
	}

	public void evaluate(){
		bool success = checkCondition ();

		if (success) {
				journal.Instance.updateKnowledge (postState);
		} else {
			print ("Condition not satisfied.");
		}
	}

	//Lookup each element in precondition and search for a similar index in player knowledge,
	//Compare them, and if all are satisfied, return true)
	private bool checkCondition(){

		Dictionary PK = journal.Instance.getKnowledge ();

		for (int i = 0; i < preCondition.Count; i++) {

			DictEntry curr = preCondition[0];
			string target = curr.getIndex ();

			if (curr != PK.retrieveEntry(target)){		//If the player knowledge doesn't match the condition, this method fails
				return false;
				
			} else {return false;}						//If the player knowledge lacks the information, it also fails
		
		}
		return true;							//Only succeeds when you've checked every precondition
	}

}




