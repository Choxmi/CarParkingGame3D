using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinCollitionDetect : MonoBehaviour {
	public GameObject car;
	public Text uitext;

	void Update () {
		if (GetComponent<BoxCollider> ().bounds.Contains (car.transform.position)) {
			uitext.text = "You Won";
		}
	}
}
