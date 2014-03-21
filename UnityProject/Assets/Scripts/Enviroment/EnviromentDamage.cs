using UnityEngine;
using System.Collections;

public class EnviromentDamage : MonoBehaviour {

	public float damageAmount = 0.5f;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Player enter");
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage("TakeDamage", damageAmount);
		}
	}

}
