﻿using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {
	public Ability deathAbility;
	public bool friendly;

	private void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == GlobalConstant.Tag.Player && !friendly && !other.isTrigger) {
			other.gameObject.GetComponent<PlayerHealth> ().decreaseHealth (90, deathAbility);
		} else if (other.gameObject.tag == GlobalConstant.Tag.Enemy) {
			other.GetComponent<EnemyHealth>().TakeDamage(1);
		}
	}
}
