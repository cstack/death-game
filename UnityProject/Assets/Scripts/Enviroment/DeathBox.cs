using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
