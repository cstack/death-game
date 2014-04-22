using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public static class DataLogging {
	
	public static bool enabled = true;


	public static void Init() {

	}

	public static void TrackAbilityUsed(Ability ability, Vector3 locationUsed) {
		if (enabled) {

			// Parse
			Dictionary<string, string> args = new Dictionary<string, string>() {
				{"name", ability.abilityName},
				{"location", locationUsed.ToString()},
				{"level", Application.loadedLevelName}
			};
			ParseAnalytics.TrackEventAsync("AbilityUsed", args);

			// GameAnalytics
			GA.API.Design.NewEvent("UsedAbility_"+ability.abilityName, locationUsed);
		}
	}

	public static void TrackPlayerDeath(Ability abilityDiedTo, float lifeTime, Vector3 deathLocation) {
		if (enabled) {
			// Parse
			Dictionary<string, string> args = new Dictionary<string, string>() {
				{"diedToAbility", abilityDiedTo != null ? abilityDiedTo.abilityName : "No ability"},
				{"lifeTime", lifeTime.ToString()},
				{"location", deathLocation.ToString()},
				{"level", Application.loadedLevelName}
			};
			ParseAnalytics.TrackEventAsync("PlayerDeath", args);

			// GameAnalytics
			GA.API.Design.NewEvent("Death: "+abilityDiedTo.abilityName, deathLocation);
			GA.API.Design.NewEvent("Life Time", lifeTime);
		}
	}

	public static void TrackLevelCompleted(float time) {
		GA.API.Design.NewEvent("Level Complete", time);
	}

	public static void TrackKilledEnemy(EnemyBase enemyScript, Ability abilityUsed, Vector3 deathLocation) {
		GA.API.Design.NewEvent("Enemy Died: " + enemyScript.name, deathLocation);
		GA.API.Design.NewEvent("Enemy Killed By: " + abilityUsed.abilityName, deathLocation);
		GA.API.Design.NewEvent("Enemy Died: " + enemyScript.name + ", Killed By: " + abilityUsed.abilityName, deathLocation) ;
	}
}