using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class AbilityControl : MonoBehaviour {

	public BasicAttack basicAttack;
	public List<Ability> abilities = new List<Ability>();
	public int maxAbilities = 3;
	public Ability current_ability; // The ability currently being used
	public bool animating = false;
	private float animationtimer = 0f;
    private GUIAbilityControl gui_ability; // for changing ability icons
	private Player player;
	private Ability abilityToActivate = null;

	void Start(){
		player = GameObject.Find ("Player").GetComponent<Player> ();
        gui_ability = GameObject.FindGameObjectWithTag("GUIAbilities").GetComponent<GUIAbilityControl>();

		basicAttack.player = player;
		foreach (Ability ability in abilities) {
			if (ability != null) {
				ability.player = player;
			}
		}
		updateAbilityUI ();
	}

	private void updateAbilityUI () {
		List<Ability> abilitiesToShow = new List<Ability>{basicAttack};
		abilitiesToShow.AddRange (abilities);
		gui_ability.setAbilities (abilitiesToShow);
	}

	public void add_ability(Ability new_ability) {
		// check if ability is already in the ability list
		foreach(Ability abi in abilities){
			if(new_ability.name == abi.name){
				// already has ability
				return;
			}
		}

		if (abilities.Count >= maxAbilities) {
			// Remove oldest ability
			abilities.RemoveAt(abilities.Count - 1);
		}

		new_ability.player = player;
		abilities.Insert (0, new_ability); // Add ability to newest position
		updateAbilityUI ();
	}

	public void Update() {
		if (!animating) {
			AbilityDetect ();
		}

		if (animationtimer > 0f) {
			animationtimer -= Time.deltaTime;
		} else if (animating) {
			animating = false;
		}
	}

	void AbilityDetect () {
		abilityToActivate = null;

		if (Input.GetKeyDown(GlobalConstant.keycode_ability_1)) {
			abilityToActivate = basicAttack;

			//Example Animation Timer Use - Move Time (0.5f now) Into Each Ability, As Well As OnAbilityHit/Finish Timers, Calls, etc.
			animating = true;
			animationtimer = 0.5f;

//			player.updateXVelocity(0f);

		} else if (Input.GetKeyDown(GlobalConstant.keycode_ability_2)) {
			if (abilities.Count > 0) {
				abilityToActivate = abilities[0];
			}
		} else if (Input.GetKeyDown(GlobalConstant.keycode_ability_3)) {
			if (abilities.Count > 1) {
				abilityToActivate = abilities[1];
			}
		} else if (Input.GetKeyDown(GlobalConstant.keycode_ability_4)) {
			if (abilities.Count > 2) {
				abilityToActivate = abilities[2];
			}
		}

		if (abilityToActivate == null) {
			return;
		}

		if (player.ghost) {
			return;
		}

		//ADD THIS AFTER TIMER ADDED TO ABILITIES:  && current_ability == null
		current_ability = abilityToActivate;

		if (abilityToActivate.abilityClip != null) {
			audio.clip = abilityToActivate.abilityClip;
		}

		abilityToActivate.Activate();
	}
}
