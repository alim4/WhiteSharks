using UnityEngine;
using System.Collections;

public class CaseObject : CaseElement {

	public Category category;		//What kind of item is it? (such as Personal Items, blades, guns, etc)
	public ArrayList infoGuilty = new ArrayList();	//Store a list of strings that might be displayed if the item is related to the murder
	public ArrayList infoNotGuilty = new ArrayList(); //If the item is unrelated, you might display one or more of these strings
	public string conversation;

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

	//Right now it just inputs preset sentences, replace with data driven stuff
	void Start(){
		this.addInfoGuilty ("There are fresh fingerprints in this weapon.");
		this.addInfoGuilty ("I really can tell that someone used this...I think.");
		this.addInfoGuilty ("I'm pretty confident that this is a trace.");
		this.addInfoNotGuilty ("God, this has been here for ages.");
		this.addInfoNotGuilty ("It's incredible how easily people tend to forget their stuff for ages.");
		this.addInfoNotGuilty ("I'm sure this has been cold for a long while.");
	}
}
