using UnityEngine;
using System.Collections;

public class WillOWispControl : MonoBehaviour {

    public float distance_from_player;
    private float speed;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        speed = player.GetComponent<Player>().maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        // follow the player
        if (Mathf.Abs(transform.position.x - player.transform.position.x) > distance_from_player)
        {
            rigidbody2D.velocity = ((player.transform.position - transform.position).normalized) * speed;
        }
        else {
            rigidbody2D.velocity = new Vector3(0, 0, 0);
        }

        //Debug.Log(Mathf.Abs(transform.position.x - player.transform.position.x));
	}
}
