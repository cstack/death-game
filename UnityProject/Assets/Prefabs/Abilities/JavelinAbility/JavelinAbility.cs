using UnityEngine;
using System.Collections;

public class JavelinAbility : ActiveAbility, Backpack.Observer {

	public GameObject javelin_prefab;

	protected override void OnActivate () {
		player.ShootJavelins ();
	}

	protected override void onGUIAttached() {
		abilityGUI.setCount (player.backpack.Get_Javelin());
		//abilityGUI.showCount ();
		player.backpack.AddObserver (this);
	}

	#region Observer implementation

	public void javelinCountChanged (int numJavelins)
	{
		abilityGUI.setCount (numJavelins);
	}

	#endregion
}
