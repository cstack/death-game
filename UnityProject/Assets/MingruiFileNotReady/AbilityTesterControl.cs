using UnityEngine;
using System.Collections;

public class AbilityTesterControl : MonoBehaviour {

	public Ability ability_to_test;
	private int attackPower = 100000;

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			//Debug.Log("Player");
			col.gameObject.GetComponent<PlayerHealth>().decreaseHealth(attackPower, ability_to_test);
		}
	}

}
