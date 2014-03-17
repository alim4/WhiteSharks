using UnityEngine;
using System.Collections;


/*
 * This class controls the behaviour of the office for the tutorial.
 * We'll probably want to change the way it's implemented later.
 */
public class Office : MonoBehaviour {
	
	public bool correctAnswer = false;
	private bool started = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.firstTimeOffice && !started) {
			Debug.Log("Hey");
			StartCoroutine ("firstDialogue");
		} else if (!GameManager.firstTimeOffice && !started) {
			Debug.Log("NO Hey");
			StartCoroutine("secondDialogue");
		}

		if (GameManager.dialogueJustFinished) {
			GameManager.dialogueJustFinished = false;
			GameManager.firstTimeOffice = false;
			StartCoroutine("goToPlaza");
		} //else if (GameManager.dialogueJustFinished && !firstTime){ 
			//We have to add something that checks if you've answered correctly
			//if (correctAnswer){
			//	GameManager.Instance.finishEpisode();
			//} else {
			//	StartCoroutine("goToPlaza");
			//}
		//}
	}

	public IEnumerator firstDialogue(){
		started = true;
		yield return new WaitForSeconds (1.5f);
		Dialoguer.StartDialogue(2);
	}

	public IEnumerator secondDialogue(){
		started = true;
		yield return new WaitForSeconds (1.5f);
		Dialoguer.StartDialogue(5);
	}

	public IEnumerator goToPlaza(){
		yield return new WaitForSeconds (1.5f);
		Application.LoadLevel("finplaza");
	}
}
