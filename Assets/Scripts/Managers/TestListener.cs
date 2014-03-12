using UnityEngine;
using System.Collections;

public class TestListener : MonoBehaviour, IEventListener {

	// Use this for initialization
	void Start () {
		EventManager.Instance.AttachListener (this, "Event_NearNPC", this.HandleDetectNear);
	}

	// Update is called once per frame
	void Update () {
		EventManager.Instance.QueueEvent (new Event_NearNPC(50, 50));
	}

	public class Event_NearNPC : BaseEvent {
		public readonly float m_loc;
		public readonly float m_otherloc;
		public Event_NearNPC(float loc, float otherloc) { m_loc = loc; m_otherloc = otherloc; }
		
		public float GetLoc { get { return m_loc; } }
		public float GetOtherLoc { get { return m_otherloc; } }
	}

	private bool HandleDetectNear(IEvent evt) { 
		Event_NearNPC castEvent = evt as Event_NearNPC;
		
		if (transform.position.x < -4.373567 ) {
			//Debug.Log ("wow!");
			EventManager.Instance.DetachListener(this, "Event_NearNPC", this.HandleDetectNear);
			return true;
		}
		
		//Debug.Log ("u did it");
		return true;
	}
}
