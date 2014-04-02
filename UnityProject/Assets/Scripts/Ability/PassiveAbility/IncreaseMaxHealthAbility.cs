using UnityEngine;
using System.Collections;

public class IncreaseMaxHealthAbility : PassiveAbility {

	public int amount = 30;

	protected override void Awake() {
		abilityName = "Increase Max Health";
	}
	
	protected override void OnActivate () {
		player.playerHealth.increaseMaxHealth(amount);
	}
}
