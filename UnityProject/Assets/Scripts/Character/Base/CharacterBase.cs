using UnityEngine;
using System.Collections;

public abstract class CharacterBase : EntityBase {
	public bool canBreathUnderwater;
	protected Animator animator;
	public Backpack backpack; // mingrui, for holding javelin count

	protected virtual void Start() {
		animator = GetComponent<Animator> ();
	}
}
