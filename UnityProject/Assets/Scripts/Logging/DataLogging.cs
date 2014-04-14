using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public static class DataLogging {
	
	public static bool enabled = true;
	
	public static ParseObject gameSession;
	public static ParseObject abilitiesUsed;
	public static ParseObject playerDeaths;
}