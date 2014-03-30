using UnityEngine;
using System.Collections;

public abstract class CharacterBase : EntityBase {
	protected Animator animator;
	public Ability currentAbility;

	protected virtual void Start() {
		animator = GetComponent<Animator> ();
	}
}
