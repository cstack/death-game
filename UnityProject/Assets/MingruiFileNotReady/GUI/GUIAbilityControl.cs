using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class GUIAbilityControl : MonoBehaviour {

    public GameObject q_icon;
    public GameObject w_icon;
    public GameObject e_icon;
    public GameObject r_icon;

    private List<GameObject> ability_icon_list = new List<GameObject>();
    private int ability_index = 3;
    private int max_index = 3;

	// Use this for initialization
	void Start () {
        ability_icon_list.Add(q_icon);
        ability_icon_list.Add(w_icon);
        ability_icon_list.Add(e_icon);
        ability_icon_list.Add(r_icon);
	}

    void Update() { 
        if(Input.GetKey(GlobalConstant.keycode_ability_4)){
            
        }
    }

    public void Add_Ability_Icon(Texture new_icon) {
        ability_icon_list[ability_index].GetComponent<UITexture>().mainTexture = new_icon;
        Refresh_Index();
    }

    // calculate the new index to use.
    // the index should cycle through towards the head of the list
    private void Refresh_Index()
    {
        if (ability_index == 0)
        {
            ability_index = max_index;
        }
        else
        {
            --ability_index;
        }
    }
}
