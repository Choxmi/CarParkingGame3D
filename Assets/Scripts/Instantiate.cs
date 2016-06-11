using UnityEngine;
using System.Collections;

public class Instantiate : MonoBehaviour {
	private GameObject floor;
	private GameObject floorLimit;
	private GameObject jeep;
	private Object[,] floorArr = new Object[20,10];
	private int[] excludeRowF = new int[]{6,8,10,12};
	private int[] excludeColumnF = new int[]{3,3,3,3,3};
	private int[] excludeRowB = new int[]{6,8,10,12};
	private int[] excludeColumnB = new int[]{5,5,5,5,5};
	private int[] excludeRowL = new int[]{4,6,8,10,12};
	private int[] excludeColumnL = new int[]{0,0,0,0,0,0};
	private int[] excludeRowR = new int[]{4,6,8,10,12};
	private int[] excludeColumnR = new int[]{9,9,9,9,9,9};

	void Start () {
		floor = GameObject.FindWithTag ("Player");
//		for (int j = 0; j < 20; j++) {
//			for (int i = 0; i < 10; i++) {
//				if ((System.Array.Exists (excludeRow, element => element == j) && System.Array.Exists (excludeColumn, element => element == i))) {
//					
//				} else {
//					floorArr [j, i] = GameObject.Instantiate (floor, new Vector3 ((j * 20), 0, (i * 40)), rotation);
//				}
//			}
//		}

		for (int x = 0; x < excludeRowF.Length; x++) {
			Quaternion rotation = Quaternion.Euler (0, 0, 0);
			floorLimit = GameObject.FindWithTag ("Barricade");
			var block = GameObject.Instantiate (floorLimit, new Vector3 ((excludeRowF[x]* 20), 0, (excludeColumnF[x] * 40)), rotation);
		}

		for (int x = 0; x < excludeRowB.Length; x++) {
			Quaternion rotation = Quaternion.Euler (0, 180, 0);
			floorLimit = GameObject.FindWithTag ("Barricade");
			jeep = GameObject.FindWithTag ("Dummy");
			var block = GameObject.Instantiate (floorLimit, new Vector3 ((excludeRowB[x]* 20), 0, (excludeColumnB[x] * 40)), rotation);
			var blockJeep = GameObject.Instantiate (jeep, new Vector3 ((excludeRowB[x]* 20), 0, (excludeColumnB[x] * 40)), rotation);
		}

		for (int x = 0; x < excludeRowL.Length; x++) {
			Quaternion rotation = Quaternion.Euler (0, 90, 0);
			floorLimit = GameObject.FindWithTag ("Finish");
			var block = GameObject.Instantiate (floorLimit, new Vector3 ((excludeColumnL[x] * 40), 0,(excludeRowL[x]* 20) ), rotation);
		}

		for (int x = 0; x < excludeRowR.Length; x++) {
			Quaternion rotation = Quaternion.Euler (0, 90, 0);
			floorLimit = GameObject.FindWithTag ("Finish");
			var block = GameObject.Instantiate (floorLimit, new Vector3 ((excludeColumnR[x] * 40), 0,(excludeRowR[x]* 20) ), rotation);
		}
	}

	void Update () {
	
	}
}
