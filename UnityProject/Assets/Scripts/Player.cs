using UnityEngine;
using System.Collections;

public class Player : CharacterBase {
	public float maxSpeed = 5f;

	private float speed;
	private enum IdleOrRunningStates {
		Idle, Running
	}

	new void Start() {
		base.Start ();
		dir = Direction.Right;
	}

	new void Update () {
		base.Update ();
		speed = Input.GetAxis ("Horizontal") * maxSpeed;
		rigidbody2D.velocity = new Vector2 (speed, 0);
		if (Mathf.Abs(speed) < 0.1f) {
			am.animate((int) IdleOrRunningStates.Idle);
		} else {
			am.animate((int) IdleOrRunningStates.Running);
		}

		if (dir == Direction.Left && speed > 0) {
			dir = Direction.Right;
		} else if (dir == Direction.Right && speed < 0) {
			dir = Direction.Left;
		}
	}
}
