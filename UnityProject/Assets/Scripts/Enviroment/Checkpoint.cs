using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public GameObject spawnpoint;
	public Animator spriteAnimation;

	void Start () {
		if (spawnpoint == null) {
			spawnpoint = GameObject.Find("SpawnPoint");
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player")
		{
			if (!col.gameObject.GetComponent<Player> ().ghost) {
//				BoxCollider2D b = collider2D as BoxCollider2D;
				spawnpoint.transform.position = transform.position + new Vector3 (2f, 2f, 0f);
				spriteAnimation.SetBool ("Toggled", true);
			}
		}
	}
}
