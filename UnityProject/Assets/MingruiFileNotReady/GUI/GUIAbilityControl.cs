using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class GUIAbilityControl : MonoBehaviour {

	public List<GameObject> abilitySlots;
    private int numAbilities = 0;

	// Use this for initialization
	void Start () {
		foreach (GameObject abilitySlot in abilitySlots) {
			abilitySlot.SetActive(false);
		}
	}

    void Update() { 
        if(Input.GetKey(GlobalConstant.keycode_ability_4)){
            
        }
    }

    public void Add_Ability_Icon(Texture new_icon) {
		GameObject abilitySlot = abilitySlots [numAbilities++];
		abilitySlot.transform.FindChild ("Texture").gameObject.GetComponent<UITexture> ().mainTexture = new_icon;
		abilitySlot.SetActive (true);
    }
}
