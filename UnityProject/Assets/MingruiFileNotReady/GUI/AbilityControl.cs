using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class AbilityControl : MonoBehaviour {

	private List<Ability> ability_array = new List<Ability>();
    private GUIAbilityControl gui_ability; // for changing ability icons
	private GameObject player;


	void Start(){
		player = GameObject.FindWithTag("Player");
        gui_ability = GameObject.FindGameObjectWithTag("GUIAbilities").GetComponent<GUIAbilityControl>();
	}

	public void add_ability(Ability new_ability) {
		ability_array.Add(new_ability);
        gui_ability.Add_Ability_Icon(new_ability.abilityIcon);
	}
}
