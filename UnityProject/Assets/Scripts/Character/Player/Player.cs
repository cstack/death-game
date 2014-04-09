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
	public float dashSpeed = 15f;
	public float dashDuration = 0.5f;

	public PlayerHealth playerHealth;
	public float speed;
	private enum IdleOrRunningStates {
		Idle, Running
	}

	public bool dashing;
	private float baseGravity;
	private AbilityControl ability_control; // mingrui, for array of ability
	private int aim; // mingrui, for aiming javelin
	public GameObject javelin; // mingrui, javelin object

	override protected void Start() {
		base.Start ();
		dir = Direction.Right;
		playerHealth = GetComponent<PlayerHealth> ();

		ability_control = GetComponent<AbilityControl>(); // mingrui
		backpack = GetComponent<Backpack>(); // mingrui
		baseGravity = rigidbody2D.gravityScale;
	}

	private void Update () {
		Get_Aim(); // mingrui

		HorizontalMove ();
		VerticalMove ();

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
		if (dashing) {
			return;
		}

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
				Invoke("Jump", 0.1f);
			}
		}

		if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0 && !feetInWater) {
			updateYVelocity(0);
		}

		if (Input.GetAxis("Vertical") < 0f && grounded && !feetInWater) {

		}

	}

	private void Jump() {

		updateYVelocity(jumpSpeed);
		grounded = false;
	}

	private void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			if (dashing) {
				Destroy(other.gameObject);
			}
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
		if (ability_control.current_ability != null) {
			ability_control.current_ability.Finish ();
			ability_control.current_ability = null;
		}
	}

	public void startDash() {
		if (dashing) {
			return;
		}

		animator.SetInteger ("ability", (int) GlobalConstant.AbilityAnimation.DashAttack);
		animator.SetTrigger ("attack");

		dashing = true;
		rigidbody2D.gravityScale = 0f;
		playerHealth.invulnerable = true;
		updateYVelocity (0f);
		StartCoroutine (dash ());
	}

	public void stopDash() {
		StopCoroutine ("dash");
		onDashFinished ();
	}

	private void onDashFinished() {
		dashing = false;
		rigidbody2D.gravityScale = baseGravity;
		playerHealth.invulnerable = false;
	}

	private IEnumerator dash() {
		float startTime = Time.time;
		while (true) {
			float portionElapsed = (Time.time - startTime) / dashDuration;
			if (portionElapsed >= 1) {
				break;
			}
			updateXVelocity (dashSpeed * (dir == Direction.Left ? -1 : 1));
			yield return null;
		}
		onDashFinished ();
	}
}
