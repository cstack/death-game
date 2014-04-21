using UnityEngine;
using System.Collections;

public abstract class Water : MonoBehaviour {

	public AudioClip aboveWater;
	public AudioClip underWater;

	void OnBecameVisible () {
		if (audio != null) {
			if (!GameObject.Find ("Player").GetComponent<Player> ().headUnderwater) {
				audio.enabled = true;
				audio.loop = true;
			} else {
				audio.enabled = false;
			}
		}
	}

	protected void enterWater () {
		if (audio != null) {
			audio.Stop();
			if (underWater != null) {
				audio.enabled = true;
				audio.Stop();
				audio.loop = true;
				audio.clip = underWater;
				audio.Play ();
			}
		}
	}

	protected void exitWater () {
		if (audio != null) {
			audio.Stop();
			if (aboveWater != null) {
				audio.enabled = true;
				audio.loop = true;
				audio.clip = aboveWater;
				audio.Play ();
			}
		}
	}

}
