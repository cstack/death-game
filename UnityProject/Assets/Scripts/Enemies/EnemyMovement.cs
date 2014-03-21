using UnityEngine;
using System.Collections;

public enum EnemyType {
	RedSquid, 
	BigPink
}

public class EnemyMovement : MonoBehaviour {

	public EnemyType enemyType;
	public bool grounded = false;
	public float moveSpeed = 4f;
	public float moveTimePeriod = 0.6f;
	public bool moveUpAndDown = true;
	public bool spawnFacingScrooge = false;
	public Vector3 spawnPosistion;
	
	private bool facingRight = true;
	private float lastMoveTime;
	public float direction = 1f;

	private Transform groundCheck;
	private GameObject Scrooge;
	private EnemyHealth enemyHealth;
	private SpriteRenderer sprite;

	void Awake () {
		Scrooge = GameObject.FindGameObjectWithTag("Player");
		enemyHealth = GetComponent<EnemyHealth>();
		sprite = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		if (enemyType == EnemyType.BigPink)
			BigPinkMove();
		else if (enemyType == EnemyType.RedSquid)
			RedSquidMove();

	}

	void FixedUpdate() {
		if(rigidbody2D.velocity.x > 0 && !facingRight)
			FlipCharacter();
		else if(rigidbody2D.velocity.x < 0 && facingRight)
			FlipCharacter();
	}

	public void FlipCharacter() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	
	public void Spawn() {
		enemyHealth.dead = false;
		sprite.enabled = true;
		transform.position = spawnPosistion;
		direction = 1f;
		rigidbody2D.isKinematic = true;
		rigidbody2D.gravityScale = 0f;
		rigidbody2D.velocity = new Vector2(0f, 0f);


		// Enable colliders on Enemy
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach (Collider2D col in cols) {			
			col.isTrigger = false;
		}

		if (spawnFacingScrooge) {
			if ((Scrooge.transform.position.x - transform.position.x) < 0)
				FlipCharacter();
		}

		lastMoveTime = Time.time;
	}

	void MoveUpAndDown() {
		if (Time.time > (lastMoveTime + moveTimePeriod)) {
			direction *= -1;
			lastMoveTime = Time.time;
		}

		rigidbody2D.velocity = new Vector2(0f, direction * moveSpeed);
	}
	
	void MoveLeftAndRight() {
		if (Time.time > (lastMoveTime + moveTimePeriod)) {
			direction *= -1;
			lastMoveTime = Time.time;
		}
		rigidbody2D.velocity = new Vector2(direction * moveSpeed, 0f);
	}

	void BigPinkMove() {
		if (!enemyHealth.dead)
			MoveLeftAndRight();
	}

	void RedSquidMove() {
		if (!enemyHealth.dead) {
			if (moveUpAndDown)
				MoveUpAndDown();
			else
				MoveLeftAndRight();
		}
	}
}
