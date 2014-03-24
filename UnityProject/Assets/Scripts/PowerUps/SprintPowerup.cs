using UnityEngine;
using System.Collections;

public class SprintPowerup : PowerBase {

	public Player player;
	public float speedIncrease = 2.0f;
	public float duration = 2.0f;
	public float cooldown = 3.0f;

	private float oldSpeed;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
		durationLength = duration;
		cooldownLength = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void setPlayer (Player _p)
	{
		player = _p;
	}

	public override void Activate()
	{
		oldSpeed = player.maxSpeed;
		player.maxSpeed = oldSpeed * speedIncrease;
	}

	public override void Deactivate()
	{
		setActive = false;
		cooldownLength = cooldown;
		player.maxSpeed = oldSpeed;
	}
}
