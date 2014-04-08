using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform poi;
	public float u = 0.4f;
	public float minX = -1f;
	public float maxX = 100f;
	public float minY = 25.6f;
	public float maxY = 200f;
	public float yOffest = 5f;
	public float z = -9f;

	Vector3 targetPos;
	Vector3 currentPos;
	Vector3 newPos;

		// Use this for initialization
	void Start () {
		poi = GameObject.Find ("Player").transform;

		camera.transparencySortMode = TransparencySortMode.Orthographic;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		targetPos = poi.position;
		currentPos = transform.position;
		newPos = (1 - u) * currentPos + u * targetPos;
		transform.position = new Vector3 (Mathf.Clamp(newPos.x, minX, maxX), Mathf.Clamp(newPos.y + yOffest, minY, maxY), z);
	}
}
