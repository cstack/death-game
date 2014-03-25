﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(TimerControl))]

public class GUIControl : MonoBehaviour {
	public GUIStyle customGUIStyle;
	
	private Transform poi;
	private TimerControl timer_control;
	private PlayerHealth player_health;
	
	// timer text format
	private string time_text;
	// breath bar format
	public Texture2D fgImage;
	public Texture2D bgImage;
	// ability box
	private GUIStyle abilityStyle;
	public Texture2D ability_1_icon;
	public Texture2D ability_2_icon;
	public Texture2D ability_3_icon;
	public Texture2D ability_4_icon;
	// ability font
	private GUIStyle afontStyle;
	
	
	void Start () {
		poi = GameObject.Find ("Player").transform;
		timer_control = poi.GetComponent<TimerControl>();
		player_health = poi.GetComponent<PlayerHealth>();
		
		// setup ability box style
		abilityStyle = new GUIStyle();
		abilityStyle.fixedHeight = 48;
		abilityStyle.fixedWidth = 48;
		abilityStyle.margin = new RectOffset(4, 4, 4, 4);
		
		// setup ability font style
		afontStyle = new GUIStyle();
		afontStyle.fixedHeight = 48;
		afontStyle.fixedWidth = 48;
		afontStyle.margin = new RectOffset(4, 4, 4, 4);
		afontStyle.fontSize = 20;
		afontStyle.alignment = TextAnchor.MiddleCenter;
	}
	
	void OnGUI ()
	{
		healthUI();
		AbilityUI();
	}
	
	// Mingrui
	private void healthUI(){
		time_text = string.Format("{0:00}:{1:00}", timer_control.read_time() / 60, timer_control.read_time() % 60);
		
		// time health lives
		GUILayout.BeginArea(new Rect(20, 20, 200, 80));
		GUILayout.Label("Time: " + time_text, customGUIStyle);
		GUILayout.Label("Health: " + player_health.currentHealth + " / " + player_health.maxHealth, customGUIStyle);
		GUILayout.Label("Lives: " + player_health.current_life_count, customGUIStyle);
		GUILayout.Label("Breath: ", customGUIStyle);
		GUILayout.EndArea();
		
		// breath bar
		GUILayout.BeginArea(new Rect(78, 78, 100, 14));
		GUI.DrawTexture(new Rect(0, 0, 100, 16), bgImage);
		GUI.DrawTexture(new Rect(0, 0, 100*player_health.breathPercent, 16), fgImage);
		GUILayout.EndArea();
	}
	
	// Mingrui
	private void AbilityUI(){
		GUILayout.BeginArea(new Rect(20, 100, 200, 100));
		GUILayout.BeginHorizontal();
		GUILayout.Box(ability_1_icon, abilityStyle);
		GUILayout.Box(ability_2_icon, abilityStyle);
		GUILayout.Box(ability_3_icon, abilityStyle);
		GUILayout.Box(ability_4_icon, abilityStyle);
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(20, 134, 200, 100));
		GUILayout.BeginHorizontal();
		GUILayout.Box("Q", afontStyle);
		GUILayout.Box("W", afontStyle);
		GUILayout.Box("E", afontStyle);
		GUILayout.Box("R", afontStyle);
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
