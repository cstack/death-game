using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerHealth>().decreaseHealth(1000000, null);
		}
	}
}
