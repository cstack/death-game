using UnityEngine;
using System.Collections;

public class MetalBirdAttack : EnemyBase {
	public GameObject javelin;

	private MetalBirdMovement movement;

	public new void Start() {
		base.Start ();
		movement = GetComponent<MetalBirdMovement> ();
	}

	// Update is called once per frame
	void Update () {
	    if (movement.is_engage() && canAttack()) {
			Attack ();
		}
	}

	protected override void Attack() {
		base.Attack ();

		JavelinControl new_javelin = ((GameObject) Instantiate (javelin,
		                                                 transform.position + new Vector3 (1, 2f, 0),
		                                                 transform.rotation)).GetComponent<JavelinControl>();
		new_javelin.speed = 8;
		new_javelin.friendly = false;
		int aim;
		if (movement.dir == Direction.Left) {
			aim = (int) GlobalConstant.direction.left;
		} else {
			aim = (int) GlobalConstant.direction.right;
		}
		//Debug.Log(aim);
		new_javelin.Create_Javelin(gameObject, aim);
	}
}
