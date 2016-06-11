using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {
	public GameObject focus;
	private GameObject car;
	private Vector2 lastPoint;

//	float rotationRate = 1.0f;
//	bool wasRotating=false;
//
//	Vector2 scrollPosition = Vector2.zero;
//	float scrollVelocity = 0;
//	float timeTouchPhaseEnded = 0;
//	float hitz = 50f;
//
//	float itemInertiaDuration = 1.0f;
//	float itemTimeTOuchPhaseEnded=0;
//	float rotateVelocityX = 0;
//	float rotateVelocityY = 0;
//
//	RaycastHit hit;
//	int layerMask = (1<<8)|(1<<2);
//
//	public void Start(){
//		layerMask = ~layerMask;
//	}

	public void Update () {
		
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			
			car = GameObject.FindWithTag ("Car");
			if (car.GetComponent<SteeringWheel> ().triggered == false && car.GetComponent<AccelerateAndBrake>().triggered == false) {
//		Vector3 camPosition = car.GetComponent<Rigidbody> ().position;
//		camPosition.y = 30f;
//		camPosition.z = camPosition.z - 20f;
//		this.transform.position = camPosition;
//		transform.LookAt (car.transform);
//		transform.Translate (Vector3.right * Time.deltaTime);
				transform.LookAt (focus.GetComponent<Rigidbody>().position);

				float offsetX;
				float offsetY;
				if (Input.touches.Length != 0) {
					Touch evt = Input.touches [0];
					if (evt.phase == TouchPhase.Began) {
						lastPoint = evt.position;
					} else if (evt.phase == TouchPhase.Moved) {
						offsetX = evt.position.x - lastPoint.x;
						offsetY = evt.position.y - lastPoint.y;
						float posi = transform.position.y;
						float posiX = transform.position.x;

						if ((transform.position.y + 0.05f * offsetY) < 5) {
							transform.position = new Vector3 (transform.position.x,5f,transform.position.z);
						}

						if ((transform.position.y) >= 5) {
							transform.RotateAround (car.transform.position, Vector3.up, 0.1f * offsetX);
							transform.RotateAround (car.transform.position, Vector3.left, 0.1f * offsetY);
						}

						lastPoint = evt.position;
					} else if (evt.phase == TouchPhase.Ended) {
						Debug.Log ("Ended");
					}
				}
			}
		}
	}

//	public void FixedUpdate(){
//		if (Input.touchCount > 0) {
//			Touch theTouch = Input.GetTouch (0);
//			Ray ray = Camera.main.ScreenPointToRay (theTouch.position);
//
//			if (Physics.Raycast (ray, out hit, hitz , layerMask)) {
//				if (Input.touchCount == 1) {
//					if (theTouch.phase == TouchPhase.Began) {
//						wasRotating = false;
//					}if (theTouch.phase == TouchPhase.Moved) {
//						scene.transform.Rotate (0, theTouch.deltaPosition.x * rotationRate, 0, Space.World);
//						wasRotating = true;
//					}
//				}
//			}
//		}
//	}
}
