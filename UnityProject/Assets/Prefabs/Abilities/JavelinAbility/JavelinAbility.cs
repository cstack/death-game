using UnityEngine;
using System.Collections;

public class JavelinAbility : ActiveAbility, Backpack.Observer {

	public GameObject javelin_prefab;

	protected override void OnActivate () {
		if(player.backpack.Get_Javelin() > 0){

			GameObject new_javelin;

			int aim;
			if(player.dir == EntityBase.Direction.Left){
				new_javelin = (GameObject)Instantiate(javelin_prefab,
				                                      player.transform.position + new Vector3(1, 2f, 0),
				                                      transform.rotation);
				aim = (int) GlobalConstant.direction.left;
			}
			else{
				new_javelin = (GameObject)Instantiate(javelin_prefab,
				                                      player.transform.position + new Vector3(-1, 2f, 0),
				                                      transform.rotation);
				new_javelin.transform.FindChild("Sprite").transform.Rotate(0, 0, 180);
				aim = (int) GlobalConstant.direction.right;
			}

			new_javelin.GetComponent<JavelinControl>().Create_Javelin(player.gameObject, aim);
			new_javelin.GetComponent<JavelinControl>().friendly = true; // change to try to fix javelin going through ground
			player.backpack.remove_jevelin(1);

			//Change this to be called after the animation finishes, if implemented
			player.gameObject.GetComponent<Player> ().AbilityAnimationFinished ();
		}
	}

	protected override void onGUIAttached() {
		abilityGUI.setCount (player.backpack.Get_Javelin());
		abilityGUI.showCount ();
		player.backpack.AddObserver (this);
	}

	#region Observer implementation

	public void javelinCountChanged (int numJavelins)
	{
		abilityGUI.setCount (numJavelins);
	}

	#endregion
}
