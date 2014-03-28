using UnityEngine;
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
