using UnityEngine;
using System.Collections;

public class BasicAttack : ActiveAbility {
	public GameObject attackColliderPrefab;

	protected override void Awake() {
		abilityName = "Melee";
	}
	
	protected override IEnumerator OnActivate () {
		GameObject collider = Instantiate (attackColliderPrefab) as GameObject;
		collider.transform.position = character.transform.position;
		if (character.dir == EntityBase.Direction.Left) {
			collider.transform.position += new Vector3(-1f, 0f, 0f);
		} else {
			collider.transform.position += new Vector3(1f, 0f, 0f);
		}
		character.GetComponent<Animator> ().SetInteger ("ability", (int) GlobalConstant.AbilityAnimation.MeleeAttack);
	}
}
