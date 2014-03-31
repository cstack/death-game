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
		if (col.gameObject.tag == "PlayerHead") {
			plyr = col.transform.parent.gameObject;
			if (!plyr.GetComponent<Player>().canBreathUnderwater) {
				plyr.SendMessage("headEnterWater");
				drowning = true;
				drwntime = drownTimer;
			}
		} else if (col.gameObject.tag == "Player") {
			plyr = col.gameObject;
			plyr.SendMessage("feetEnterWater");
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "PlayerHead") {
			plyr = col.transform.parent.gameObject;
			plyr.SendMessage("headExitWater");
			drowning = false;
		} else if (col.gameObject.tag == "Player") {
			plyr = col.gameObject;
			plyr.SendMessage("feetExitWater");
		}
	}

}
