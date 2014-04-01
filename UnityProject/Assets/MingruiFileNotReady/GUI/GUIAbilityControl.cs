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
        if(Input.GetKey(GlobalConstant.keycode_ability_4)){
            
        }
    }

	private void setAbilitySlot(int index, Ability ability) {
		AbilitySlot abilitySlot = abilitySlots [index];
		abilitySlot.transform.FindChild ("Texture").GetComponent<UITexture> ().mainTexture = ability.abilityIcon;
		abilitySlot.gameObject.SetActive (true);
		ability.setAbilityGUI (abilitySlot);
	}

	public void setAbilities(List<Ability> abilities) {
		for (int i = 0; i < abilities.Count; i++) {
			setAbilitySlot(i, abilities[i]);
		}
	}
}
