using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestPath : MonoBehaviour {
	public Collider2D hit;
	Vector2 targetPosition;
	Vector2 direction;
	Vector2 closestColl;
	public float maxSpeed = 8f;
	public List<Vector2> newPath = new List<Vector2>();
	Vector2 first;

	void Start(){
		FindClosestPoint ();
	}
	GameObject FindClosestPoint() {
		GameObject[] points;
		points = GameObject.FindGameObjectsWithTag("point");
		GameObject closest= null;
		float distance = Mathf.Infinity;
		Vector2 position = transform.position;
		foreach (GameObject point in points) {
			float curDistance = Vector2.Distance(point.transform.position,position);
			if (curDistance < distance) {
				closest = point;
				distance = curDistance;
			}
		}
		Debug.Log ("closet point: " + closest.transform.position);
		return closest;
	}
	public void move(){
		foreach (Vector2 point in newPath) {
			first.x = point.x;
			first.y = point.y;
			//Debug.Log("In list: "+ point);

			//newPath.Remove(point);
		}
		//Debug.Log ("first passed");
	}
	void FixedUpdate(){	
		float distance;
		if(Input.GetMouseButton(0)){
			//get mouse clicked location and convert them to world point.
			targetPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(targetPosition.x, targetPosition.y, camera.nearClipPlane));
			targetPosition.x = mousePosition.x;
			targetPosition.y = mousePosition.y;
		}
		//if something is on the way, use find path
		if (Physics2D.Linecast(transform.position, targetPosition)){
			/*hit = objectOnWay(targetPosition);
			if(hit != null){
				findPath( hit, targetPosition);
				//Debug.Log("length of newPath: "+ newPath.Count);
				newPath.Reverse();
			}*/
			Vector2 toPoint = FindClosestPoint().transform.position;
			distance = Vector2.Distance (transform.position, toPoint);
			transform.position = Vector2.Lerp (transform.position, toPoint,Time.deltaTime* (maxSpeed/distance));

		}

		//problem
		//move();
		else{
			//move();
			distance = Vector2.Distance (transform.position, targetPosition);
	
			transform.position = Vector2.Lerp (transform.position, targetPosition,Time.deltaTime* (maxSpeed/distance));
			//hit = Physics2D.Raycast(transform.position, direction);*/
		/*	if(hit.collider!=null &&hit.collider != transform.collider2D){
				Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
				List<Vector2> newPath = new List<Vector2>();
				findPath(hit.collider, targetPosition, newPath);*/
				//hit.collider

			//}

		}
		/*else{
			newPath.Remove (first);
		}*/
	/*	if (newPath.Count == 0) {
			Debug.Log("empty");
		}*/
	}
	public Collider2D objectOnWay(Vector2 target){
		Vector2 direct;
		direct.x = target.x - transform.position.x;
		direct.y = target.y - transform.position.y;
		//Debug.Log ("transform.position: " + transform.position);
		//Debug.Log ("direct: " + direct);

		//get the info of that object.
		RaycastHit2D tempHit = Physics2D.Raycast(transform.position, direct);
		//need to change
		if(tempHit.collider!=null && tempHit.collider != transform.collider2D)
			return tempHit.collider;
		else{
			//Debug.Log("returning null");
			return null;

		}
	}

	public void findPath(Collider2D tempColl, Vector2 target){
		PolygonCollider2D coll = (PolygonCollider2D)tempColl;

	//	float dist = Vector2.Distance(transform.position,target);
		closestColl = transform.position;
		foreach(Vector2 corner in coll.points){
			//Debug.Log("Vertex Position: " + corner);
			if (!newPath.Contains(corner)){
				if(Vector2.Distance(corner, target) < Vector2.Distance(closestColl, target)){
					closestColl = corner;
				}
			}
		}
		//Debug.Log("cloest vertex: "+ closestColl);
		if (!newPath.Contains(closestColl)){
			newPath.Add(closestColl);
		}
		//need to change


		if (Physics2D.Linecast (transform.position, closestColl)){
			findPath(objectOnWay(closestColl), closestColl);
		}
	}
	public void drawPath(){
		Vector3 start;
		start.x = transform.position.x;
		start.y = transform.position.y;
		start.z = 10f;
		Vector3 end;
		foreach(Vector2 point in newPath){
			end.x = point.x;
			end.y = point.y;
			end.z = 10f;
			Debug.DrawLine(start, end);
			start = end;
		}
	}
	/*float calDis(Vector2 from, Vector2 to){
		float result;
		float x2 = from.x;
		float x1 = to.x;
		float y2 = from.y;
		float y1 = to.y;
		result = Mathf.Sqrt ((x2 - x1)*(x2 - x1) +(y2-y1)*(y2-y1));
		return result;
	}*/
}
