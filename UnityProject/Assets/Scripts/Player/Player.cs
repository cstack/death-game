using UnityEngine;
using System.Collections;

public class Player : EntityBase {
	public float maxSpeed = 5f;

	public bool grounded;
	public float jumpSpeed = 5f;

	private float playerHealth;
	public ActiveAbility ability;
	private float speed;
	private enum IdleOrRunningStates {
		Idle, Running
	}

	private Animator animator;

	private void Start() {
		dir = Direction.Right;
		animator = GetComponent<Animator> ();
	}

	private void Update () {
		speed = Input.GetAxis ("Horizontal") * maxSpeed;
		updateXVelocity (speed);

		animator.SetFloat ("speed", Mathf.Abs(speed));

		if (dir == Direction.Left && speed > 0) {
			dir = Direction.Right;
		} else if (dir == Direction.Right && speed < 0) {
			dir = Direction.Left;
		}

		if (Input.GetButtonDown("Jump") && grounded) {
			updateYVelocity(jumpSpeed);
			grounded = false;
			animator.SetBool("grounded", false);
		}

		if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0) {
			updateYVelocity(0);
		}

		if (Input.GetButtonDown("Fire1") && ability != null) {
			ability.use();
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "ground") {
			if (other.contacts.Length > 0 && rigidbody2D.velocity.y <= 0 &&
			    Vector2.Dot(other.contacts[0].normal, Vector2.up) > 0.5) {
				// Collision was on bottom
				grounded = true;
				animator.SetBool("grounded", true);
			}
		}
	}

	public void TakeDamage (float dmg) {

	}
}
