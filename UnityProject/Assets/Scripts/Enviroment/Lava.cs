using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
	public float damagePerSecond = 20f;
	public Ability deathAbility;

	private void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && !other.isTrigger) {
			other.gameObject.GetComponent<PlayerHealth>().decreaseHealth(Time.deltaTime * damagePerSecond, deathAbility);
		}
	}
}
