﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiverWater : MonoBehaviour {
	
	public Texture2D[] frames = null;
	public float framesPerSecond = 10.0f;
	public float currentStrength = 3.0f;

	public List<GameObject> objectsInRiver;

	void Update () {
		if (frames != null) {
			int index = (int) (Time.time * framesPerSecond);
			index = index % frames.Length;
			renderer.material.mainTexture = frames[index];
		}

		if (objectsInRiver.Count > 0) {
			moveObjects ();
		}
	}

	void moveObjects () {
		foreach (GameObject gob in objectsInRiver) {
			print ("yes");
			if (gob.rigidbody2D != null) {
				print ("no");
				Vector2 vel = gob.rigidbody2D.velocity;
				vel.x = vel.x + currentStrength;
				gob.rigidbody2D.velocity = vel;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag != "PlayerHead" && !objectsInRiver.Contains (col.gameObject)) {
			objectsInRiver.Add (col.gameObject);
			EntityBase tmp = col.gameObject.GetComponent<EntityBase> ();

			tmp.enterRiver (currentStrength);
		}
	}
	
	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag != "PlayerHead") {
			objectsInRiver.Remove (col.gameObject);
			EntityBase tmp = col.gameObject.GetComponent<EntityBase> ();

			tmp.exitRiver ();
		}
	}
}
