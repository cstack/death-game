using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

	//private int shotPower = 5;
	
	public float shotTimerEnd = 1.0f;
	private float shotTimer = 0;
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
	
	/*void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "hero")
		{
			Destroy(this.gameObject);
			col.gameObject.GetComponent<Health>().decreaseHealth(shotPower);
		}
	}*/

	void OnCollisionEnter2D (Collision2D other){
		if(other.gameObject.tag == "Player"){
			Destroy(this.gameObject);
		}
	}

}
