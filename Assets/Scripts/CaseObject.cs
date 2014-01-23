using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public Category category;		//What kind of item is it? (such as Personal Items, blades, guns, etc)
	public ArrayList infoGuilty;	//Store a list of strings that might be displayed if the item is related to the murder
	public ArrayList infoNotGuilty; //If the item is unrelated, you might display one or more of these strings

/*	//enable conversation object if left mouse button is clicked.
	public override void onMouseDown(){
		//Does nothing at the moment
	}*/

	public void addInfoGuilty(string newInfo){
		infoGuilty.Add(newInfo);
	}

	public void addInfoNotGuilty(string newInfo){
		infoNotGuilty.Add(newInfo);
	}
}
