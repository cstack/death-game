using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {


	public float hp = 1f;
	public float damageAmount = 1.0f;
	public bool dead = false;

	private float collistionTimer = 0f;
	private bool kill = false;

	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
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
		//collistionTimer = 0.1f;
		dead = true;

		Collider2D[] cols = GetComponents<Collider2D> ();
		foreach (Collider2D col in cols) {
			col.isTrigger = true;
		}
		rigidbody2D.gravityScale = 1f;
		rigidbody2D.isKinematic = false;
	}

}
