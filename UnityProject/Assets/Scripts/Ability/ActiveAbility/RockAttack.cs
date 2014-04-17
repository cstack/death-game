using UnityEngine;
using System.Collections;

public class RockAttack : ActiveAbility {
	
	protected override void OnActivate () {
		player.startRockAttack ();
	}
}
