using UnityEngine;
using System.Collections;

public class EnviromentDamage : MonoBehaviour {

	public int damageAmount = 1;
	public float drownTimer = 0.2f;

	private GameObject plyr;
	private bool drowning = false;
	private float drwntime = 0f;

	void Update () {
		if (drowning && plyr != null) {
			if (drwntime > 0f) {
				drwntime -= Time.deltaTime;
			} else {
				plyr.SendMessage("decreaseBreath", damageAmount);
				drwntime = drownTimer;
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		//		Debug.Log("Player enter");
		if (col.gameObject.tag == "Player") {
			plyr = col.gameObject;
			drowning = true;
			drwntime = drownTimer;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		//		Debug.Log("Player enter");
		if (col.gameObject.tag == "Player") {
			if (plyr != null) {
				drowning = false;
				plyr.SendMessage ("resetBreath");
			}
		}
	}

}
