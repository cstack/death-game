using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : CharacterBase {
	public float maxSpeed = 5f;

	public bool grounded;
	public bool feetInWater;
	public bool headUnderwater;
	public bool crouching = false;
	public bool animating = false;
	public float jumpSpeed = 5f;
	public float swimSpeed = 2f;
	public float waterDrag = 4f;
	public float waterGravity = 0.5f;
	public float jumptimer = 0.1f;

	public PlayerHealth playerHealth;
	public float speed;
	private enum IdleOrRunningStates {
		Idle, Running
	}

	private AbilityControl ability_control; // mingrui, for array of ability
	private int aim; // mingrui, for aiming javelin
	public GameObject javelin; // mingrui, javelin object

	override protected void Start() {
		base.Start ();
		dir = Direction.Right;
		playerHealth = GetComponent<PlayerHealth> ();

		ability_control = GetComponent<AbilityControl>(); // mingrui
		backpack = GetComponent<Backpack>(); // mingrui
	}

	private void Update () {
		Get_Aim(); // mingrui

		if (!crouching) {
			HorizontalMove ();
		}

		if (!ability_control.animating) {
			VerticalMove ();
		}

	}

	//mingrui
	// get arrow key direction
	private void Get_Aim() {
		if(Input.GetKey(GlobalConstant.keycode_up)){
			aim = (int)GlobalConstant.direction.up;
		}
		else if(Input.GetKey(GlobalConstant.keycode_down)){
			aim = (int)GlobalConstant.direction.down;
		}
		else if(Input.GetKey(GlobalConstant.keycode_left)){
			aim = (int)GlobalConstant.direction.left;
		}
		else if (Input.GetKey(GlobalConstant.keycode_right))
		{
			aim = (int)GlobalConstant.direction.right;
		}
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
			if (feetInWater) {
				updateYVelocity(swimSpeed);
			} else { 
				animator.SetTrigger("jump");
				animator.SetBool("grounded", false);
				
				//Let animation finish first!
				Invoke("Jump", jumptimer);
			}
		}

		if (!Input.GetButton("Jump") && rigidbody2D.velocity.y > 0 && !feetInWater) {
			animator.SetTrigger("exitjump");
			updateYVelocity(0);
		}

		if (Input.GetButton("Downward") && grounded && !feetInWater) {
			crouching = true;
			animator.SetBool("crouching", crouching);
		}

		if (!Input.GetButton("Downward")) {
			crouching = false;
			animator.SetBool("crouching", crouching);
		}

	}

	private void Jump() {
		if (!Input.GetButton("Jump")) {
			return;
		}

		updateYVelocity(jumpSpeed);
		grounded = false;
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
		if (newAbility == null) {
			return;
		}

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
	}

	public void headExitWater () {
		headUnderwater = false;
		rigidbody2D.drag = 0;
		playerHealth.resetBreath ();
	}

	public void feetEnterWater () {
		feetInWater = true;
	}

	public void feetExitWater () {
		feetInWater = false;
	}

	public void AbilityAnimationHit() {

		if (ability_control.current_ability != null) {
			ability_control.current_ability.Hit ();
		}
	}

	public void AbilityAnimationFinished() {
		ability_control.animating = false;

		if (ability_control.current_ability != null) {
			ability_control.current_ability.Finish ();
			ability_control.current_ability = null;
		}
	}
}
