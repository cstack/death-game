using UnityEngine;
using System.Collections;

public class AirPocket : MonoBehaviour {

	public EnvironmentDamage fatherWater;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "PlayerHead") {
			fatherWater.enterAirPocket ();
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "PlayerHead") {
			fatherWater.exitAirPocket ();
		}
	}
}
