using UnityEngine;
using System.Collections;

public abstract class CaseElement : MonoBehaviour {

	//Every case element contains these bits of information
	public string elementName;	//Its name
	public string description;	//A text description of it
	public Sprite profileImage; //Its base image (square mug-shot)

	private GuiltLevel guilt;		//How is it related to the case?  
	private int location;			//Where is it during gameplay, every room/scene has a corresponding Int id
	private GameObject conversation;//The associated dialogue when a case element is clicked
	private bool visible;			//Is the element visible in the journal?


	//Setters for all Case Element fields
	public void setElementName(string someName){
		elementName = someName;
	}

	public void setDescription(string someDesc){
		description = someDesc;
	}


	public void setProfileImage(Sprite someImage){
		profileImage = someImage;
	}

	public void setGuilt(GuiltLevel level){
		guilt = level;
	}


	public void setLocation(int somePlace){
		location = somePlace;
	}

	public void setConv(GameObject someConv){
		conversation = someConv;
	}


	//Getters for all Case Element fields
	public string getElementName( ){
		return elementName;
	}
	
	public string getDescription( ){
		return description;
	}


	public Sprite getProfileImage( ){
		return profileImage;
	}
	
	public GuiltLevel getGuilt( ){
		return guilt;
	}


	public int getLocation( ){
		return location;
	}
	
	public GameObject getConv( ){
		return conversation;
	}
}
