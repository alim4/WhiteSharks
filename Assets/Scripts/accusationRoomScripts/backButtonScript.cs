using UnityEngine;
using System.Collections;

public class backButtonScript : MonoBehaviour {

	private int type;
	private float speed = 25.0f;
	private bool moving = false;
	private Vector3 goal;

	// Use this for initialization
	void Start () {
		goal = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			transform.position = Vector3.MoveTowards(
				transform.position, goal, Time.deltaTime * speed);
			//print("reached");
			if (transform.position == goal) {
				moving = false;
			}
		}
	}

	public void setMotion(Vector3 target) {
		goal = target;
		moving = true;
	}

}
