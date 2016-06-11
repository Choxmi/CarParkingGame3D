using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollitionDetect : MonoBehaviour {
	public Text gameOver;

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Over") {
			gameOver.text = "GAME OVER";
		}
	}
}
