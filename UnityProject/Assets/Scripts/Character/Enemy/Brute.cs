using UnityEngine;
using System.Collections;

public class Brute : EnemyBase {
	public float maxSpeed = 1.5f;
	public float range = 1f;
	public int attackPower = 50;
	public Ability deathAbility = null;
    
    // jump before charging
    public float jump_force;
    public float charge_force;
    private bool do_charge = false;
    private float charge_wait_duration = 2f;
    private float charge_wait_timer = 0f;
    private bool charge_finished = false;

	private GameObject attackCollider;

	override protected void Start() {
		base.Start ();
		attackCollider = transform.FindChild ("Attack").gameObject;
	}

	/*
	void Update () {
		float offset = distanceToPlayer ();
		float speed = 0;
		if (Mathf.Abs(offset) > range) {
			if (offset > 0) {
				speed = maxSpeed;
			} else {
				speed = -1 * maxSpeed;
			}
		} else if (offset > 0 && dir == Direction.Left) {
			dir = Direction.Right;
		} else if (offset < 0 && dir == Direction.Right) {
			dir = Direction.Left;
		} else {
			speed = 0;
			if (canAttack()) {
				Attack();
			}
		}
		
		updateXVelocity (speed);
		animator.SetFloat ("speed", Mathf.Abs(speed));
		
		if (dir == Direction.Left && speed > 0) {
			dir = Direction.Right;
		} else if (dir == Direction.Right && speed < 0) {
			dir = Direction.Left;
		}
	}
    */

    // moves slowly towards player if outside of charging range.
    // in charging range, stop, jump up and down a few times.
    // charge.
    void Update() {
        if(do_charge){
            if(canAttack()){
                do_charge = false;
                charge_finished = false;
                return;
            }
            Bull_Charge();
            return;
        }

        float offset = distanceToPlayer();
        float speed = 0;
        if (Mathf.Abs(offset) > range)
        {
            Stop_Attack();
            if (offset > 0)
            {
                speed = maxSpeed;
            }
            else
            {
                speed = -1 * maxSpeed;
            }
        }
        else if (offset > 0 && dir == Direction.Left)
        {
            dir = Direction.Right;
        }
        else if (offset < 0 && dir == Direction.Right)
        {
            dir = Direction.Left;
        }
        else
        {
            speed = 0;
            if (canAttack())
            {
                Attack();
            }
        }

        updateXVelocity(speed);
        animator.SetFloat("speed", Mathf.Abs(speed));

        if (dir == Direction.Left && speed > 0)
        {
            dir = Direction.Right;
        }
        else if (dir == Direction.Right && speed < 0)
        {
            dir = Direction.Left;
        }
    }

	
	override protected void Attack() {
		base.Attack ();
        // add a little jump to notify the player of charging
        rigidbody2D.AddForce(new Vector2(0, jump_force));
		//animator.SetTrigger ("Attack");
		attackCollider.SetActive (true);

        // get ready to charge
        do_charge = true;
	}

    private void Bull_Charge() { 
        // bull charge is a different state
        // right after attacking
        if(!charge_finished){
            // start wait timer, to wait for jump to finished
            // and give player time to react
            charge_wait_timer += Time.deltaTime;
            if(charge_wait_timer > charge_wait_duration){
                charge_wait_timer = 0;
                // start charge
                float c_force = charge_force;
                if (dir == Direction.Left)
                {
                    c_force = -charge_force;
                }
                rigidbody2D.AddForce(new Vector2(c_force, 0));
                charge_finished = true;
            }
        }
    }

    private void Stop_Attack() {
        //animator.SetTrigger("Walk");
        AbilityAnimationFinished();
    }

	public void AbilityAnimationFinished() {
		attackCollider.SetActive (false);
	}
}
