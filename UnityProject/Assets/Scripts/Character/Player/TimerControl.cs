using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerHealth))]

public class TimerControl : MonoBehaviour {
	// the time you have in this cycle of life
	public float available_time;
	public float max_available_time;
	// the scale of decrease relative to real world time
	private float decreasing_scale = 1;
	
	// variables to keep track of progress, in real world time
	// the time spent in this life
	private float cycle;
	// the total time spent in all lives
	private float age;
	
	private Transform poi;
	private PlayerHealth player_health;
	
	// Use this for initialization
	void Start () {
		poi = GameObject.Find("Player").transform;
		player_health = poi.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		cycle += Time.deltaTime;
		
		available_time -= decreasing_scale * Time.deltaTime;
		
		// if time runs out
		if(available_time <= 0){
			player_health.playerDeath(null);
		}
	}
	
	public float read_time(){
		return available_time;
	}
	
	public void increase_time(float amount){
		available_time += amount;
	}
	
	public void decrease_time(float amount){
		available_time -= amount;
	}
	
	public void decelerate_time(float scale){
		decreasing_scale *= scale;
	}
	
	public void accelerate_time(float scale){
		decreasing_scale /= scale;
	}

	public void restartTimer(){
		available_time = max_available_time;
	}
}
