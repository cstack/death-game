using UnityEngine;
using System.Collections;

public class DamageOverlay : MonoBehaviour {
	private UITexture overlay;
	private PlayerHealth player_health;
	
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		player_health = player.GetComponent<PlayerHealth>();
		overlay = GetComponent<UITexture> ();
	}
	
	void Update()
	{
		overlay.alpha = 1 - (player_health.currentHealth / player_health.maxHealth);
	}
}
