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

    public void Add_Ability(Ability ability) {
		AbilitySlot abilitySlot = abilitySlots [numAbilities++];
		abilitySlot.transform.FindChild ("Texture").GetComponent<UITexture> ().mainTexture = ability.abilityIcon;
		abilitySlot.gameObject.SetActive (true);
		ability.setAbilityGUI (abilitySlot);
    }
}
