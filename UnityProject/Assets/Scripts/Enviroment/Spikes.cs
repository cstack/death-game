using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {
	public Ability deathAbility;
	public bool friendly;

	private void Start() {
		GetComponentInChildren<DaggerThrower> ().friendly = friendly;
	}

	private void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == GlobalConstant.Tag.Player && !friendly) {
			other.gameObject.GetComponent<PlayerHealth> ().decreaseHealth (100, deathAbility);
		} else if (other.gameObject.tag == GlobalConstant.Tag.Enemy) {
			Destroy(other.gameObject);
		}
	}
}
