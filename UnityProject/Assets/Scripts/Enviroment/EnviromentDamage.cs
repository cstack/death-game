using UnityEngine;
using System.Collections;

public class EnviromentDamage : MonoBehaviour {

	public float damageAmount = 0.5f;

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("Player enter");
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("TakeDamage", damageAmount);
		}
	}

}
