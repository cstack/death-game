using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int currentHealth;
	public int maxHealth;
	public int currentBreath;
	public int maxBreath;
	public bool inWater = false;
	public float breathPercent = 1f;
	public int current_life_count;
	public bool invulnerable;
	public GameObject spawnPoint;

	private Transform poi;
	private TimerControl timer_control;

	void Start() {
		poi = GameObject.Find("Player").transform;
		timer_control = poi.GetComponent<TimerControl>();
	}

	public void enterWater () {
		inWater = true;
	}
	
	public void exitWater () {
		inWater = false;
		currentBreath = maxBreath;
		breathPercent = (float) currentBreath / maxBreath;
	}
	
	public void decreaseBreath(int amount) {
		if(currentBreath <= 0) {
			decreaseHealth (amount);
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
	
	public void decreaseHealth(int amount) {
		if(invulnerable){return;}
		currentHealth -= amount;
		checkAlive();
	}
	
	public void increaseMaxHealth(int amount) {
		maxHealth += amount;
	}
	
	public void increaseLifeCount(int amount){
		current_life_count += amount;
	}
	
	private void checkAlive() { 
		if(currentHealth <= 0){
			playerDeath();
		}
	}
	
	private void checkPossible() { 
		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		}
	}
	
	// check if perma or temp death
	public void playerDeath() {
		--current_life_count;
		if(current_life_count <= 0){
			permaDeath();
		}
		else
		{
			tempDeath();
		}
	}
	
	private void permaDeath()
	{
		endGame();
	}
	
	private void tempDeath()
	{
		// reinitialize everything
		currentHealth = maxHealth;

		timer_control.restartTimer();

		respawn();
	}
	
	private void respawn() {
		poi.position = spawnPoint.transform.position;
	}
	
	private void endGame() { 
		
	}
}
