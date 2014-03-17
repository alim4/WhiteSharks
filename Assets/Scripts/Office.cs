using UnityEngine;
using System.Collections;


/*
 * This class controls the behaviour of the office for the tutorial.
 * We'll probably want to change the way it's implemented later.
 */
public class Office : MonoBehaviour {

	private bool firstTime = true;
	public bool correctAnswer = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (firstTime) {
						StartCoroutine ("firstDialogue");
		} else if (!GameManager.dialogueJustFinished) {
			StartCoroutine("secondDialogue");
		}
		if (GameManager.dialogueJustFinished && firstTime) {
			GameManager.dialogueJustFinished = false;
		} else if (GameManager.dialogueJustFinished && !firstTime){ 
			GameManager.dialogueJustFinished = false;
			//We have to add something that checks if you've answered correctly
			if (correctAnswer){
				GameManager.Instance.finishEpisode();
			} else {
				Application.LoadLevel("finplaza");
			}
		}
	}

	public IEnumerator firstDialogue(){
		yield return new WaitForSeconds (1.5f);
		Dialoguer.StartDialogue(2);
	}

	public IEnumerator secondDialogue(){
		yield return new WaitForSeconds (1.5f);
		Dialoguer.StartDialogue(5);
	}
}
