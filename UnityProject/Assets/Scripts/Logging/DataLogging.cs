using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class DataLogging : MonoBehaviour {
	
	void Start () {
		ParseObject.RegisterSubclass<DeathEvent>();
	}

	void Update () {
	
	}
}
