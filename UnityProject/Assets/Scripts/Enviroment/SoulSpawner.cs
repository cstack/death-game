using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class SoulSpawner : MonoBehaviour {
	public Soul soulPrefab;
	public List<GameObject> waypoints;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnSouls ());
	}
	
	private IEnumerator SpawnSouls() {
		while (true) {
			Soul soul = (Soul) Instantiate(soulPrefab);
			soul.waypoints = waypoints;
			soul.transform.position = transform.position;
			yield return new WaitForSeconds(Random.value*3f);
		}
	}
}
