using UnityEngine;
using System.Collections;

public class Reaper : EnemyBase {
	public float maxSpeed = 2f;
	public float range = 10f;
	
	public enum State {
		Idle, Charging, Spinning
	};
	private State _state;
	public State state {
		get {
			return _state;
		}
		set {
			if (value == state) {
				return;
			}
			StartCoroutine(ExitState(state));
			_state = value;
			animator.SetInteger("state", (int) value);
			StartCoroutine(EnterState(value));
		}
	}

	private GameObject attackCollider;

	override protected void Start () {
		base.Start ();
		attackCollider = transform.FindChild ("Attack").gameObject;
	}

	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.Idle:
			MoveTowardPlayer();
			break;
		case State.Spinning:
			MoveInStraightLine();
			break;
		}

	}

	private IEnumerator EnterState(State newState) {
		switch (newState) {
		case State.Spinning:
			attackCollider.SetActive(true);
			yield return new WaitForSeconds(3f);
			state = State.Idle;
			break;
		}
		yield return null;
	}

	private IEnumerator ExitState(State oldState) {
		switch (oldState) {
		case State.Spinning:
			attackCollider.SetActive(false);
			break;
		}
		yield return null;
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
				Attack();
			}
		}
		updateXVelocity (speed);
	}

	private void MoveInStraightLine() {
		float speed = maxSpeed;
		if (dir == Direction.Left) {
			speed *= -1;
		}
		updateXVelocity (speed);
	}

	protected override void Attack() {
		base.Attack ();
		State[] attacks = {State.Spinning};
		int attackIndex = (int) Random.value * attacks.Length;
		StartCoroutine (ChargeAndAttack(attacks[attackIndex]));
	}

	private IEnumerator ChargeAndAttack(State attackState) {
		state = State.Charging;
		yield return new WaitForSeconds(1f);
		state = attackState;
	}
}
