// ------------------------------------------------------------------------------
// CaseGenerator
// This class initializes the case and takes care of the random generation.
// It's only purpose is to determine: Murderer, Weapon, Location and to set the alibies of the different NPCs.
// ------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseGenerator : Object {
	
	private ShuffleList<NPC> npcs = new ShuffleList<NPC>(); //Needs to be filled with copy
	private ShuffleList<Category> categories = new ShuffleList<Category>(); //Needs to be filled with copy
	private ShuffleList<CaseObject> weapons = new ShuffleList<CaseObject>();
	private ShuffleList<CaseObject> weaponsA = new ShuffleList<CaseObject>();
	private ShuffleList<CaseObject> weaponsB = new ShuffleList<CaseObject>();
	private ShuffleList<CaseObject> weaponsC = new ShuffleList<CaseObject>();
	private ShuffleList<string> rooms = new ShuffleList<string>(); //Needs to be filled with copy
	private NPC suspectA, suspectB, suspectC;
	private Category categoryA, categoryB, categoryC;
	private string roomA, roomB, roomC;
	private CaseObject weaponA, weaponB, weaponC;

	//Not sure if we still need these
	/*private NPC guilty;
	private CaseObject weapon;*/
	private Case theCase;
	private Random rng = new Random();


	/*
	 * Case Generator
	 * Constructor for the generator class.
	 * It initializes the lists that will be used in the case generation
	 * with a copy of the contents in the game manager.
	 * It also removes the item categories that aren't aplicable to weapons.
	 * (It might change when we decide to use the Case class.)
	 */
	public CaseGenerator(){
			//Getting all NPCs and Rooms in a separate list
			//When we init, we need to get a copy of the lists that are stored in the game manager.
			//Later on, we'll need to make sure that the case is stored correctly

			//Debug.LogError ("Initializing Case Generator");

			foreach(NPC n in GameManager.npcList){
				npcs.Add(n);
			}
			foreach (string r in GameManager.roomList) {
				rooms.Add(r);	
			}
			foreach(CaseObject w in GameManager.weaponList){
				weapons.Add(w);
			}
			System.Array cs = System.Enum.GetValues (typeof(Category));
			foreach(Category c in cs){
				categories.Add(c);
			}
		categories.Remove (Category.None);
		categories.Remove (Category.PersonalItem);
		categories.Remove (Category.Miscellaneous);
		categories.Remove (Category.unrelated);
		}


	/*
	 * generateCase
	 * Generate case handles the case generation. The chosen elements are static positions
	 * of the different lists, but these elements are randomly shuffled by a custom extension
	 * of the List class.
	 * (It's set up to implement the Case class, but that's currently not working.)
	 */
	public Case generateCase(){
		theCase = new Case ();
		//Debug.LogError ("Generating case");
		npcs.Shuffle (rng);
		suspectA = npcs [0];
		suspectB = npcs [1];
		suspectC = npcs [2];
		//Debug.LogError ("Suspects are: " + suspectA +", " + suspectB + " and " + suspectC );

		categories.Shuffle (rng);
		categoryA = categories [0];
		categoryB = categories [1];
		categoryC = categories [2];

		suspectA.weaponProficiency = categoryA;
		suspectB.weaponProficiency = categoryB;
		suspectC.weaponProficiency = categoryC;


		//Debug.LogError ("Categories are: " + categoryA +" and " + categoryB );

		foreach (CaseObject w in weapons){
			if (w.category.CompareTo(categoryA) == 0){
				weaponsA.Add(w);
			}
		}
		Debug.LogError ("Made copy of categoryA weapons, size:" + weaponsA.Count + " and it is " + categoryA);
		foreach (CaseObject w in weapons){
			if (w.category.CompareTo(categoryB) == 0){
				weaponsB.Add(w);
			}
		}
		Debug.LogError ("Made copy of categoryB weapons, size:" + weaponsB.Count+ " and it is " + categoryB);

		foreach (CaseObject w in weapons){
			if (w.category.CompareTo(categoryC) == 0){
				weaponsC.Add(w);
			}
		}

		weaponA = weaponsA [Random.Range (0, weaponsA.Count)];
		weaponB = weaponsB [Random.Range (0, weaponsB.Count)];

		rooms.Shuffle (rng);
		roomA = rooms [0];
		roomB = rooms [1];
		roomC = rooms [2];

		//Now that we have everything randomized, we set up responsibilities (guilty)
		this.makeGuilty (suspectA, roomC);
		this.makeSuspect (suspectB, roomB);
		this.makeSuspect (suspectC, roomC);

		this.activateWeapon (weaponA);
		this.activateWeapon (weaponB);

		Debug.LogError ("Case generated as:");
		Debug.LogError ("Guilty :" +suspectA+ " who is proficient with "+suspectA.weaponProficiency +" and was in " + suspectA.alibi);
		Debug.LogError ("Suspect1 :" +suspectB+ " who is proficient with "+suspectB.weaponProficiency +" and was in " + suspectB.alibi);
		Debug.LogError ("Suspect2 :" +suspectC+ " who is proficient with "+suspectC.weaponProficiency +" and was in " + suspectC.alibi);

		Debug.LogError(weaponA);

		theCase.activeWeapons.Add (weaponA);
		theCase.activeWeapons.Add (weaponB);
		theCase.activeWeapons.Add (weaponC);
		theCase.setGuilty (suspectA);
		theCase.setRoom (roomA);
		theCase.setWeapon (weaponA);

		return theCase;
	}
	
	
	private NPC makeGuilty(NPC n, string r){
		n.setGuilt (GuiltLevel.guilty);
		n.alibi = r;
		GameManager.guilty = n;
		return n;
	}	


	private NPC makeSuspect(NPC n, string r){
		n.setGuilt (GuiltLevel.suspect);
		n.alibi = r;
		return n;
	}

	private CaseObject activateWeapon(CaseObject o){
		o.setGuilt (GuiltLevel.guilty);
		//GameManager.theCase.setWeapon (o);
		GameManager.weapon = o;
		return o;
	}
}


/*
 * ShuffleList<T>
 * This class is an extension of  List<T> to enable shuffling in the lists and enable a
 * more efficient way of randomizing.
 * The shuffling algorithm is based off the Fisher-Yates shuffle.
 * Since the Unity random is not instance based, it should be thread safe.
 */
public class ShuffleList<T> : List<T> {

	//Method ToString implemented for 
	override
	public string ToString(){
		string aux="";
		foreach (T i in this) {
			aux += i.ToString() + "-";
		}
		return aux;
	}

	public void Shuffle(Random rng)  
	{  

		int n = this.Count;  
		while (n > 1) {  
			n--;  
			int k = Random.Range(0, n + 1);  
			T value = this[k];  
			this[k] = this[n];  
			this[n] = value;  
		}  
	}
	
}