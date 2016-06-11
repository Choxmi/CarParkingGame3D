using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] wheels = new Transform[4];
	public float maxTorque=1f;
	public float maxRotate=1f;
	public float maxSpeed = 100f;
	public GameObject gear;
	public float velo; 
	public bool brakeTr = false;

	void Update () {
		bindMeshes ();
		velo = GetComponent<Rigidbody> ().velocity.z/10;
		GetComponent<AudioSource> ().pitch = velo;
		//transform.rotation = Quaternion.Euler (0, transform.rotation.y, transform.rotation.z);
	}

	void FixedUpdate(){
		
		float steer = (gameObject.GetComponent<SteeringWheel> ().GetAngle ());
		float angle = steer/maxRotate;
		wheelColliders [1].steerAngle = angle;
		wheelColliders [2].steerAngle = angle;

		float accelerate = gameObject.GetComponent<AccelerateAndBrake> ().GetAccelerate ();
		bool accelerating = gameObject.GetComponent<AccelerateAndBrake> ().GetAccelerating ();

		if (brakeTr == true) {
			Brake ();
		}

		if (gear.GetComponent<GearInit> ().reverseGear)
			accelerate = -accelerate;
		
			for (int i = 0; i < 4; i++) {
				if (!accelerating) {
					wheelColliders [i].brakeTorque = 5 * Mathf.Abs (accelerate) * maxTorque;
				} else {
					wheelColliders [i].brakeTorque = 0;
					wheelColliders [i].motorTorque = accelerate * maxTorque;
				}
			}

		if (GetComponent<Rigidbody> ().velocity.z > maxSpeed) {
			Vector3 speed = GetComponent<Rigidbody> ().velocity;
			speed.z = maxSpeed;
			GetComponent<Rigidbody> ().velocity = speed;
		}
	}

	void bindMeshes(){
		for (int i = 0; i < 4; i++) {
			Quaternion quat;
			Vector3 pos;
			wheelColliders [i].GetWorldPose (out pos,out quat);
			wheels [i].position = pos;
			wheels [i].rotation = quat;
		}
	}

	public void BrakeTrigger(){
		if (brakeTr == false) {
			brakeTr = true;
		} else {
			brakeTr = false;
		}
	}

	public void Brake(){
		Debug.Log ("Breaking");
		GetComponent<Rigidbody> ().velocity -= GetComponent<Rigidbody> ().velocity*0.05f;
	}

	public void BrakeRelease(){
		brakeTr = false;
	}
}
