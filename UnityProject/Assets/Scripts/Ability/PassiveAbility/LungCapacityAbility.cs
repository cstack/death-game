using UnityEngine;
using System.Collections;

public class LungCapacityAbility : ActiveAbility {
	public float healthRegain = 50f;

	public override void onAttachedToCharacter ()
	{
		base.onAttachedToCharacter ();
		player.canBreathUnderwater = true;
	}

	public override void onRemovedFromCharacter ()
	{
		base.onRemovedFromCharacter ();
		player.canBreathUnderwater = false;
	}

	protected override void OnActivate () {
		BonusAnnouncer bonus_announcer = (BonusAnnouncer)GameObject.Find("Bonus Announcer").GetComponent<BonusAnnouncer>();
		bonus_announcer.Announce_Bonus(string.Format("+ {0} hp", healthRegain));
		player.GetComponent<PlayerHealth> ().increaseHealth (healthRegain);
	}
}
