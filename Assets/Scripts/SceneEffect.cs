using UnityEngine;
using System.Collections;

public class SceneEffect : MonoBehaviour {
	
	public static bool fadeIn, fadeOut = false;
	public bool ready = false;

	void Start(){
		fadeIn = true;
		//renderer.material.color = Color.white;
	}

	public void fade(){
		renderer.material.color = Color.Lerp(renderer.material.color,Color.clear, 0.1f);
		if(renderer.material.color!= Color.clear)
			fade ();
	}

	// Update is called once per frame
	void Update () {
		if (fadeIn){
			renderer.material.color = Color.Lerp(renderer.material.color,Color.clear, 0.1f);
			if(renderer.material.color == Color.clear){
				fadeIn =false;
			}
		}
		if(fadeOut){
			renderer.material.color = Color.Lerp(renderer.material.color, Color.black, 0.5f);
			if(renderer.material.color == Color.black){
				//fadeOut = false;
				//playerScript.effectReady = true;
			}
		}

	}
}
