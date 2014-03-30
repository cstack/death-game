using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class AbilityControl : MonoBehaviour {
	
	public List<Ability> ability_array = new List<Ability>();
	public Ability current_ability; // The ability currently being used
    private GUIAbilityControl gui_ability; // for changing ability icons
	private Player player;
	
	void Start(){
		player = GameObject.Find ("Player").GetComponent<Player> ();
        gui_ability = GameObject.FindGameObjectWithTag("GUIAbilities").GetComponent<GUIAbilityControl>();
		foreach (Ability ability in ability_array) {
			ability.character = player;
			gui_ability.Add_Ability_Icon(ability.abilityIcon);
		}
	}

	public void add_ability(Ability new_ability) {
		ability_array.Add(new_ability);
        gui_ability.Add_Ability_Icon(new_ability.abilityIcon);
	}

	public void Update() {
		Ability abilityToActivate = null;
		if (Input.GetKeyDown(GlobalConstant.keycode_ability_1)) {
			abilityToActivate = ability_array[0];
		}

		if (abilityToActivate != null) {
			abilityToActivate.Activate();
			current_ability = abilityToActivate;
		}
	}
}
