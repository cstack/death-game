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
		bar_right = GameObject.Find("GUI Health Bar Facing Right");
		bar_left = GameObject.Find("GUI Health Bar Facing Left");
	}
	
	private void Update() {
		if (enemyHealth.maxHP > 1f)
		{
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
		else {
			bar_right.SetActive(false);
			bar_left.SetActive(false);
		}
	}
}
