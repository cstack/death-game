using UnityEngine;
using System.Collections;

public class PassiveAbility : Ability {

	protected override void Awake() {
		abilityName = "Passive";
	}
	
	protected override void Start() {
		abilityName = "Passive";
	}

	protected override void onAttachedToCharacter() {
		Activate ();
	}
}
