// ------------------------------------------------------------------------------
// CaseGenerator
// This class initializes the case and takes care of the random generation.
// It's only purpose is to determine: Murderer, Weapon, Location and to set the alibies of the different NPCs.
// ------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseGenerator : Object {
	
	private List<NPC> npcs = new List<NPC>();
	private List<CaseObject> activeWeapons = new List<CaseObject>();
	private List<string> rooms = new List<string>();
	private NPC guilty;
	private CaseObject weapon;
	private Case theCase;
	
	public CaseGenerator(){
		//Getting all NPCs in a separate list
		foreach(NPC n in GameManager.npcList){
			npcs.Add(n);
		}

		foreach (string r in GameManager.roomList) {
			rooms.Add(r);		
		}

		}

	public Case generateCase(){

		theCase = new Case ();


		//Getting one to be guilty at random and removing it from the list
		int rg = Random.Range(0, npcs.Count - 1);
		guilty = makeGuilty (npcs[rg]);
		npcs.Remove (npcs[rg]);
		Debug.Log ("Guilty is " + guilty.elementName);

		//Activate guilty weapon
		foreach (CaseObject c in GameManager.weaponList) {
			if (c.category == guilty.weaponProficiency){
				activeWeapons.Add(c);
			}
		}

		int rgw = Random.Range(0, activeWeapons.Count - 1);
		weapon = activateWeapon (activeWeapons[rgw]);
		Debug.Log ("Weapon is " + weapon.elementName);


		//Same with rooms
		int roomrandom = Random.Range(0, rooms.Count - 1);
		GameManager.theCase.setRoom(rooms[roomrandom]);
		GameManager.room = rooms [roomrandom];
		rooms.Remove (rooms[roomrandom]);
		Debug.Log ("Room is " + theCase.getRoom());

		//Setting only a percentage (right now half) of the ones left to be suspect, other to be witness
		//Maybe a for each until a certain range sets suspects, for each of a 
		int threshold = npcs.Count / 2;

		for (int i=0; i<threshold; i++) {
			string room = rooms[Random.Range(0, rooms.Count-1)];
			npcs[i] = makeSuspect(npcs[i], room);
		}

		for (int i=threshold; i<npcs.Count; i++) {
			string room = rooms[Random.Range(0, rooms.Count-1)];
			NPC suspect = npcs[Random.Range(0, npcs.Count-1)];
				npcs[i] = makeWitness(npcs[i], room, suspect);
		}

		return theCase;
	}
	
	
	private NPC makeGuilty(NPC n){
		n.setGuilt (GuiltLevel.guilty);
		n.alibi.Add(n.personalSentence);
		n.alibi.Add(GameManager.roomList[Random.Range(0, GameManager.roomList.Count-1)]);
		n.convo = n.alibi [0] + " I was in the " + n.alibi [1] + ".";
		Debug.Log ("Murderer " + n.elementName + " conversation: " + n.convo);
		GameManager.guilty = n;
		//GameManager.theCase.setGuilty (n);
		return n;
	}	


	private NPC makeSuspect(NPC n, string r){
		n.setGuilt (GuiltLevel.suspect);
		n.alibi.Add(n.personalSentence);
		n.alibi.Add (r);
		n.convo = n.alibi [0] + " I was in the " + n.alibi [1] + ".";
		Debug.Log ("Suspect " + n.elementName + " conversation: " + n.convo);
		return n;
	}

	private NPC makeWitness(NPC n, string r, NPC s){
		n.setGuilt (GuiltLevel.witness);
		n.alibi.Add(n.personalSentence);
		n.alibi.Add (r);
		n.alibi.Add (s.elementName);
		n.convo = n.alibi [0] + " I was in the " + n.alibi [1] + " with " + n.alibi[2] +  ".";
		Debug.Log ("Witness " + n.elementName + " conversation: " + n.convo);
		return n;
	}

	private CaseObject activateWeapon(CaseObject o){
		o.setGuilt (GuiltLevel.guilty);
		//GameManager.theCase.setWeapon (o);
		GameManager.weapon = o;
		return o;
		}

	
	private void setAlibi(NPC n){
		
		
	}

	
	//NPC npc1 = new NPC(Noel Alt, Description, Whatever);
	
	
	
}

