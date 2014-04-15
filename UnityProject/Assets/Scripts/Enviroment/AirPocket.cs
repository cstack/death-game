using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AirPocket : MonoBehaviour {
	
	public EnvironmentDamage fatherWater;

	public Texture2D[] frames = null;
	public float framesPerSecond = 10.0f;

	void Update () {
		if (frames != null) {
			int index = (int) (Time.time * framesPerSecond);
			index = index % frames.Length;
			renderer.material.mainTexture = frames[index];
		}

	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "PlayerHead") {
			fatherWater.enterAirPocket ();
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "PlayerHead") {
			fatherWater.exitAirPocket ();
		}
	}
}
