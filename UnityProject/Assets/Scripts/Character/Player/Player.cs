using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : CharacterBase {
	public float maxSpeed = 5f;

	public bool grounded;
	public bool feetInWater;
	public bool headUnderwater;
	public float jumpSpeed = 5f;
	public float swimSpeed = 2f;
	public float waterDrag = 4f;
	public float waterGravity = 0.5f;

	private PlayerHealth playerHealth;
	public Ability ability; // mingrui
	public float speed;
	private enum IdleOrRunningStates {
		Idle, Running
	}

	private Animator animator;

	private AbilityControl ability_control; // mingrui

	private void Start() {
		dir = Direction.Right;
		animator = GetComponent<Animator> ();
		playerHealth = GetComponent<PlayerHealth> ();

		if (ability != null) {
			ability.character = this;
		}

		ability_control = GetComponent<AbilityControl>(); // mingrui
	}

	private void Update () {

		HorizontalMove ();
		VerticalMove ();
		AbilityDetect ();
	}

	private void HorizontalMove () {

		speed = Input.GetAxis ("Horizontal") * maxSpeed;
		updateXVelocity (speed);

		animator.SetFloat ("speed", Mathf.Abs(speed));

		if (dir == Direction.Left && speed > 0) {
			dir = Direction.Right;
		} else if (dir == Direction.Right && speed < 0) {
			dir = Direction.Left;
		}

	}

	private void VerticalMove () {

		if (Input.GetButtonDown("Jump") && (grounded || feetInWater)) {
			if (headUnderwater) {
				updateYVelocity(swimSpeed);
			} else {
				updateYVelocity(jumpSpeed);
			}
			grounded = false;
			animator.SetBool("grounded", false);
		}

		if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0) {
			updateYVelocity(0);
		}

	}

	private void AbilityDetect () {

		if (Input.GetButtonDown("Fire1") && ability != null) {
			ability.Activate();
		}

	}

	private void OnCollisionStay2D (Collision2D other) {
		if (other.gameObject.tag == "ground") {
			if (other.contacts.Length > 0 && rigidbody2D.velocity.y <= 0 &&
			    Vector2.Dot(other.contacts[0].normal, Vector2.up) > 0.5) {
				// Collision was on bottom
				grounded = true;
				animator.SetBool("grounded", true);
			}
		}
	}

	public void AddAbility (Ability newAbility){
		ability = newAbility;
		ability.character = this;
		ability_control.add_ability(newAbility); // mingrui
	}

	private void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.tag == "ground") {
			grounded = false;
			animator.SetBool("grounded", false);
		}
	}

	public void headEnterWater () {
		headUnderwater = true;
		rigidbody2D.drag = waterDrag;
		playerHealth.startDrowning ();
	}

	public void headExitWater () {
		headUnderwater = false;
		rigidbody2D.drag = 0;
		playerHealth.stopDrowning ();
	}

	public void feetEnterWater () {
		feetInWater = true;
	}

	public void feetExitWater () {
		feetInWater = false;
	}

	public void AbilityAnimationHit() {
		ability.Hit ();
	}

	public void AbilityAnimationFinished() {
		ability.Finish ();
	}
}
