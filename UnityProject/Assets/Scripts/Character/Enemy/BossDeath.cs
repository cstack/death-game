using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class BossDeath : MonoBehaviour {
	public Soul soulPrefab;
	public List<GameObject> waypoints;
	public GameObject EnemyDestroyer;

	void Start() {
		waypoints.Add (GameObject.Find ("Waypoint 4"));
		waypoints.Add (GameObject.Find ("Waypoint 5"));
	}

	void OnDestroy() {
		for (int i = 0; i < 50; i++) {
			Soul soul = (Soul) Instantiate(soulPrefab);
			soul.transform.position = transform.position + new Vector3(Random.value - 0.5f, Random.value * 3f, 0);
			soul.waypoints = waypoints;
			soul.explode = true;
			soul.explosionOrigin = transform.position;
		}

		GameObject enemyDestroyer = (GameObject) Instantiate (EnemyDestroyer);
		enemyDestroyer.transform.position = transform.position;

		Destroy (GameObject.Find ("BossGate"));
	}
}
