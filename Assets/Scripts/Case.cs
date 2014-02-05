using UnityEngine;
using System.Collections;

public class Case : Object {

	private NPC guiltyNpc;		//Who is guilty in this case?
	private CaseObject weapon;	//What weapon did they use?
	private string room;		//Which room did the murder take place in?

	public NPC getGuilty(){
		return guiltyNpc;
	}

	public CaseObject getWeapon(){
		return weapon;
	}

	public string getRoom(){
		return room;
	}

	public void setGuilty(NPC theGuilty){
		guiltyNpc = theGuilty;
	}
	
	public void setWeapon(CaseObject theWeapon){
		weapon = theWeapon;
	}
	
	public void setRoom(string theRoom){
		room = theRoom;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
