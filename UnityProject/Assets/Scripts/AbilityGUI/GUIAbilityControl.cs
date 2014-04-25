using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class GUIAbilityControl : MonoBehaviour {

	public List<AbilitySlot> abilitySlots;
    private int numAbilities = 0;

	// Use this for initialization
	void Start () {
		foreach (AbilitySlot abilitySlot in abilitySlots) {
			abilitySlot.gameObject.SetActive(false);
		}
	}

    void Update() {
        if (Input.GetKey(GlobalConstant.keycode_ability_1)
            || Input.GetKeyDown(GlobalConstant.controller_ability_1))
        {
            abilitySlots[0].startCooldownAnimation();
        }
        if (Input.GetKey(GlobalConstant.keycode_ability_2)
            || Input.GetKeyDown(GlobalConstant.controller_ability_2))
        {
            abilitySlots[1].startCooldownAnimation();
        }
        if (Input.GetKey(GlobalConstant.keycode_ability_3)
            || Input.GetKeyDown(GlobalConstant.controller_ability_3))
        {
            abilitySlots[2].startCooldownAnimation();
        }
    }

	private void setAbilitySlot(int index, Ability ability) {
		AbilitySlot abilitySlot = abilitySlots [index];
		abilitySlot.transform.FindChild ("Texture").GetComponent<UITexture> ().mainTexture = ability.abilityIcon;
        abilitySlot.initializeCooldownTime(ability.cooldown);
		abilitySlot.gameObject.SetActive (true);
		ability.setAbilityGUI (abilitySlot);
	}

	public void setAbilities(List<Ability> abilities) {
		for (int i = 0; i < abilities.Count; i++) {
			setAbilitySlot(i, abilities[i]);
		}
	}
}
