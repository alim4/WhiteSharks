using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneSetup : MonoBehaviour {

	public string sceneName;
	public int id;
	private List<NPC> npcList;

	// Use this for initialization
	void Start () {
		Debug.Log("HI!!!!!!!!!!!!!!!");
		this.npcList = GameManager.getSceneNPCList(this.id);
		Debug.Log (npcList.Count);
		foreach (NPC n in npcList) 
		{
			string npcName = "NPCs/" + n;
			Debug.Log ("NPC: " + n.name);
			NPC t = (NPC) Instantiate(Resources.Load<NPC>(n.name));
			t.playerObj = GameObject.Find("player");
			Debug.Log ("Result: " + t + " and " + GameObject.Find("GameObject"));		
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
