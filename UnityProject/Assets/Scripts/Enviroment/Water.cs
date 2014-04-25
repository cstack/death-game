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

	void OnBecameInvisible() {
		audio.enabled = false;
	}

	protected void enterWater () {
		PlayClip (underWater);
	}

	protected void exitWater () {
		PlayClip (aboveWater);
	}

	private void PlayClip (AudioClip aud) {

		if (audio != null) {
			audio.Stop();
			if (aboveWater != null) {
				audio.enabled = true;
				audio.loop = true;
				audio.clip = aud;
				audio.Play ();
			}
		}
	}

}
