using UnityEngine;
using System.Collections;

public class WillOWispControl : MonoBehaviour {

    public float distance_from_player;
    private float speed = 10f;
    public GameObject player;
	public float player_height_offset = 1.5f;
	public float hover_duration = 0.2f;
	public float hover_timer = 0;
	public float hover_speed = 10f;

	// for bored movement
	public float bored_duration = 3f;
	public float bored_timer = 0;
	public float bored_speed = 6f;

	// Use this for initialization
	void Start () {
//		speed = player.GetComponent <Player> ().maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		// updated every frame
		hover_timer += Time.deltaTime;

        // follow the player
		if (Mathf.Abs(transform.position.x - player.transform.position.x) > distance_from_player 
		    || Mathf.Abs(transform.position.y - player.transform.position.y) > distance_from_player)
        {
			rigidbody2D.velocity = ((new Vector3(player.transform.position.x, player.transform.position.y + player_height_offset, player.transform.position.z) - transform.position).normalized) * player.rigidbody2D.velocity.magnitude;
        }
        else {
			// idle position, do a little hover

			// reset velocity to zero
            rigidbody2D.velocity = new Vector3(0, 0, 0);

			// hover
			if(hover_timer > hover_duration){
				hover_timer = 0;
				// change direction
				hover_speed *= -1;
				rigidbody2D.velocity = new Vector3(0, hover_speed, 0);
			}
        }

		Bored_Movement();
	}

	// long idle, bored movement
	void Bored_Movement(){
		if(rigidbody2D.velocity.x != 0){
			bored_timer = 0;
		}
		bored_timer += Time.deltaTime;
		if(bored_timer > bored_duration){
			// bored movement, go to the player to sort of nudge the player to make player notice and keep going
			// WHAT TO DO HERE OH GOD!
		}
	}
}
