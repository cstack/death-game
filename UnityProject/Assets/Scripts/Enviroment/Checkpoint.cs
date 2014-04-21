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
			Player player = col.gameObject.GetComponent<Player>();
			if (!player.ghost) {
//				BoxCollider2D b = collider2D as BoxCollider2D;
				//Debug.Log("Set spawn and move to willo");
				spawnpoint.transform.position = transform.parent.position;
				spriteAnimation.SetBool ("Toggled", true);
			}
		}
	}
}
