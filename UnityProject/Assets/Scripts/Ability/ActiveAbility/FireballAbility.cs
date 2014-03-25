using UnityEngine;
using System.Collections;

public class FireballAbility : ActiveAbility {
	
	public Texture2D icon;
	public EnemyShot shot_prefab;

	protected override void Awake() {
		abilityName = "Fireball";
		abilityIcon = icon;
	}

	protected override void OnActivate () {
		EnemyShot shot = (EnemyShot)Instantiate(shot_prefab);
		shot.init_shot(character, true);
	}
}
