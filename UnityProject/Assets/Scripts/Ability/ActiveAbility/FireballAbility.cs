using UnityEngine;
using System.Collections;

public class FireballAbility : ActiveAbility {

	public EnemyShot shot_prefab;

	protected override void Awake() {
		abilityName = "Fireball";
	}

	protected override void OnActivate () {
		EnemyShot shot = (EnemyShot)Instantiate(shot_prefab);
		shot.init_shot(character, true);

		//Change this to be called after the animation finishes, if implemented
		character.gameObject.GetComponent<Player> ().AbilityAnimationFinished ();
	}
}
