// Anthony Lim
// Taken from http://bibdy.net/index.php/projects/furious-tourist-unity3d/138-unity3d-c-event-manager

/*****************
 * Example usage of the Event Manager
 * 
 * public class TestListener : MonoBehaviour, IEventListener {
 *
 *	// Use this for initialization
 *	void Start () {
 *		EventManager.Instance.AttachListener (this, "Event_NearNPC", this.HandleDetectNear);
 *	}
 *
 *	// Update is called once per frame
 *	void Update () {
 *		EventManager.Instance.QueueEvent (new Event_NearNPC(50, 50));
 *	}
 *
 *	public class Event_NearNPC : BaseEvent {
 *		public readonly float m_loc;
 *		public readonly float m_otherloc;
 *		public Event_NearNPC(float loc, float otherloc) { m_loc = loc; m_otherloc = otherloc; }
 *		
 *		public float GetLoc { get { return m_loc; } }
 *		public float GetOtherLoc { get { return m_otherloc; } }
 *	}
 *
 *	private bool HandleDetectNear(IEvent evt) { 
 *		Event_NearNPC castEvent = evt as Event_NearNPC;
 *		
 *		if (transform.position.x < -4.373567 ) {
 *			Debug.Log ("wow!");
 *			EventManager.Instance.DetachListener(this, "Event_NearNPC", this.HandleDetectNear);
 *			return true;
 *		}
 *		
 *		Debug.Log ("u did it");
 *		return true;
 *	 }
 * } 
 *****************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate bool DelegateEventHandler(IEvent evt);

public interface IEventListener {}

public interface IEvent {
	string GetName();
}

public class BaseEvent : IEvent {
	public string GetName() { return this.GetType().Name; }
}

public class EventManager : MonoBehaviour
{
	public bool LimitQueueProcesing = false;
	public float QueueProcessTime = 0.0f;
	private static EventManager s_Instance = null;
	private Hashtable m_listenerTable = new Hashtable();
	private Queue m_eventQueue = new Queue();

	// override so we don't have the typecast the object
	public static EventManager Instance 
	{
		get {
			if (s_Instance == null) {
				s_Instance = new GameObject("EventManager").AddComponent<EventManager>();
			}
			return s_Instance;
		}
	}

	//Add a listener to the event manager that will receive any events of the supplied event name.
	public bool AttachListener(IEventListener listener, string eventName, DelegateEventHandler handler)
	{
		if (listener == null || eventName == null)
		{
			Debug.Log("Event Manager: AttachListener failed due to no listener or event name specified.");
			return false;
		}
		
		if (!m_listenerTable.ContainsKey(eventName)) {
			m_listenerTable.Add(eventName, new ArrayList());
		}
		
		ArrayList listenerList = m_listenerTable[eventName] as ArrayList;
		if (listenerList.Contains(handler))
		{
			Debug.Log("Event Manager: Listener: " + listener.GetType().ToString() + " is already in list for event: " + eventName);
			return false; //listener already in list
		}
		
		listenerList.Add(handler);
		return true;
	}
	
	public bool DetachListener (IEventListener listener) {
		foreach (DictionaryEntry pair in m_listenerTable) {
			ArrayList listenerList = pair.Value as ArrayList;
			listenerList.Remove (listener);
		}
		return true;
	}
	
	//Remove a listener from the subscribed to event.
	public bool DetachListener(IEventListener listener, string eventName, DelegateEventHandler handler)
	{
		if (!m_listenerTable.ContainsKey(eventName))
			return false;
		
		ArrayList listenerList = m_listenerTable[eventName] as ArrayList;
		if (!listenerList.Contains(handler))
			return false;
		
		listenerList.Remove(handler);
		return true;
	}
	
	//Trigger the event instantly, this should only be used in specific circumstances,
	//the QueueEvent function is usually fast enough for the vast majority of uses.
	public bool TriggerEvent(IEvent evt)
	{
		string eventName = evt.GetName();
		if (!m_listenerTable.ContainsKey(eventName))
		{
			Debug.Log("Event Manager: Event \"" + eventName + "\" has no listeners!");
			return false; //No listeners for event so ignore it
		}
		
		ArrayList listenerList = m_listenerTable[eventName] as ArrayList;
		foreach (DelegateEventHandler listener in listenerList)
		{
			if (listener(evt))
				return true; //Event consumed.
		}
		
		return true;
	}
	
	//Inserts the event into the current queue.
	public bool QueueEvent(IEvent evt)
	{
		if (!m_listenerTable.ContainsKey(evt.GetName()))
		{
			Debug.Log("EventManager: QueueEvent failed due to no listeners for event: " + evt.GetName());
			return false;
		}
		
		m_eventQueue.Enqueue(evt);
		return true;
	}
	
	//Every update cycle the queue is processed, if the queue processing is limited,
	//a maximum processing time per update can be set after which the events will have
	//to be processed next update loop.
	void Update()
	{
		float timer = 0.0f;
		while (m_eventQueue.Count > 0)
		{
			if (LimitQueueProcesing)
			{
				if (timer > QueueProcessTime)
					return;
			}
			
			IEvent evt = m_eventQueue.Dequeue() as IEvent;
			if (!TriggerEvent(evt))
				Debug.Log("Error when processing event: " + evt.GetName());
			
			if (LimitQueueProcesing)
				timer += Time.deltaTime;
		}
	}
	
	public void OnApplicationQuit()
	{
		m_listenerTable.Clear();
		m_eventQueue.Clear();
		s_Instance = null;
	}

}