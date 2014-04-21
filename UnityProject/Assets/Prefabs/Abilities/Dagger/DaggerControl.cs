using UnityEngine;
using System.Collections;

public class DaggerControl : MonoBehaviour {

    public int attackPower = 50;
    public Ability deathAbility = null;

    // disappear after collision or after a timer
    void OnCollisionEnter2D(Collision2D col) { 
        // if collided with player
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().decreaseHealth(attackPower, deathAbility);
		} else if (col.gameObject.layer == LayerMask.NameToLayer("Enemies"))
		{
			col.gameObject.GetComponent<EnemyHealth>().TakeDamage(1f);
		}
        // if with other

        Destroy(gameObject);
    }
}
