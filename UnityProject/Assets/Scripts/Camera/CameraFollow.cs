using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform poi;
	public float u = 0.4f;
	public float minX = 0f;
	public float maxX = 100f;
	public float minY = 25.6f;
	public float maxY = 25.6f;
	public float yOffest = 5f;
	public float z = -9f;

	// Use this for initialization
	void Start () {
		poi = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = poi.position;
		Vector3 currentPos = transform.position;
		Vector3 newPos = (1 - u) * currentPos + u * targetPos;
		transform.position = new Vector3 (Mathf.Clamp(newPos.x, minX, maxX), Mathf.Clamp(newPos.y + yOffest, minY, maxY), z);
	}
}
