using UnityEngine;
using System.Collections;

public class FireEnemy : EnemyBase {
	public float maxSpeed = 3f;
	public float range;

	public EnemyShot shot_prefab;

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
		} else if (offset > 0 && dir == Direction.Left) {
			dir = Direction.Right;
		} else if (offset < 0 && dir == Direction.Right) {
			dir = Direction.Left;
		} else {
			speed = 0;
			if (canAttack()) {
				// Trying to figure out a way to get attack animation
				// working, but it's not cooperating with me.
				animator.SetBool ("Attack", true);
				Attack();
				animator.SetBool ("Attack", false);
			}
		}

		updateXVelocity (speed);

		if (dir == Direction.Left && speed > 0) {
			dir = Direction.Right;
		} else if (dir == Direction.Right && speed < 0) {
			dir = Direction.Left;
		}
	}

	override protected void Attack() {
		base.Attack ();
		EnemyShot shot = (EnemyShot)Instantiate(shot_prefab);
		shot.init_shot(this, false);
	}

}
