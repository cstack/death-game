using UnityEngine;
using System.Collections;

public abstract class EnemyBase : CharacterBase {
	public float cooldownTime;

	protected Player player;

	private float timeOfLastAttack;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
		player = (GameObject.Find ("Player")).GetComponent<Player>();
		timeOfLastAttack = Time.time;
	}

	protected float distanceToPlayer() {
		return player.transform.position.x - transform.position.x;
	}

	protected bool canAttack() {
		return ((Time.time - timeOfLastAttack) > cooldownTime);
	}

	protected virtual void Attack() {
		timeOfLastAttack = Time.time;
	}
}
