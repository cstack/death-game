using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Used to Pass Information Between Scenes (and To Server? - Heat Maps)

public class GameInfo : MonoBehaviour {

	private int curLevel = 0;

	public List<string> levelStrings = new List<string>()
	{
		"Main",
		"Jake2"
	};

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public string nextLevel () {

		if (curLevel + 1 < levelStrings.Count) {
			return levelStrings[++curLevel];
		}

		return levelStrings [0];
	}
	
}
