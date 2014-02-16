using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Dictionary : MonoBehaviour{

	private List<DictEntry> entries;


	//Search for the ListIndex (as int) of a DictEntry (its index is a string), returns -1 if not found
	public int findIndex(string indexToFind){

		for (int i = 0; i < entries.Count; i++) {
			string dictIndex = entries[i].getIndex ();

			if (dictIndex.Equals(indexToFind)){
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

		print("Could not find entry by the index:     " + newEntry.getIndex ());
		return false;

	}

	//Look in the dictionary for an entry with the given index
	public DictEntry retrieveEntry(string lookupIndex){

		int tempIndex = findIndex (lookupIndex);

		if (tempIndex >= 0) {
			return entries[tempIndex];
		}

		print("Could not find entry by the index:     " + lookupIndex);
		return null;

	}


}




