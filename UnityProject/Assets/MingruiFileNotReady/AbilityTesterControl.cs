using UnityEngine;
using System.Collections;

public class AbilityTesterControl : MonoBehaviour {

	public Ability ability_to_test;

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			Debug.Log("Player");
		}
	}

}
