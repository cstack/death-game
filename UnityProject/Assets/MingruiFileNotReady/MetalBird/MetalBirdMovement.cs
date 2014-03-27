using UnityEngine;
using System.Collections;

public class MetalBirdMovement : MonoBehaviour {

    public int speed;
    public int engage_range;
    public int dive_distance;
    private bool is_rising = true;

    private GameObject player;
    private float distance_from_player;
    private bool engage = false;

    // idle hover
    public float hover_speed;
    public float hover_oneway_time;
    private float hover_dir_time = 0;
    private Vector3 nest_position; // the initial position of this bird
    private Vector3 out_of_nest_tolerance = new Vector3(0.5f, 0.5f, 0.5f);
    private bool is_nested = true; // starts at the nest position
    private float return_nest_speed = 3;

    void Awake() {
        player = GameObject.FindWithTag("Player");
    }

	void Start () {
        nest_position = transform.position;
        //Debug.Log(nest_position);
	}
	
	void Update () {
        spot_player();

        movement();
	}

    void spot_player() { 
        distance_from_player = Mathf.Sqrt( 
            (transform.position.x - player.transform.position.x)
            * (transform.position.x - player.transform.position.x)
            + (transform.position.y - player.transform.position.y)
            * (transform.position.y - player.transform.position.y)
            );

        if (distance_from_player < engage_range)
        {
            engage = true;
            //Debug.Log("metal bird engage!");
        }
        else {
            engage = false;
            //Debug.Log("metal bird disengage!");
        }
    }

    void movement() {
        if (engage) { 
            // when engages, get out of nest position
            is_nested = false;
            engage_movement();
        }
        else {
            if (is_nested)
            {
                idle_hover();
            }
            else {
                return_to_nest();
            }
        }
    }

    void engage_movement() {
        rising_movement();

        // attack, called from another script
    }

    void rising_movement() {
        if (is_rising && distance_between(transform, player.transform) < engage_range)
        {
            // rise in reverse to the player direction
            rigidbody2D.velocity = (transform.position - player.transform.position).normalized * speed;
        }
        else{
            stop();
            is_rising = false;
        }
    }

    void idle_hover() {
        hover_dir_time += Time.deltaTime;
        // change direction
        if(hover_dir_time > hover_oneway_time){
            hover_dir_time = 0;
            hover_speed = -hover_speed;
            rigidbody2D.velocity = new Vector2(0, hover_speed);
        }
    }

    void return_to_nest() {
        // stop all other movement, not using velocity anymore
        stop();

        // nest is a small area, detecting exact position is tricky with floats
        if (!(Mathf.Abs(transform.position.x - nest_position.x) < out_of_nest_tolerance.x
            && Mathf.Abs(transform.position.y - nest_position.y) < out_of_nest_tolerance.y
            && Mathf.Abs(transform.position.z - nest_position.z) < out_of_nest_tolerance.z))
        {
            // move position (over time) towards the nest direction
            transform.position += (nest_position - transform.position).normalized
                * Time.deltaTime * return_nest_speed;
            //Debug.Log((nest_position - transform.position).normalized);
        }
        else {
            is_nested = true;
        }
    }

    // public functions

    public bool is_engage() {
        return engage;
    }

    private float distance_between(Transform a, Transform b) {
        return Mathf.Sqrt(
            (a.position.x - b.position.x)
            * (a.position.x - b.position.x)
            + (a.position.y - b.position.y)
            * (a.position.y - b.position.y)
            );
    }

    private void stop(){
        rigidbody2D.velocity = new Vector2(0, 0);
    }
}
