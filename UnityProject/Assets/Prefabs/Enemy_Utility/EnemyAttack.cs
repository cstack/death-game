using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    public int attackPower;
    public float knockbackSpeed;
    public float knockupSpeed;

	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.tag == "Player"){
            // decrease health
            other.gameObject.GetComponent<PlayerHealth>().decreaseHealth(attackPower);

			/*
            // knock back
            Vector2 knockbackDir = other.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            other.gameObject.GetComponent<PlayerControl>().startKnockback();
            other.rigidbody.velocity = new Vector2(knockbackDir.normalized.x * knockbackSpeed
                , knockupSpeed);
                */

		}
	}
}
