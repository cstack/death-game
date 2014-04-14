using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public GameObject spawnpoint;
	public Animator spriteAnimation;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.name == "Player")
		{
			spawnpoint.transform.position = transform.position + new Vector3 (2f, 2f, 0f);
			spriteAnimation.SetBool ("Toggled", true);
		}
	}
}
