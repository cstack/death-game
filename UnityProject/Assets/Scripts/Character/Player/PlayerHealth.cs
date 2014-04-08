using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int currentHealth;
	public int maxHealth;
	public int currentBreath;
	public int maxBreath;
	private bool inWater;
	public float breathPercent = 1f;
	public int current_life_count;
	public bool invulnerable;
	public GameObject spawnPoint;
	public LungCapacityAbility lungs;

	private Player poi;
	private TimerControl timer_control;

	void Start() {
		poi = (Player)GameObject.Find("Player").GetComponent<Player>();
		timer_control = poi.GetComponent<TimerControl>();
	}

	public void resetBreath() {
		currentBreath = maxBreath;
		breathPercent = (float) currentBreath / maxBreath;
	}

	public void decreaseBreath(int amount) {
		if(currentBreath <= 0) {
			decreaseHealth (amount, lungs);
		} else {
			currentBreath -= amount;
			breathPercent = (float) currentBreath / maxBreath;
		}
	}
	
	public void resetHealth() {
		currentHealth = maxHealth;
	}

	public void increaseHealth(int amount) {
		currentHealth += amount;
		checkPossible();
	}
	
	public void decreaseHealth(int amount, Ability ability) {
		if(invulnerable){return;}
		currentHealth -= amount;
		checkAlive(ability);
	}
	
	public void increaseMaxHealth(int amount) {
		maxHealth += amount;
	}
	
	public void increaseLifeCount(int amount){
		current_life_count += amount;
	}
	
	private void checkAlive(Ability ability) { 
		if(currentHealth <= 0){
			playerDeath(ability);
		}
	}
	
	private void checkPossible() { 
		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		}
	}
	
	// check if perma or temp death
	public void playerDeath(Ability ability) {
		--current_life_count;
		resetPlayer ();
		if(current_life_count <= 0){
			permaDeath();
		}
		else
		{
			tempDeath(ability);
		}
	}
	
    // Time is RESET here
	private void permaDeath()
	{
		timer_control.restartTimer();
		Application.LoadLevel (Application.loadedLevel);
	}

    // This function does NOT reset time
	private void resetPlayer() {
		currentHealth = maxHealth;
		poi.transform.position = spawnPoint.transform.position;
		if (poi.feetInWater) {
			poi.feetExitWater();
		}
		if (poi.headUnderwater) {
			poi.headExitWater();
		}
	}
	
	private void tempDeath(Ability ability)
	{
		if (ability == null) {
			return;
		}

		//Prints ability that's being added
//		Debug.Log ("Died and gained the ability " + ability.abilityName);
	
		if (ability != null) {
			poi.AddAbility(ability);
		}
	}
}
