﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class Reaper : EnemyBase {
	public float maxSpeed = 2f;
	public float range = 10f;
	public float minDistance = 2f;
	public FireEnemy impPrefab;
	public GameObject birdPrefab;
	public Brute brutePrefab;
	public Lava lavaPrefab;

	public Soul soulPrefab;
	public List<GameObject> waypoints;
	public GameObject EnemyDestroyer;

	public AudioClip creepyLaughone;
	private float noiseTimerone = 5f;

	public AudioClip creepyLaughtwo;
	private float noiseTimertwo = 3f;
	
	public AudioClip boss;

	public enum State {
		Idle, Charging, Spinning, SummonImps, SummonBirds, SummonBrute,
		SummonLava
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

		PlayCreepySound (creepyLaughone);

		waypoints.Add (GameObject.Find ("Waypoint 4"));
		waypoints.Add (GameObject.Find ("Waypoint 5"));
		
		if (boss != null) {
			if (Camera.main.audio != null) {
				Camera.main.audio.Stop ();
				Camera.main.audio.clip = boss;
				Camera.main.audio.loop = true;
				Camera.main.audio.Play ();
			}
		}
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

		PlayNoises ();

	}

	private IEnumerator EnterState(State newState) {
		switch (newState) {
		case State.Idle:
		case State.Spinning:
			rigidbody2D.isKinematic = false;
			break;
		default:
			rigidbody2D.isKinematic = true;
			break;
		}

		switch (newState) {
		case State.Spinning:
			attackCollider.SetActive(true);
			yield return new WaitForSeconds(3f);
			state = State.Idle;
			break;
		case State.SummonImps:
			for (int i = 0; i < 3; i++) {
				SummonImp();
				yield return new WaitForSeconds(1f);
			}
			state = State.Idle;
			break;
		case State.SummonBirds:
			for (int i = 0; i < 2; i++) {
				SummonBird();
				yield return new WaitForSeconds(1f);
			}
			state = State.Idle;
			break;
		case State.SummonBrute:
			for (int i = 0; i < 1; i++) {
				SummonBrute();
				yield return new WaitForSeconds(1f);
			}
			state = State.Idle;
			break;
		case State.SummonLava:
			for (int i = -1; i <= 5; i++) {
				float x = i * 3 * (dir == Direction.Right ? 1 : -1);
				Vector3 position = transform.position + new Vector3(x, 15f, 0);
				SummonLava(position);
				yield return new WaitForSeconds(0.5f);
			}
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
		} else if (Mathf.Abs(offset) < minDistance) {
			if (offset > 0) {
				speed = -1 * maxSpeed;
			} else {
				speed = maxSpeed;
			}
		} else {
			speed = 0;
		}
		updateXVelocity (speed);

		if (canAttack()) {
			Attack();
		}
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
		State[] attacks = {State.Spinning, State.SummonImps, State.SummonBirds, State.SummonBrute,
			State.SummonLava};
		int attackIndex = (int) (Random.value * attacks.Length);
		StartCoroutine (ChargeAndAttack(attacks[attackIndex]));
	}

	private IEnumerator ChargeAndAttack(State attackState) {
		state = State.Charging;
		yield return new WaitForSeconds(1f);
		state = attackState;
	}

	private void SummonImp() {
		FireEnemy imp = (FireEnemy) Instantiate (impPrefab);
		imp.transform.position = transform.position + new Vector3 (Random.value*10f - 5, Random.value*8f, 0);
	}

	private void SummonBird() {
		GameObject bird = (GameObject) Instantiate (birdPrefab);
		bird.transform.position = transform.position + new Vector3 (Random.value*10f - 5, 4f + Random.value*3f, 0);
	}

	private void SummonBrute() {
		Brute brute = (Brute) Instantiate (brutePrefab);
		brute.transform.position = transform.position + new Vector3 (Random.value*10f - 5, 5f + Random.value*5f, 0);
	}

	private void SummonLava(Vector3 position) {
		Lava lava = (Lava) Instantiate (lavaPrefab);
		lava.transform.position = position;
	}

	private void PlayNoises () {
		if (noiseTimerone > 0f) {
			noiseTimerone -= Time.deltaTime;
		} else {
			PlayCreepySound (creepyLaughone);
		}

		if (noiseTimertwo > 0f) {
			noiseTimertwo -= Time.deltaTime;
		} else {
			PlayCreepySound (creepyLaughtwo);
		}
	}

	private void PlayCreepySound (AudioClip theSound) {
		
		if (audio != null) {
			audio.enabled = true;
			audio.loop = false;
			if (theSound != null && !audio.isPlaying) {
				audio.clip = theSound;
				audio.Play ();
				noiseTimertwo = Random.Range (2f, 5f);
				noiseTimerone = Random.Range (2f, 5f);
			}
		}

	}

	public override void OnDie() {
		for (int i = 0; i < 50; i++) {
			Soul soul = (Soul) Instantiate(soulPrefab);
			soul.transform.position = transform.position + new Vector3(Random.value - 0.5f, Random.value * 3f, 0);
			soul.waypoints = waypoints;
			soul.explode = true;
			soul.explosionOrigin = transform.position;
		}
		
		GameObject enemyDestroyer = (GameObject) Instantiate (EnemyDestroyer);
		enemyDestroyer.transform.position = transform.position;
		
		Destroy (GameObject.Find ("BossGate"));
	}
}
