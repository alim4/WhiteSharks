using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : CaseElement {

	enum AnimationState //enum to avoid magic numbers in the animation ArrayList
	{
		idle=0,
		conversation
	};
	
	public GameObject playerObj;
	public BoxCollider2D box;

	//NPC specific data fields
	public Category weaponProficiency;	//What kinds of weapons is the NPC skilled with
	public bool highClass;				//Does the NPC belong to the higher class society (top floors) or not?
	public string alibi;			//A set of info that represents an alibi, requires another npc, location
	public ArrayList animations;		//An array list of sprites representing the animation
	public string scene;

	public float trust;
	public NPCNames enumName;
	public List<NPCNames> relations;
	public int myConvo;
	
	public Dictionary npcKnowledge;

	//Mouse icon information
	public string mouseOverIcon = "Speech_Icon";

	void Start(){
		myConvo = GameManager.npcConversations[(int)enumName];
	}
	
	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}

	//enable conversation object if left mouse button is clicked.
	public void OnMouseDown(){
		if(Input.GetMouseButton(0)){
			Debug.LogError("HI");
			if (this.elementName.CompareTo("Liam O'Shea")==0){
				Debug.LogError("HI LIAM");
				GameManager.npcList.Find(x => x.elementName.CompareTo("Liam O'Shea")==0).description = "Liam is a guard at the Fin. He can wield LASER PISTOLS. He says he was at the GYM.";
				journal.Instance.updateNPCs();
			}
			else if (this.elementName.CompareTo("Nina Walker")==0){
				Debug.LogError("HI NINA");
				GameManager.npcList.Find(x => x.elementName.CompareTo("Nina Walker")==0).description = "Nina is a member of the YAP. A successful young teenager. She can wield the eSWORD. She says she was at the CAFÉ.";
				journal.Instance.updateNPCs();
			}
			else if (this.elementName.CompareTo("Josh Susach")==0) {
				Debug.LogError("HI JOSH");
				GameManager.npcList.Find(x => x.elementName.CompareTo("Josh Susach")==0).description = "He's a criminal. He's an artist. He's proud of both. He can wield the METAL PIPE. He says he was at the GYM.";
				journal.Instance.updateNPCs();
			}
			playerScript temp = (playerScript) FindObjectOfType(typeof(playerScript));
			temp.canWalk = false;
			Dialoguer.StartDialogue(myConvo);
			string npcResource = (this.elementName + "Sprite").Replace(" ", string.Empty);
 			Texture2D npcTex = (Texture2D) Resources.Load (npcResource);
 			Debug.Log ("npcResource: " + npcResource);
 			Debug.LogError ("EleName: " + this.elementName);
 			Debug.LogError ("Texture: " + npcTex.ToString());
 
 			DialogueGUI dGUI = GameManager.Instance.GetComponent<DialogueGUI>();
 			Debug.LogError ("dgui: " + dGUI.ToString());
 			dGUI.setTargetTex(npcTex);
 			dGUI.tweenCam();
			//GameManager.npcList.Find(x => x.elementName.CompareTo(this.elementName) == 0).setVisible(true);
			
			/*
			//conversationObj.renderer.enabled = true;
			//conversationObj.collider2D.enabled = true;
			GameManager.npcList.Find(x => x.elementName.CompareTo(this.elementName) == 0).setVisible(true);

			convoBubble.GetComponentInChildren<UILabel>().text = convo;
			convoBubble.GetComponentInChildren<UI2DSprite>().sprite2D = this.getProfileImage();*/
		}
	}

	public NPCNames getEnumName(){
		return enumName;
	}
	
	public Category getWeaponProf(){
		return weaponProficiency;
	}
	
	public float getTrust(){
		return trust;
	}
	
	public string getAlibi(){

		if (alibi == "") {
			alibi = (string) GameManager.Instance.roomIDList[location];
		}

		return alibi;
	}
	
	//switch the displaying order of the npc. 
	void Update () {
		/*if (transform.position.y < playerObj.transform.position.y) {
			renderer.sortingLayerName= "foreground";
			renderer.sortingOrder = 2;
			box.isTrigger = true;
		}
		else{
			renderer.sortingLayerName= "middleground";
			box.isTrigger = false;
		}*/
	}
}
