using UnityEngine;
using System.Collections;

public class EnemyDestroyer : MonoBehaviour {

	private IEnumerator Start() {
		float initialVolume = Camera.main.audio.volume;
		float volume = initialVolume;
		float initialTime = Time.time;
		float timeToFade = 5f;
		while (Time.time - initialTime < timeToFade) {
			float percentThroughFade = (Time.time - initialTime) / timeToFade;
			float volumeMultiplier = 1 - percentThroughFade;
			Camera.main.audio.volume = initialVolume * volumeMultiplier;
			yield return null;
		}
	}

	private void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.tag == GlobalConstant.Tag.Enemy){
			Destroy(other.gameObject);
		}
	}
}
