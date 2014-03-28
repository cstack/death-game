using UnityEngine;
using System.Collections;

public class BasicAttack : ActiveAbility {
	private GameObject attackCollider;

	protected override void Awake() {
		abilityName = "Melee";
	}
	
	protected override void OnActivate () {
		am.SetInteger ("ability", (int) GlobalConstant.AbilityAnimation.MeleeAttack);
		am.SetTrigger ("attack");
	}

	protected override void OnHit() {
		attackCollider = (GameObject) character.transform.FindChild ("Attack").gameObject;
		attackCollider.SetActive (true);
	}

	protected override void OnFinish() {
		attackCollider.SetActive (false);
	}
}
