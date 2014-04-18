using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.tag == "Enemy"){
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(1f);
		}
	}
}
