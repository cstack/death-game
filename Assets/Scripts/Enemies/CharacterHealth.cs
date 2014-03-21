using UnityEngine;
using System.Collections;

public class CharacterHealth {

	public float hp = 1f;
	public float damageAmount = 1f;

	public bool TakeDamage(float damage) {
		hp -= damage;
		if (hp <= 0f) {
			Die();
			return true;
		} else
			return false;
	}
	
	private virtual void Die() {}
}
