using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public GameObject spawnpoint;
	public Animator spriteAnimation;

	public AudioClip bossapproaching; 

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
				spawnpoint.transform.position = transform.parent.position;
				spriteAnimation.SetBool ("Toggled", true);

				if (bossapproaching != null) {
					if (Camera.main.audio != null) {
						if (Camera.main.audio.clip != bossapproaching || !Camera.main.audio.isPlaying) {
							Camera.main.audio.Stop ();
							Camera.main.audio.clip = bossapproaching;
							Camera.main.audio.loop = true;
							Camera.main.audio.Play ();
						}
					}
				}
			}
		}
	}
}
