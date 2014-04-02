using UnityEngine;
using System.Collections;

public class LungCapacityAbility : PassiveAbility {

	protected override void Awake() {
		abilityName = "Breath Underwater";
	}
	
	protected override void OnActivate () {
		player.canBreathUnderwater = true;
	}
}
