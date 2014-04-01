using UnityEngine;
using System.Collections;

public class JavelinAbility : ActiveAbility, Backpack.Observer {

	public GameObject javelin_prefab;

	protected override void OnActivate () {
		if(character.backpack.Get_Javelin() > 0){

			GameObject new_javelin;
			int aim;
			if(character.dir == EntityBase.Direction.Left){
				new_javelin = (GameObject)Instantiate(javelin_prefab,
				                                      character.transform.position + new Vector3(1, 2f, 0),
				                                      transform.rotation);
				aim = (int) GlobalConstant.direction.left;
			}
			else{
				new_javelin = (GameObject)Instantiate(javelin_prefab,
				                                      character.transform.position + new Vector3(-1, 2f, 0),
				                                      transform.rotation);
				aim = (int) GlobalConstant.direction.right;
			}
			
			new_javelin.GetComponent<JavelinControl>().Create_Javelin(character.gameObject, aim);
			new_javelin.GetComponent<JavelinControl>().friendly = true; // change to try to fix javelin going through ground
			new_javelin.layer = 12;
			character.backpack.remove_jevelin(1);

			//Change this to be called after the animation finishes, if implemented
			character.gameObject.GetComponent<Player> ().AbilityAnimationFinished ("JavelinAbility");

			//Debug.Log(new_javelin.GetComponent<JavelinControl>().Check_If_Flying());
		}
	}

	protected override void onGUIAttached() {
		abilityGUI.setCount (character.backpack.Get_Javelin());
		abilityGUI.showCount ();
		character.backpack.AddObserver (this);
	}

	#region Observer implementation

	public void javelinCountChanged (int numJavelins)
	{
		abilityGUI.setCount (numJavelins);
	}

	#endregion
}
