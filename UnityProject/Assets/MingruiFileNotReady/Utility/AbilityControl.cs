using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class AbilityControl : MonoBehaviour {

	private List<Ability> ability_array = new List<Ability>();
	private GameObject player;
	private GUIControl gui_control;


	void Start(){
		player = GameObject.FindWithTag("Player");
		gui_control = player.GetComponent<GUIControl>();
	}

	public void add_ability(Ability new_ability) {
		ability_array.Add(new_ability);
		gui_control.Add_Ability_Icon(new_ability.abilityIcon);
	}
}
