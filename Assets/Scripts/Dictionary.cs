using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Dictionary {

	private List<DictEntry> entries;

	public Dictionary(){
		entries = new List<DictEntry> ();
	}

	public Dictionary(Dictionary other){
		entries = new List<DictEntry> (other.retrieveList ());
	}
	
	public int size(){
		return entries.Count;
	}

	public List<DictEntry> retrieveList(){
		return entries;
	}

	//Search for the ListIndex (as int) of a DictEntry (its index is a string), returns -1 if not found
	public int findIndex(NPCNames indexToFind){

		for (int i = 0; i < entries.Count; i++) {
			NPCNames dictIndex = entries[i].getIndex ();

			if (dictIndex == indexToFind){
				return i;
			}

		}

		return -1;
	}

	//Print the entire list, debug tool
	public void printEntries(){
		for (int i = 0; i < entries.Count; i++) {
			entries[i].printEntry();
		}
	}

	//Add a new entry to the dictionary
	public void addNewEntry(DictEntry newEntry){
		entries.Add (newEntry);
	}

	//Give the dictionary an entry to update, returns true if successful, false if the update failed
	public bool updateDictionary(DictEntry newEntry){

		int tempIndex = findIndex (newEntry.getIndex ());

		if (tempIndex >= 0) {
			entries [tempIndex].updateEntry (newEntry);
			return true;
		}

		Debug.Log("Could not find entry by the index:     " + newEntry.getIndex ());
		return false;

	}

	//Look in the dictionary for an entry with the given index
	public DictEntry retrieveEntry(NPCNames lookupIndex){

		int tempIndex = findIndex (lookupIndex);

		if (tempIndex >= 0) {
			return entries[tempIndex];
		}

		Debug.Log("Could not find entry by the index:     " + lookupIndex);
		return null;

	}

	//Access dictionary by an int value (like a list/array)
	public DictEntry this[int i]{
		get{ return this.entries [i];}
		set{ this.entries [i] = value;}
	}



}




