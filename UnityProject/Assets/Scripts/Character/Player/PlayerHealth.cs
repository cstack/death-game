using UnityEngine;
using System.Collections;
using Parse;

public class PlayerHealth : MonoBehaviour {
	public float currentHealth;
	public float maxHealth;
	public int currentBreath;
	public int maxBreath;
	public float hurtTime = 1.0f;
	private bool inWater;
	private float hurtTimer = 0f;
	public float breathPercent = 1f;
	public int current_life_count;
	public bool invulnerable;
	public GameObject spawnPoint;
	public LungCapacityAbility lungs;
	public int death_count;

	public AudioClip Hurt;
	public AudioClip Dying;

	private Player poi;
	private float lifeTime;
	private TimerControl timer_control;
	private BonusAnnouncer bonus_announcer;

	void Start() {
		poi = (Player)GameObject.Find("Player").GetComponent<Player>();
		timer_control = poi.GetComponent<TimerControl>();
		bonus_announcer = (BonusAnnouncer)GameObject.Find("Bonus Announcer").GetComponent<BonusAnnouncer>();
		transform.position = spawnPoint.transform.position;
	}

	void Update() {
		lifeTime += Time.deltaTime;

		if (hurtTimer > 0f) {
			hurtTimer -= Time.deltaTime;
		}
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

	public void increaseHealth(float amount) {
		currentHealth += amount;
		checkPossible();
	}
	
	public void decreaseHealth(float amount, Ability ability) {
		if (hurtTimer <= 0f) {
			poi.PlayClipOnAction (Hurt);
			hurtTimer = hurtTime;
		}

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
		DataLogging.TrackPlayerDeath(ability, lifeTime, transform.position);

		death_count++;
		resetPlayer ();
		poi.becomeGhost ();
		if(current_life_count <= 0){
			permaDeath();
		}
		else {
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
		lifeTime = 0;
		currentHealth = maxHealth;

		poi.AbilityAnimationFinished();

		if (poi.feetInWater) {
			poi.feetExitWater();
		}
		if (poi.headUnderwater) {
			poi.headExitWater();
		}
	}
	
	private void tempDeath(Ability ability)
	{
		if (Dying != null) {
			poi.PlayClipOnAction (Dying);
		}
		if (ability == null) {
			return;
		}

		
		//Prints ability that's being added
//		Debug.Log ("Died and gained the ability " + ability.abilityName);
	
		if (poi.AddAbility(ability)) {
			bonus_announcer.Announce_Bonus("New Ability: " + ability.abilityName, 5f);
		}

	}
}
