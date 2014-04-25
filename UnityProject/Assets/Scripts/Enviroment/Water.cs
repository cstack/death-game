using UnityEngine;
using System.Collections;

public abstract class Water : MonoBehaviour {

	public AudioClip aboveWater;
	public AudioClip underWater;

	void OnBecameVisible () {
		if (audio != null) {
			if (audio.clip != null) {
				if (!GameObject.Find ("Player").GetComponent<Player> ().headUnderwater) {
					audio.enabled = true;
					audio.loop = true;
				} else {
					audio.enabled = false;
				}
			} else if (aboveWater != null) {
				audio.clip = aboveWater;
			} else if (underWater != null && GameObject.Find ("Player").GetComponent<Player> ().headUnderwater) {
				audio.clip = underWater;
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
