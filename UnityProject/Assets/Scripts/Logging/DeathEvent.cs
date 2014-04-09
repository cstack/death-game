using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

[ParseClassName("DeathEvent")]
public class DeathEvent : ParseObject {

	[ParseFieldName("WorldPosition")]
	public string WorldPosition
	{
		get { return GetProperty<string>("WorldPosition"); }
		set { SetProperty<string>(value, "WorldPosition"); }
	}

	[ParseFieldName("LifeTime")]
	public int LifeTime
	{
		get { return GetProperty<int>("LifeTime"); }
		set { SetProperty<int>(value, "LifeTime"); }
	}
}
