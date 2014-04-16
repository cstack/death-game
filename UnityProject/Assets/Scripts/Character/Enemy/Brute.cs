using UnityEngine;
using System.Collections;

public class Brute : EnemyBase {
	public float maxSpeed = 1.5f;
	public float range = 1f;
	public int attackPower = 50;
	public Ability deathAbility = null;
    
    // jump before charging
    public float jump_force;

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
	}

    private void Stop_Attack() {
        //animator.SetTrigger("Walk");
        AbilityAnimationFinished();
    }

	public void AbilityAnimationFinished() {
		attackCollider.SetActive (false);
	}
}
