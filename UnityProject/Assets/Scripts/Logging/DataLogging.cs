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

			string abilityName = ability.abilityName;
			
			if (abilityName == null) {
				abilityName = "No ability";
			}

			// Parse
			Dictionary<string, string> args = new Dictionary<string, string>() {
				{"name", abilityName},
				{"location", locationUsed.ToString()},
				{"level", Application.loadedLevelName}
			};
			ParseAnalytics.TrackEventAsync("AbilityUsed", args);

			// GameAnalytics
			GA.API.Design.NewEvent("Ability Used:" + abilityName, locationUsed);
		}
	}

	public static void TrackPlayerDeath(Ability abilityDiedTo, float lifeTime, Vector3 deathLocation) {
		if (enabled) {
			string abilityName = abilityDiedTo != null ? abilityDiedTo.abilityName : "No Ability";

			// Parse
			Dictionary<string, string> args = new Dictionary<string, string>() {
				{"diedToAbility", abilityName},
				{"lifeTime", lifeTime.ToString()},
				{"location", deathLocation.ToString()},
				{"level", Application.loadedLevelName}
			};
			ParseAnalytics.TrackEventAsync("PlayerDeath", args);

			// GameAnalytics
			GA.API.Design.NewEvent("Player Death:Killed By" + abilityName, deathLocation);
			GA.API.Design.NewEvent("Player Death:Life Time", lifeTime);
		}
	}

	public static void TrackLevelCompleted(float time) {
		if (enabled) {
			GA.API.Design.NewEvent("Level Complete", time);
		}
	}

	public static void TrackEnemyDeath(EnemyBase enemyScript, Vector3 deathLocation) {
		if (enabled) {
			GA.API.Design.NewEvent("Enemy Death:Enemy:" + enemyScript.name, deathLocation);
		}
	}

	public static void TrackEnemyDeath(EnemyBase enemyScript, Ability ability, Vector3 deathLocation) {
		if (enabled) {
			string abilityName = ability.abilityName;
			
			if (abilityName == null) {
				abilityName = "No ability";
			}

			GA.API.Design.NewEvent("Enemy Death:Enemy:" + enemyScript.name + ":Killed By:" + abilityName, deathLocation);
		}
	}
}