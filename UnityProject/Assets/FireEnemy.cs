using UnityEngine;
using System.Collections;

public class FireEnemy : CharacterBase {
	public float maxSpeed = 3f;
	public float range = 4f;
	public float cooldownTime = 2f;

	private Player player;
	private float timeOfLastAttack;
	// Use this for initialization
	void Start () {
		player = (GameObject.Find ("Player")).GetComponent<Player>();
		timeOfLastAttack = Time.time;
	}

	private float distanceToPlayer() {
		return player.transform.position.x - transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		float offset = distanceToPlayer ();
		float speed = 0;
		if (Mathf.Abs(offset) > range) {
			if (offset > 0) {
				speed = maxSpeed;
			} else {
				speed = -1 * maxSpeed;
			}
		} else {
			speed = 0;
			float timeSinceLastAttack = Time.time - timeOfLastAttack;
			if (timeSinceLastAttack > cooldownTime) {
				Attack();
				timeOfLastAttack = Time.time;
			}
		}

		updateXVelocity (speed);

		if (dir == Direction.Left && speed > 0) {
			dir = Direction.Right;
		} else if (dir == Direction.Right && speed < 0) {
			dir = Direction.Left;
		}
	}

	private void Attack() {
		Debug.Log ("Attack");
	}
}
