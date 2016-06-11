using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class SteeringWheel : MonoBehaviour {
	public Graphic steeringWheel;

	RectTransform rectT;
	Vector2 centerPoint;

	public float maximumSteeringAngle = 300f;
	public float wheelReleaseSpeed = 300f;
	public bool triggered = false;

	public float wheelAngle = 0f;
	float wheelPrevAngle = 0f;

	bool wheelBeingHeld = false;

	public float GetClampedValue(){
		return wheelAngle / maximumSteeringAngle;
	}

	public float GetAngle(){
		return wheelAngle;
	}

	public void Start(){
		rectT = steeringWheel.rectTransform;
		InitEventSystem ();
		UpdateRect ();
	}

	public void Update(){
		if (!wheelBeingHeld && !Mathf.Approximately (0f, wheelAngle)) {
			float deltaAngle = wheelReleaseSpeed * Time.deltaTime;
			if (Mathf.Abs (deltaAngle) > Mathf.Abs (wheelAngle))
				wheelAngle = 0f;
			else if (wheelAngle > 0f)
				wheelAngle -= deltaAngle;
			else
				wheelAngle += deltaAngle;
		}

		rectT.localEulerAngles = (new Vector3 (0,0,-wheelAngle));
	}

	void InitEventSystem(){
		EventTrigger wheelEvents = steeringWheel.gameObject.AddComponent<EventTrigger> ();

		if (wheelEvents == null) {
			wheelEvents = steeringWheel.gameObject.AddComponent<EventTrigger> ();
		}

		if (wheelEvents.triggers == null) {
			wheelEvents.triggers = new System.Collections.Generic.List<EventTrigger.Entry> ();
		}

		EventTrigger.Entry entry = new EventTrigger.Entry ();
		EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent ();
		UnityAction<BaseEventData> functionCall = new UnityAction<BaseEventData> (PressEvent);
		callback.AddListener (functionCall);
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback = callback;

		wheelEvents.triggers.Add (entry);

		entry = new EventTrigger.Entry ();
		callback = new EventTrigger.TriggerEvent ();
		functionCall = new UnityAction<BaseEventData> (DragEvent);
		callback.AddListener (functionCall);
		entry.eventID = EventTriggerType.Drag;
		entry.callback = callback;

		wheelEvents.triggers.Add (entry);

		entry = new EventTrigger.Entry ();
		callback = new EventTrigger.TriggerEvent ();
		functionCall = new UnityAction<BaseEventData> (ReleaseEvent);
		callback.AddListener (functionCall);
		entry.eventID = EventTriggerType.PointerUp;
		entry.callback = callback;

		wheelEvents.triggers.Add (entry);
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
		centerPoint = new Vector2 (_rect.x + _rect.width * 0.5f, _rect.y - _rect.height * 0.5f);
	}

	public void PressEvent(BaseEventData eventData){
		Vector2 pointerPos = ((PointerEventData)eventData).position;
		triggered = true;

		wheelBeingHeld = true;
		wheelPrevAngle = Vector2.Angle (Vector2.up, pointerPos - centerPoint);
	}

	public void DragEvent(BaseEventData eventData){
		Vector2 pointerPos = ((PointerEventData)eventData).position;
		triggered = true;
		float wheelNewAngle = Vector2.Angle (Vector2.up, pointerPos - centerPoint);

		if (Vector2.Distance (pointerPos, centerPoint) > 0f) {
			if (pointerPos.x > centerPoint.x)
				wheelAngle += wheelNewAngle - wheelPrevAngle;
			else
				wheelAngle -= wheelNewAngle - wheelPrevAngle;
		}
		wheelAngle = Mathf.Clamp (wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
		wheelPrevAngle = wheelNewAngle;
	}

	public void ReleaseEvent(BaseEventData eventData){
		DragEvent (eventData);
		triggered = false;
		wheelBeingHeld = false;
	}
		
}
