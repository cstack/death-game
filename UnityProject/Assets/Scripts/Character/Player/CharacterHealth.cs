using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

	public float hp = 1f;
	public float damageAmount = 1.0f;
	public bool dead = false;

	void Start () {
	}
	
	void Update() {
	}
	
	public bool TakeDamage(float damage) {
		hp -= damage;
		if (hp <= 0f) {
			Die();
			return true;
		} else
			return false;
	}
	
	void Die() {
		dead = true;
		Debug.Log("Character Dead");
	}
}
