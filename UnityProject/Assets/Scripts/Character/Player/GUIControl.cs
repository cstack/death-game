using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(TimerControl))]

public class GUIControl : MonoBehaviour {
	private Transform poi;
	private TimerControl timer_control;
	private PlayerHealth player_health;
	
	public GUIStyle customGUIStyle;
	public float sizeConstant = 1.0f;
	private Rect timer_pos = new Rect(20f, 20f, 40f, 20f);
	private Rect health_pos = new Rect(20f, 40f, 40f, 20f);
	private Rect life_pos = new Rect(20f, 60f, 40f, 20f);
	private Rect breath_pos = new Rect(20f, 80f, 80f, 10f);
	private Rect breath = new Rect(20f, 80f, 80f, 10f); 
	
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
	
	void OnGUI ()
	{
		time_text = string.Format("{0:00}:{1:00}", timer_control.read_time() / 60, timer_control.read_time() % 60);
		GUI.Label(timer_pos, "Time: " + time_text, customGUIStyle);
		GUI.Label(health_pos, "Health: " + player_health.currentHealth + " / " + player_health.maxHealth, customGUIStyle);
		GUI.Label(life_pos, "Lives: " + player_health.current_life_count, customGUIStyle);


		if (player_health.isDrowning()) {
			breath_pos = new Rect(20f, 80f, 125f, 15f);
			GUI.skin.box.normal.background = MakeTex (1, 1, Color.black);
			GUI.Box (breath_pos, "");

			//GUI Box behaves strangely 
			if (player_health.breathPercent > 0.04f) {
				GUI.skin.box.normal.background = MakeTex (1, 1, Color.blue);
				breath = new Rect(20f, 80f, 125f * player_health.breathPercent, 15f);
//				Debug.Log (125f * player_health.breathPercent);
				GUI.Box (breath, "");
			}
		}

	}

	private Texture2D MakeTex (int width, int height, Color col) {
		Color[] pix = new Color[width * height];
		
		for( int i = 0; i < pix.Length; ++i ) {
			pix[ i ] = col;
		}
		
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
		
	}
}
