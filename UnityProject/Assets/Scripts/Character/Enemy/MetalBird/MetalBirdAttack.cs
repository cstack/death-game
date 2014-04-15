using UnityEngine;
using System.Collections;

public class MetalBirdAttack : EnemyBase {

	private MetalBirdMovement movement;

    private DaggerThrower[] throwers;

	public new void Start() {
		base.Start ();
		movement = GetComponent<MetalBirdMovement> ();
        throwers = GetComponentsInChildren<DaggerThrower>();
	}

	// Update is called once per frame
	void Update () {
	    if (movement.is_engage() && canAttack()) {
			Attack ();
		}
	}

	protected override void Attack() {
		base.Attack ();
        
        // ask thrower component to attack
        foreach (DaggerThrower dt in throwers) {
            dt.Throw_Projectile();
        }
	}
}
