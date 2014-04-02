using UnityEngine;
using System.Collections;

public class FireballAbility : ActiveAbility {

	public EnemyShot shot_prefab;

	protected override void Awake() {
		abilityName = "Fireball";
	}

	protected override void OnActivate () {
		EnemyShot shot = (EnemyShot)Instantiate(shot_prefab);
		shot.init_shot(player, true);

		player.AbilityAnimationFinished();
	}
}
