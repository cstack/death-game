using UnityEngine;
using System.Collections;

public class DashAttack : ActiveAbility {

	protected override void Awake() {
		abilityName = "Dash Attack";
	}
	
	protected override void OnActivate () {
		player.startDash ();
	}

}
