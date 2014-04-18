using UnityEngine;
using System.Collections;

public class Reaper : EnemyBase {
	public float maxSpeed = 2f;
	public float range = 10f;
	
	public enum State {
		Moving
	};
	public State state;

	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.Moving:
			MoveTowardPlayer();
			break;
		}

	}

	private void MoveTowardPlayer() {
		float offset = distanceToPlayer ();
		
		if (offset > 0 && dir == Direction.Left) {
			dir = Direction.Right;
		} else if (offset < 0 && dir == Direction.Right) {
			dir = Direction.Left;
		}

		float speed = 0;
		if (Mathf.Abs(offset) > range) {
			if (offset > 0) {
				speed = maxSpeed;
			} else {
				speed = -1 * maxSpeed;
			}
		} else {
			speed = 0;
			if (canAttack()) {
				PerformAttack();
			}
		}
		updateXVelocity (speed);
	}

	private void PerformAttack() {
		Debug.Log("ATTACK!");
	}
}
