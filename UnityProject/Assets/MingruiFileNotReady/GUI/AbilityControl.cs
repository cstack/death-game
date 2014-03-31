using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class AbilityControl : MonoBehaviour {

	public List<Ability> ability_array = new List<Ability>();
	public Ability current_ability; // The ability currently being used
    private GUIAbilityControl gui_ability; // for changing ability icons
	private Player player;
	private Ability abilityToActivate = null;

	void Start(){
		player = GameObject.Find ("Player").GetComponent<Player> ();
        gui_ability = GameObject.FindGameObjectWithTag("GUIAbilities").GetComponent<GUIAbilityControl>();
		foreach (Ability ability in ability_array) {
			if (ability != null) {
				ability.character = player;
				gui_ability.Add_Ability_Icon(ability.abilityIcon);
			}
		}
	}

	public void add_ability(Ability new_ability) {
		// check if ability is already in the ability list
		foreach(Ability abi in ability_array){
			if(new_ability.name == abi.name){
				// already has ability
				return;
			}
		}

		ability_array.Add(new_ability);
        gui_ability.Add_Ability_Icon(new_ability.abilityIcon);
	}

	public void Update() {
		AbilityDetect ();
	}

	void AbilityDetect () {
		abilityToActivate = null;

		if (Input.GetKeyDown(GlobalConstant.keycode_ability_1)) {
			if (ability_array.Count > 0) {
				abilityToActivate = ability_array[0];
			}
		} else if (Input.GetKeyDown(GlobalConstant.keycode_ability_2)) {
			if (ability_array.Count > 1) {
				abilityToActivate = ability_array[1];
			}
		} else if (Input.GetKeyDown(GlobalConstant.keycode_ability_3)) {
			if (ability_array.Count > 2) {
				abilityToActivate = ability_array[2];
			}
		} else if (Input.GetKeyDown(GlobalConstant.keycode_ability_4)) {
			if (ability_array.Count > 3) {
				abilityToActivate = ability_array[3];
			}
		}

		//ADD THIS AFTER TIMER ADDED TO ABILITIES:  && current_ability == null
		if (abilityToActivate != null) {
			current_ability = abilityToActivate;

			if (abilityToActivate.abilityClip != null) {
				audio.clip = abilityToActivate.abilityClip;
			}

			abilityToActivate.Activate();
		}
	}
}
