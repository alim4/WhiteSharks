/*
npc class. 

changes: added collision with the player. -John Mai

*/
using UnityEngine;
using System.Collections;

public class npcScript_old : MonoBehaviour {
	
	public GameObject conversation, playerObj;

	//NPC sprite info goes here
	public ArrayList spriteMap; //0 is the neutral pose, other images should fill out the array
	public Sprite idleSprite; //The sprite that represents the NPC in the game world

	//NPC basic knowledge goes here, basically what do they know about how they are related to the case
	public int guiltLevel; //0 is not guilty, 1 is a witness, 2 is a suspect, 3 is guilty
	public GameObject murderWeapon; //If the NPC is guilty, this should have a weapon, otherwise it should be empty
	public ArrayList possibleWeapons; //Every possible weapon the NPC COULD have used to commit the murder
	public bool hasAlibi; //does the NPC have an alibi?
	public string murderLocation; //If the NPC was guilty, where did it occur?

	//NPC basic descriptions go here
	public string name; //name of the NPC
	public int age; //How old is the NPC
	public string basicDescription; //A basic description of the NPC, a short paragraph at most
	public string location; //What room is the NPC in?


	//Every getter/setter for NPC data

	//Sprite Map
	public void setSpriteMap (ArrayList theSpriteMap){
		spriteMap = new ArrayList(theSpriteMap);
	}
	
	public ArrayList getSpriteMap (){
		return spriteMap;
	}

	//Idle Sprites
	public void setIdleSprite (Sprite theIdleSprite){
		idleSprite = theIdleSprite;
	}
	
	public Sprite getIdleSprite (){
		return idleSprite;
	}

	//Guilt
	public void setGuiltLevel (int level){
		guiltLevel = level;
	}
	
	public int getGuiltLevel (){
		return guiltLevel;
	}

	//Murder Weapon
	public void setMurderWeapon (GameObject theWeapon){
		murderWeapon = theWeapon;
	}
	
	public GameObject getMurderWeapon (){
		return murderWeapon;
	}

	//Possible weapons
	public void setPossible (ArrayList possibilities){
		possibleWeapons = new ArrayList(possibilities);
	}
	
	public ArrayList getPossible (){
		return possibleWeapons;
	}

	//Alibi?
	public void setHasAlibi (bool yesOrNo){
		hasAlibi = yesOrNo;
	}
	
	public bool getHasAlibi (){
		return hasAlibi;
	}

	//Murder location
	public void setMurderScene (string whereDidItHappen){
		murderLocation = whereDidItHappen;
	}
	
	public string getMurderScene (){
		return murderLocation;
	}



	//enable conversation object if left mouse button is clicked.
	void OnMouseDown(){
		if(Input.GetMouseButton(0)){
			conversation.renderer.enabled = true;
			conversation.collider2D.enabled = true;
		}
	}
	//switch the displaying order of the npc. 
	void Update () {
		if (transform.position.y < playerObj.transform.position.y) {
			renderer.sortingLayerName= "foreground";
			renderer.sortingOrder = 2;
		}
		else{
			renderer.sortingLayerName= "middleground";
		}
	}
}
