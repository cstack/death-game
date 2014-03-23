using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(TimerControl))]

public class GUIControl : MonoBehaviour {
	private Transform poi;
	private TimerControl timer_control;
	private PlayerHealth player_health;
	
	public GUIStyle customGUIStyle;
	private Rect timer_pos = new Rect(20, 20, 40, 20);
	private Rect health_pos = new Rect(20, 40, 40, 20);
	private Rect life_pos = new Rect(20, 60, 40, 20);
	
	private string time_text;
	
	// Use this for initialization
	void Start () {
		poi = GameObject.Find ("Player").transform;
		timer_control = poi.GetComponent<TimerControl>();
		player_health = poi.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(timer_control.read_time());
	}
	
	void OnGUI()
	{
		time_text = string.Format("{0:00}:{1:00}", timer_control.read_time() / 60, timer_control.read_time() % 60);
		GUI.Label(timer_pos, "Time: " + time_text, customGUIStyle);
		GUI.Label(health_pos, "Health: " + player_health.currentHealth + " / " + player_health.maxHealth, customGUIStyle);
		GUI.Label(life_pos, "Lives: " + player_health.current_life_count, customGUIStyle);
	}
	
}
