using UnityEngine;
using System.Collections;

public class StillWater : Water {
	public Texture2D[] frames = null;
	public float framesPerSecond = 10.0f;

	void Update () {
		if (frames != null && frames.Length > 0) {
			int index = (int) (Time.time * framesPerSecond);
			index = index % frames.Length;
			renderer.material.mainTexture = frames[index];
		}
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag != "PlayerHead") {
			enterWater();
		}
		
	}
	
	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag != "PlayerHead") {
			exitWater();
		}
	}
}
