using UnityEngine;
using System.Collections;

public class EnemyDestroyer : MonoBehaviour {

	private void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.tag == GlobalConstant.Tag.Enemy){
			Destroy(other.gameObject);
		}
	}
}
