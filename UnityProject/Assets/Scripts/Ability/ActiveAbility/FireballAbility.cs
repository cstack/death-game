using UnityEngine;
using System.Collections;

public class FireballAbility : ActiveAbility {

	public EnemyShot shot_prefab;

	protected override void Awake() {
		abilityName = "Fireball";
	}

	protected override void OnActivate () {
		player.summonFireballs (shot_prefab);
	}
}
