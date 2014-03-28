using UnityEngine;
using System.Collections;

public class EnemyMeleeAttack : MonoBehaviour {
	public int attackPower = 50;
	public Ability deathAbility = null;

	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerHealth>().decreaseHealth(attackPower, deathAbility);
		}
	}
}
