using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AccelerateAndBrake : MonoBehaviour {
	public Graphic accelerator;
	public bool triggered = false;

	RectTransform rectT;
	Vector2 centerPoint;

	private float accelerateVal = 0f;
	private float maxSpeed = 1000f;

	private bool accelerating = false;

	public float GetAccelerate(){
		return accelerateVal;
	}

	public float GetBrake(){
		return accelerateVal;
	}

	public bool GetAccelerating(){
		return accelerating;
	}

	public void Start(){
		rectT = accelerator.rectTransform;
		InitEvents ();
		UpdateRect ();
	}

	public void InitEvents(){
		EventTrigger events = accelerator.gameObject.AddComponent<EventTrigger> ();

		if (events == null) {
			events = accelerator.gameObject.AddComponent<EventTrigger> ();
		}

		if (events.triggers == null) {
			events.triggers = new System.Collections.Generic.List<EventTrigger.Entry> ();
		}

		EventTrigger.Entry entry = new EventTrigger.Entry ();
		EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent ();
		UnityAction<BaseEventData> functionCall = new UnityAction<BaseEventData> (DragEvent);
		callback.AddListener (functionCall);
		entry.eventID = EventTriggerType.Drag;
		entry.callback = callback;

		events.triggers.Add (entry);

		entry = new EventTrigger.Entry ();
		callback = new EventTrigger.TriggerEvent ();
		functionCall = new UnityAction<BaseEventData> (ReleaseEvent);
		callback.AddListener (functionCall);
		entry.eventID = EventTriggerType.PointerUp;
		entry.callback = callback;

		events.triggers.Add (entry);

		entry = new EventTrigger.Entry ();
		callback = new EventTrigger.TriggerEvent ();
		functionCall = new UnityAction<BaseEventData> (PressEvent);
		callback.AddListener (functionCall);
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback = callback;

		events.triggers.Add (entry);
	}

	void UpdateRect(){
		Vector3[] corners = new Vector3[4];
		rectT.GetWorldCorners (corners);

		for (int i = 0; i < 4; i++) {
			corners [i] = RectTransformUtility.WorldToScreenPoint (null, corners [i]);
		}

		Vector3 bottomLeft = corners [0];
		Vector3 topRight = corners [2];
		float width = topRight.x - bottomLeft.x;
		float height = topRight.y - bottomLeft.y;

		Rect _rect = new Rect (bottomLeft.x, topRight.y, width, height);
		centerPoint = new Vector2 (_rect.x + _rect.width * 0.1f, _rect.y - _rect.height * 0.1f);
	}

	public void DragEvent(BaseEventData eventData){
		Vector2 pointerPos = ((PointerEventData)eventData).position;
		float newSpeed = Vector2.Distance (pointerPos,centerPoint)*100f;
		triggered = true;
		if (newSpeed > maxSpeed) {
			accelerateVal = maxSpeed;
		} else {
			accelerateVal = newSpeed;
		}
	}

	public void ReleaseEvent(BaseEventData eventData){
		accelerating = false;
		triggered = false;
	}

	public void PressEvent(BaseEventData eventData){
		accelerating = true;
		triggered = true;
	}
}