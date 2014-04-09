using UnityEngine;
using System.Collections;

public class DieAfterTime : MonoBehaviour {
	public float time;
	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > time) {
			Destroy(gameObject);
		}
	}
}
