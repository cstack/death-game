using UnityEngine;
using System.Collections;

public class DropletSpawner : MonoBehaviour {
	public Lava dropletPrefab;

	// Use this for initialization
	private IEnumerator Start () {
		while (true) {
			Drip ();
			yield return new WaitForSeconds(Random.value + 1);
		}
	}
	
	public void Drip() {
		Lava drop = (Lava) Instantiate (dropletPrefab);
		drop.transform.position = transform.position;
		drop.transform.parent = transform;
	}
}
