using UnityEngine;
using System.Collections;

public abstract class CharacterBase : EntityBase {
	public bool canBreathUnderwater;
	protected Animator animator;

	protected virtual void Start() {
		animator = GetComponent<Animator> ();
	}
}
