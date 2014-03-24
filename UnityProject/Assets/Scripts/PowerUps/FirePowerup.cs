using UnityEngine;
using System.Collections;

public class FirePowerup : ActiveAbility {
	public EnemyShot shot_prefab;

	private Player player;

	public override void setPlayer(Player _p){
		player = _p;
	}

	public override void use(){
		EnemyShot shot = (EnemyShot)Instantiate(shot_prefab);
		shot.init_shot(player, true);
	}

	public FirePowerup () {
		abilityName = "FirePowerup";
	}
}
