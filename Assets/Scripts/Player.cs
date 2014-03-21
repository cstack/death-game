using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float maxSpeed = 5f;

	private float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		speed = Input.GetAxis ("Horizontal") * maxSpeed;
		rigidbody2D.velocity = new Vector2 (speed, 0);
	}
}
