using UnityEngine;
using System.Collections;

public class HealthBarControl : MonoBehaviour {
	private GameObject bar_right;
	private GameObject bar_left; 
	private EnemyHealth enemyHealth;
	private EnemyBase enemy;

	private void Awake() {
		enemyHealth = transform.parent.GetComponent<EnemyHealth> ();
		enemy = transform.parent.GetComponent<EnemyBase> ();
		bar_right = transform.FindChild("GUI Health Bar Facing Right").gameObject;
		bar_left = transform.FindChild("GUI Health Bar Facing Left").gameObject;
		Vector3 scale = transform.localScale;
		scale.x = enemyHealth.maxHP / 3f;
		transform.localScale = scale;
	}
	
	private void Update() {
		if (enemy.dir == EntityBase.Direction.Left)
		{
			bar_right.SetActive(false);
			bar_left.SetActive(true);
		}
		else {
			bar_right.SetActive(true);
			bar_left.SetActive(false);
		}
	}
}
