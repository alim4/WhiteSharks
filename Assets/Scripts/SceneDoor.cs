using UnityEngine;
using System.Collections;

public class SceneDoor : MonoBehaviour {
	public int id;
	public float x, y;

	//Mouse icon information
	public string mouseOverIcon = "Door_Icon";
	private string defaultIcon = "Walk_Icon";		//The standard mouse icon when not hovering over an object
	
	public void OnMouseExit(){
		GameManager.Instance.updateMouseIcon (defaultIcon);
	}
	
	public void OnMouseEnter(){
		GameManager.Instance.updateMouseIcon(mouseOverIcon);
	}
}
