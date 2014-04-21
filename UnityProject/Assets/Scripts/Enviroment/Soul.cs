using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class Soul : MonoBehaviour {
	public List<GameObject> waypoints;
	public float speed = 3f;

	private int waypointIndex;

	// Use this for initialization
	void Start () {
		StartCoroutine (Wander ());
	}

	private IEnumerator Wander() {
		while (waypointIndex < waypoints.Count) {
			Vector3 offset = waypoints[waypointIndex].transform.position - transform.position;

			if (offset.magnitude <= 5f) {
				waypointIndex++;
			}

			Vector3 vel = offset.normalized * speed + new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0);
			rigidbody2D.velocity = vel;
			yield return new WaitForSeconds(Random.value * 5f);
		}

		Destroy(gameObject);
	}
}
