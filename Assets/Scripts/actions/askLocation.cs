using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class askLocation : playerAction {

	public string who;

	// Use this for initialization
	public askLocation(string theNPC, Dictionary npcKnow){
		preCondition = new List<DictEntry> ();
		who = theNPC;
		DictEntry npcVersion = npcKnow.retrieveEntry (theNPC);
		postState = new DictEntry(theNPC, GuiltLevel.unrelated, Category.unrelated, npcVersion.getLocation (), npcVersion.getRelations(), -1.0f);

		preCondition.Add ( new DictEntry (theNPC, GuiltLevel.unrelated, Category.unrelated, "", null, -1.0f));
	}

	

}
