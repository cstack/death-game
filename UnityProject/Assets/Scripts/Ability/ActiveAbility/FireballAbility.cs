using UnityEngine;
using System.Collections;

public class FireballAbility : ActiveAbility {

	public EnemyShot shot_prefab;
	public float kickback = 200f;

	protected override void Awake() {
		abilityName = "Fireball";
	}

	protected override void OnActivate () {
		player.summonFireballs (shot_prefab, kickback);
	}
}
