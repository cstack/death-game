using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.tag == GlobalConstant.Tag.Enemy){
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(1f);
		} else if (other.gameObject.layer == LayerMask.NameToLayer(GlobalConstant.Layer.EnemyProjectile)) {
			Destroy(other.gameObject);
		}
	}
}
