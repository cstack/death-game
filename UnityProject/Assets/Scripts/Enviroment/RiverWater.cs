using UnityEngine;
using System.Collections;

public class RiverWater : MonoBehaviour {
	
	public Texture2D[] frames = null;
	public float framesPerSecond = 10.0f;
	
	void Update () {
		if (frames != null) {
			int index = (int) (Time.time * framesPerSecond);
			index = index % frames.Length;
			renderer.material.mainTexture = frames[index];
		}
	}
	
}
