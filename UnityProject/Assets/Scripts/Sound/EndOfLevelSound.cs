using UnityEngine;
using System.Collections;

public class EndOfLevelSound : MonoBehaviour {

	public void FadeOut(float timeToFade) {
		StartCoroutine (FadeOutCoroutine (timeToFade));
	}

	public IEnumerator FadeOutCoroutine(float timeToFade) {
		float initialVolume = audio.volume;
		float volume = initialVolume;
		float initialTime = Time.time;
		while (Time.time - initialTime < timeToFade) {
			float percentThroughFade = (Time.time - initialTime) / timeToFade;
			float volumeMultiplier = 1 - percentThroughFade;
			audio.volume = initialVolume * volumeMultiplier;
			yield return null;
		}
	}
}
