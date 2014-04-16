using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

[ParseClassName("GameSession")]
public class GameSession : ParseObject {
	
	[ParseFieldName("levels")]
	public Dictionary<string, LevelSession> levels {
		get { return GetProperty<Dictionary<string, LevelSession>>("levels"); }
		set { SetProperty<Dictionary<string, LevelSession>>(value, "levels"); }
	}
}

[ParseClassName("LevelSession")]
public class LevelSession : ParseObject {
	
	[ParseFieldName("playerDeaths")]
	public List<PlayerDeath> playerDeaths {
		get { return GetProperty<List<PlayerDeath>>("playerDeaths"); }
		set { SetProperty<List<PlayerDeath>>(value, "playerDeaths"); }
	}

	[ParseFieldName("abilitiesUsed")]
	public List<AbilityUsed> abilitiesUsed {
		get { return GetProperty<List<AbilityUsed>>("abilitiesUsed"); }
		set { SetProperty<List<AbilityUsed>>(value, "abilitiesUsed"); }
	}
}

[ParseClassName("PlayerDeath")]
public class PlayerDeath : ParseObject {
	
	[ParseFieldName("ability")]
	public string ability {
		get { return GetProperty<string>("ability"); }
		set { SetProperty<string>(value, "ability"); }
	}
	
	[ParseFieldName("lifeTime")]
	public float lifeTime {
		get { return GetProperty<float>("lifeTime"); }
		set { SetProperty<float>(value, "lifeTime"); }
	}
	
	[ParseFieldName("location")]
	public string location {
		get { return GetProperty<string>("location"); }
		set { SetProperty<string>(value, "location"); }
	}
	
	[ParseFieldName("level")]
	public string level {
		get { return GetProperty<string>("level"); }
		set { SetProperty<string>(value, "level"); }
	}
}

[ParseClassName("AbilityUsed")]
public class AbilityUsed : ParseObject {
	
	[ParseFieldName("name")]
	public string ability {
		get { return GetProperty<string>("name"); }
		set { SetProperty<string>(value, "name"); }
	}

	[ParseFieldName("count")]
	public int count {
		get { return GetProperty<int>("count"); }
		set { SetProperty<int>(value, "count"); }
	}

	[ParseFieldName("location")]
	public string location {
		get { return GetProperty<string>("location"); }
		set { SetProperty<string>(value, "location"); }
	}

	[ParseFieldName("level")]
	public string level {
		get { return GetProperty<string>("level"); }
		set { SetProperty<string>(value, "level"); }
	}
}

