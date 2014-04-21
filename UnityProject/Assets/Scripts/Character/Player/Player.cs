using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class Player : CharacterBase {
	public float maxSpeed = 5f;

	public bool grounded;
	public bool feetInWater;
	public bool headUnderwater;
	public bool crouching = false;
	public bool animating = false;
	public float jumpSpeed = 20f;
	public float swimSpeed = 7f;
	public float waterDrag = 4f;
	public float waterGravity = 0.5f;
	public float jumptimer = 0.1f;
	public float dashSpeed = 15f;
	public float rollSpeed = 12f;
	public float dashDuration = 0.5f;
	public float rollDuration = 2f;
	public float ghostTravelTime = 4f;
	public float slamSpeed = 1f;
	public float timejump = 0f;
	public float slamWaitTime = 0.3f;
	public PlayerHealth playerHealth;
	public float speed;
	private enum IdleOrRunningStates {
		Idle, Running
	}

	public bool dashing;
	public bool isRock;
	private GameObject spikeShield;
	private DaggerThrower[] throwers;
	private float baseGravity;
	private AbilityControl ability_control; // mingrui, for array of ability
	private int aim; // mingrui, for aiming javelin
	private BoxCollider2D b;
	private BoxCollider2D c;
	public bool ghost;
	private float ghostSpeed;
	public GameObject javelin; // mingrui, javelin object

	public WillOWispControl willoControl;
	
	void Awake() {
		if (DataLogging.enabled) {
			DataLogging.gameSession = new ParseObject("GameSession");
		}
		willoControl = GameObject.Find("WillOWisp").GetComponent<WillOWispControl>();
	}

	override protected void Start() {
		base.Start ();
		dir = Direction.Right;
		playerHealth = GetComponent<PlayerHealth> ();

		ability_control = GetComponent<AbilityControl>(); // mingrui
		backpack = GetComponent<Backpack>(); // mingrui
		baseGravity = rigidbody2D.gravityScale;
		spikeShield = transform.FindChild ("SpikeShield").gameObject;

		spikeShield.SetActive (true);
		throwers = GetComponentsInChildren<DaggerThrower> ();
		spikeShield.SetActive (false);

		b = collider2D as BoxCollider2D;
		c = transform.FindChild("Head").GetComponent<BoxCollider2D> ();
	}

	private void Update () {
		if (ghost) {
			RespawnMove();
			return;
		}

		Get_Aim(); // mingrui

		if (!ability_control.animating) {
			VerticalMove ();
		}

		if (!crouching) {
			HorizontalMove ();
		}

		if (timejump <= 0f) {
			SlamDown ();
		}

		Crouch ();
		UpdateTimers ();
	}

	private void UpdateTimers() {
		if (timejump > 0f) {
			timejump -= Time.deltaTime;
		}
	}

	public void becomeGhost() {
		ghost = true;
		animator.SetBool ("ghost", true);
		transform.FindChild ("Head").GetComponent<BoxCollider2D> ().isTrigger = true;
		GetComponent<BoxCollider2D> ().isTrigger = true;
		playerHealth.invulnerable = true;
		rigidbody2D.gravityScale = 0f;

		// Calculate spped needed to reach last checkpoint
		Vector3 target = playerHealth.spawnPoint.transform.position;
		Vector3 displacement = target - transform.position;
		ghostSpeed = displacement.magnitude / ghostTravelTime;
	}

	public void RespawnMove() {
		Vector3 target = playerHealth.spawnPoint.transform.position;
		Vector3 displacement = target - transform.position;
		if (displacement.magnitude < 1f) {
			Respawn ();
			return;
		}
		Vector3 direction = displacement / displacement.magnitude;
		rigidbody2D.velocity = direction * ghostSpeed;
	}

	public void Respawn() {
		ghost = false;
		GetComponent<BoxCollider2D> ().isTrigger = false;
		transform.FindChild ("Head").GetComponent<BoxCollider2D> ().isTrigger = false;
		playerHealth.invulnerable = false;
		rigidbody2D.gravityScale = baseGravity;
		animator.SetBool ("ghost", false);
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

		if (isRock) {
			updateXVelocity(0f);
			return;
		}

		if (ability_control.animating) {
			if (Mathf.Abs(speed) > 0.01f) {
				//Gradually reduce the player's y velocity - change 10f to ability specific slow down constant
				float spdbefore = speed;
				speed -= (speed/Mathf.Abs(speed)) * Time.deltaTime * 10f;

				//If it passed 0
				if ((speed/Mathf.Abs(speed)) != (spdbefore/Mathf.Abs(spdbefore))) {
					speed = 0f;
				}
			}
		} else {
			speed = Input.GetAxis ("Horizontal") * maxSpeed;
		}

		updateXVelocity (speed);

		animator.SetFloat ("speed", Mathf.Abs(speed));

		if (dir == Direction.Left && speed > 0) {
			dir = Direction.Right;
		} else if (dir == Direction.Right && speed < 0) {
			dir = Direction.Left;
		}

	}

	private void VerticalMove () {

		if (isRock) {
			return;
		}

		if (Input.GetButtonDown("Jump") && (grounded || feetInWater)) {
			if (feetInWater) {
				updateYVelocity(swimSpeed * 2);
			} else { 
				animator.SetTrigger("jump");
				
				//Let animation finish first!
				Invoke("Jump", jumptimer);
			}
		}

//		if (!Input.GetButton("Jump") && rigidbody2D.velocity.y > 0 && !feetInWater) {
//			animator.SetTrigger("exitjump");
//			updateYVelocity (0);
//		}


	}

	private void SlamDown () {

		if (Input.GetButton("Downward") && !grounded) {
			updateYVelocity(rigidbody2D.velocity.y - slamSpeed);
		}
	}

	private void Crouch () {
		
		//Crouch
		
		if (Input.GetButton("Downward") && grounded && !feetInWater && !Input.GetButton("Right") && !Input.GetButton("Left")) {
			crouching = true;
			animator.SetBool("crouching", crouching);
			animator.SetFloat("speed", 0f);
			updateXVelocity (0f);
			b.size = new Vector2 (1.1f, 1.45f);
			b.center = new Vector2 (0.1f, 0.75f);
			c.enabled = false;
		}

		if (crouching) {
			if (!Input.GetButton("Downward") || !grounded) {
				crouching = false;
				animator.SetBool("crouching", crouching);
				b.size = new Vector2 (1.1f, 2.37f);
				b.center = new Vector2 (0.1f, 1.185f);
				c.enabled = true;
			}
		}
	}

	private void Jump() {
//		if (!Input.GetButton("Jump")) {
//			return;
//		}

		updateYVelocity(jumpSpeed);
		grounded = false;
		animator.SetBool("grounded", grounded);
		timejump = slamWaitTime;
	}

	private void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			if (dashing ||
			    (isRock && rigidbody2D.velocity.y < 0)) {
				other.gameObject.GetComponent<EnemyHealth>().TakeDamage(1f);
			}
		}
	}

	private void OnCollisionStay2D (Collision2D other) {
		if (other.gameObject.tag == "ground" || other.gameObject.tag == "Enemy") {
			if (other.contacts.Length > 0 && rigidbody2D.velocity.y <= 4 &&
			    Vector2.Dot(other.contacts[0].normal, Vector2.up) > 0.5) {
				// Collision was on bottom
				grounded = true;
				animator.SetBool("grounded", true);
			}
		}
	}

	public bool AddAbility (Ability newAbility){
		// return true if ability was added
		if (newAbility == null) {
			return false;
		}

		return ability_control.add_ability(newAbility); // mingrui
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

	public void summonSpikes(float duration) {
		StartCoroutine (createSpikes(duration));
	}

	private IEnumerator createSpikes(float duration) {
		spikeShield.SetActive (true);
		yield return new WaitForSeconds(duration);
		foreach (DaggerThrower dt in throwers) {
			dt.Throw_Projectile();
		}
		spikeShield.SetActive (false);
	}

    public int getFacingDir(){
        return (dir == Direction.Left ? -1 : 1);
    }

	public void summonFireballs(EnemyShot fireball, float kickback) {
		StartCoroutine (createFireballs(fireball, kickback));
	}

	private IEnumerator createFireballs(EnemyShot fireball, float kickbackForce) {
		for (int i = 0; i < 3; i++) {
			EnemyShot shot = (EnemyShot)Instantiate(fireball);
			shot.init_shot(this, true);
			Vector3 kickback = shot.rigidbody2D.velocity * kickbackForce * -1;
			rigidbody2D.AddForce(kickback);
			yield return new WaitForSeconds(0.1f);
		}

		AbilityAnimationFinished();
	}

	public void startRockAttack() {
		if (isRock) {
			return;
		}
		
		animator.SetInteger ("ability", (int) GlobalConstant.AbilityAnimation.RockAttack);
		animator.SetTrigger ("attack");
		
		isRock = true;
		rigidbody2D.gravityScale = baseGravity * 3f;
		playerHealth.invulnerable = true;
		StartCoroutine (rockAttack ());
	}
	
	public void stopRockAttack() {
		StopCoroutine ("rockAttack");
		onRockAttackFinished ();
	}

	private IEnumerator rockAttack() {
		yield return new WaitForSeconds (2f);
		onRockAttackFinished ();
	}
	
	private void onRockAttackFinished() {
		animator.SetInteger ("ability", 0);
		isRock = false;
		rigidbody2D.gravityScale = baseGravity;
		playerHealth.invulnerable = false;
	}
}
