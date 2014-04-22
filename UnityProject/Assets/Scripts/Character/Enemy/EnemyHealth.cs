using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {


	public float maxHP = 1f;
	private float hp;
	public float damageAmount = 1.0f;
	public bool dead = false;
	public bool invulnerable;

	private float collistionTimer = 0f;
	private bool kill = false;

	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		hp = maxHP;
	}

	void Update() {
	}

	public float getHP() {
		return hp;
	}

	private IEnumerator Invulnerability() {
		invulnerable = true;
		yield return new WaitForSeconds (0.5f);
		invulnerable = false;
	}

	public bool TakeDamage(float damage) {
		if (invulnerable) {
			return false;
		} else {
			StartCoroutine(Invulnerability());
		}

		hp -= damage;
		if (hp <= 0f) {
			Die();
			return true;
		} else {
			return false;
		}
	}
	
	void Die() {
		GetComponent<EnemyBase> ().OnDie ();
		Destroy (gameObject);
		//collistionTimer = 0.1f;
		/*dead = true;

		Collider2D[] cols = GetComponents<Collider2D> ();
		foreach (Collider2D col in cols) {
			col.isTrigger = true;
		}
		rigidbody2D.gravityScale = 1f;
		rigidbody2D.isKinematic = false;*/
	}

}
