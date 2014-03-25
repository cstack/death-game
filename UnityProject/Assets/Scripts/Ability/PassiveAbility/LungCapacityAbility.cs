using UnityEngine;
using System.Collections;

public class LungCapacityAbility : PassiveAbility {

	public Texture2D icon;

	protected override void Awake() {
		abilityName = "Lung Capacity";
		abilityIcon = icon;
	}
	
	protected override void OnActivate () {
	
	}
}
