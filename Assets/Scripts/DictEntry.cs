using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/*
 * This class is meant to hold game state as the NPC or Player knows it
 * When you talk to certain NPCs, they update the players Dictionary with
 * their own entries.  Entries are the key to unlocking different dialogue
 * options (they act as preconditions).
*/
public class DictEntry {
	
	public NPCNames index;			//Look up npcs through a string index (their name)
	public GuiltLevel guilt;		//Their guilt level
	public Category weapon;			//Their weapon
	public string location;			//Their location
	public float trust;				//Their trust

	public DictEntry(NPCNames ind, GuiltLevel gui, Category weap, string loc, float tru){
		index = ind;
		guilt = gui;
		weapon = weap;
		location = loc;
		trust = tru;
	}

	public NPCNames getIndex(){
		return index;
	}

	public GuiltLevel getGuilt(){
		return guilt;
	}

	public Category getWeapon(){
		return weapon;
	}

	public string getLocation(){
		return location;
	}

	public float getTrust(){
		return trust;
	}

	//Print entry to console, useful for testing/debugging
	//Does not print relations
	public void printEntry(){
		Debug.LogWarning ("[ " + index.ToString() + "-> Guilt: " + guilt.ToString () + ", Weapon: " + weapon.ToString () + ", Loc: " + location + 
					", Trust: " + trust + " ]\n");
		}

	//When given an entry whose index matches, update values to the new entry
	//Also, only updates relevent values, if there are -1 or empty strings in the newEntry, older values are kept
	public void updateEntry(DictEntry newEntry){

		if (index == newEntry.getIndex ()) {
			if ((int) newEntry.getGuilt () >= 0)
				guilt = newEntry.getGuilt ();

			if ((int) newEntry.getWeapon () >= 0)
				weapon = newEntry.getWeapon ();

			if (newEntry.getLocation () != "-1")
				location = newEntry.getLocation ();

			if (newEntry.getTrust () >= 0)
				trust = newEntry.getTrust ();
		} 
	}

	//Alter the Equals() to allow DictEntry comparison, handy for comparing a preCondition to the current player knowledge
	//Always skip any elements that are -1 or null, this is useful for checking preConditions that only check specific parts
	//of an entry, this is done by matching the temporary otherEntry values to the current
	//If the index doesnt match, this is always false
	public override bool Equals (object o){
		DictEntry otherEntry = (DictEntry)o;

		if (otherEntry == null)
			return false;

		if (otherEntry.getGuilt () == GuiltLevel.unrelated)
			otherEntry.guilt = guilt;

		if (otherEntry.getWeapon () == Category.unrelated)
			otherEntry.weapon = weapon;

		if (otherEntry.getLocation () == "-1")
			otherEntry.location = location;

		if (otherEntry.getTrust () == -1.0) 
			otherEntry.trust = trust;
				

		if (index == otherEntry.getIndex () && 
		    guilt == otherEntry.getGuilt ()  && 
		    weapon == otherEntry.getWeapon ()  && 
		    location == otherEntry.getLocation ()  && 
		    trust == otherEntry.getTrust () ) {
			return true;
		}
		
		return false;
	}

	//Alter the == to allow DictEntry comparison, handy for comparing a preCondition to the current player knowledge	
	public static bool operator ==(DictEntry oldEntry, DictEntry otherEntry){

		return oldEntry.Equals (otherEntry);

	}

	//Alter the != to allow DictEntry comparison, handy for comparing a preCondition to the current player knowledge	
	public static bool operator !=(DictEntry oldEntry, DictEntry newEntry){
		
		if (oldEntry.getIndex() != newEntry.getIndex () && 
		    oldEntry.getGuilt() != newEntry.getGuilt ()  && 
		    oldEntry.getWeapon() != newEntry.getWeapon ()  && 
		    oldEntry.getLocation() != newEntry.getLocation ()  && 
		    oldEntry.getTrust() != newEntry.getTrust () ) {
			return true;
		}
		
		return false;
	}

}
