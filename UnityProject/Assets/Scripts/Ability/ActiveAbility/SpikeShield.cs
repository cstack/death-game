using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class SpikeShield : ActiveAbility {
	public float duration = 1f;
	
	protected override void OnActivate () {
		player.summonSpikes (duration);
	}
}
