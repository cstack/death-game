using UnityEngine;
using System.Collections;

public class EnemyShot : EntityBase {

	//private int shotPower = 5;
	public float speed = 10;
	public float shotTimerEnd = 1.0f;
	private float shotTimer = 0;
	public int attackPower;

	private bool friendly;

	public FireballAbility ability;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if (shotTimer > shotTimerEnd)
		{
			Destroy (this.gameObject);
		}
		shotTimer += Time.deltaTime;
	}

	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.tag == "Player" && !friendly){
			Destroy(this.gameObject);
			other.gameObject.GetComponent<PlayerHealth>().decreaseHealth(attackPower, ability);
		}
		else if(other.gameObject.tag == "Enemy" && friendly){
			Destroy(this.gameObject);
			Destroy(other.gameObject);
		}
	}

	public void init_shot(EntityBase source, bool friend){
		friendly = friend;
		float height = source.GetComponent<SpriteRenderer>().bounds.size.y;
		transform.position = source.transform.position + new Vector3(0, height/2, 0);
		dir = source.dir;
		rigidbody2D.velocity = source.rigidbody2D.velocity;
		if(dir == Direction.Left){
			rigidbody2D.velocity += new Vector2(-speed, 0);
			transform.position += new Vector3(-1f, 0, 0);
		}
		else {
			rigidbody2D.velocity += new Vector2(speed, 0);
			transform.position += new Vector3(1f, 0, 0);
		}
	}
}
