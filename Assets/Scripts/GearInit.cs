using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GearInit : MonoBehaviour {
	public Graphic gearD;
	public Graphic gearR;
	public bool reverseGear=false;

	// Use this for initialization
	void Start () {
		gearD.enabled = true;
		gearR.enabled = false;
	}
	
	public void GearPressEvent(){
		if (gearD.enabled) {
			Debug.Log("GearD");
			gearD.enabled = false;
			gearR.enabled = true;
			reverseGear = false;
		} else {
			Debug.Log("GearR");
			gearD.enabled = true;
			gearR.enabled = false;
			reverseGear = true;
		}
	}
}
