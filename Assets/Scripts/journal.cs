using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class journal : MonoBehaviour {
	//PoI buttons
	public GameObject poI2;

	public List<NPC> personsOfInterest;

	//Test case elements of NPC type
	public Sprite emptyPortrait;
	private string emptyName;
	public NPC npc1;
	public NPC npc2;
	public NPC npc3;

	//Grab view tab buttons. Will change to use gameobject find.
	public GameObject viewTab1;
	public GameObject viewTab2;
	public GameObject viewTab3;

	//Grab buttons and textfield from view. Will change to use gameobject find.
	private List<GameObject> poiButtonList;
	public GameObject poiButton1;
	public GameObject poiButton2;
	public GameObject poiButton3;
	public UILabel poiName;
	public UILabel poiDescription;

	// Use this for initialization
	void Start () {
		//Default name for "invisible" person of interest.
		emptyName = "?????";

		//Person of interest list.
		personsOfInterest = new List<NPC>();
		personsOfInterest.Add(npc1);
		personsOfInterest.Add(npc2);
		personsOfInterest.Add(npc3);

		//Listens for tab button presses in journal and runs onClick with button clicked as parameter.
		/*UIEventListener.Get (viewTab1).onClick += this.onTabClick;
		UIEventListener.Get (viewTab2).onClick += this.onTabClick;
		UIEventListener.Get (viewTab3).onClick += this.onTabClick;*/

		//Listens for button presses in poiView and runs onClick with button clicked as parameter.
		UIEventListener.Get(poiButton1).onClick += this.onClick;
		UIEventListener.Get(poiButton2).onClick += this.onClick;
		UIEventListener.Get(poiButton3).onClick += this.onClick;

		//List of person of interest portrait buttons.
		poiButtonList = new List<GameObject>();
		poiButtonList.Add (poiButton1);
		poiButtonList.Add (poiButton2);
		poiButtonList.Add (poiButton3);

		initPoIView ();
	}

	void Update () {
	}

	//Change journal view by tab buttons.
	//Should have onClick moved into it to provide one general on click event for any button if possible.
	void onTabClick(GameObject button){
		if (button == viewTab1) {
		} 
		else if (button == viewTab2) {
		}
		else {
		}
	}

	//Needs clean up (repetitive). Needs to be general to work with both PoI view and object view
	void onClick(GameObject button){
		if (button == poiButton1) {
			if(personsOfInterest[0] != null){
				poiName.text = personsOfInterest[0].getElementName();
				poiDescription.text = personsOfInterest[0].getDescription();
			}
			else {
				poiName.text = emptyName; 
				poiDescription.text = emptyName;
			}
		}
		else if(button == poiButton2) {
			if(personsOfInterest[1] != null){
				poiName.text = personsOfInterest[1].getElementName();
				poiDescription.text = personsOfInterest[1].getDescription();
			}
			else {
				poiName.text = emptyName; 
				poiDescription.text = emptyName;
			}
		}
		else if(button == poiButton3) {
			if(personsOfInterest[2] != null){
				poiName.text = personsOfInterest[2].getElementName();
				poiDescription.text = personsOfInterest[2].getDescription();
			}
			else {
				poiName.text = emptyName; 
				poiDescription.text = emptyName;
			}
		}
	}

	//Initialize PoI view.
	void initPoIView(){
		for (int i = 0; i < personsOfInterest.Count; i++) {
			//Debug.Log (i);
			if(personsOfInterest[i] != null){
				Debug.Log(personsOfInterest[i].getProfileImage ().ToString ());
				//GetComponent is slow, will use gameobject find component in children in later iteration.
				poiButtonList[i].gameObject.GetComponent<UI2DSprite>().sprite2D = personsOfInterest[i].getProfileImage();
			}
			else {
				poiButtonList[i].gameObject.GetComponent<UI2DSprite>().sprite2D = emptyPortrait;
			}
		}
	}

	void changePoIView(){

	}
}
