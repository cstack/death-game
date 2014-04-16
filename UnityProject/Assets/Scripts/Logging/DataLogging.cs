using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public static class DataLogging {
	
	public static bool enabled = true;
	
	public static ParseObject gameSession;
	public static ParseObject playerDeaths;
	public static ParseObject abilitiesUsed;

	public static void Init() {
		gameSession = new ParseObject("GameSession");
	}

	public static void TrackAbilityUsed(Ability ability, Vector3 locationUsed) {
		if (enabled) {
			Dictionary<string, string> args = new Dictionary<string, string>() {
				{"name", ability.abilityName},
				{"location", locationUsed.ToString()},
				{"level", Application.loadedLevelName}
			};
			ParseAnalytics.TrackEventAsync("AbilityUsed", args);
		}
	}

	public static void TrackPlayerDeath(Ability abilityDiedTo, float lifeTime, Vector3 deathLocation) {
		if (enabled) {
			Dictionary<string, string> args = new Dictionary<string, string>() {
				{"diedToAbility", abilityDiedTo.abilityName},
				{"lifeTime", lifeTime.ToString()},
				{"location", deathLocation.ToString()},
				{"level", Application.loadedLevelName}
			};
			ParseAnalytics.TrackEventAsync("PlayerDeath", args);
		}
	}
}