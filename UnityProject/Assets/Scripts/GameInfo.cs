using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Used to Pass Information Between Scenes (and To Server? - Heat Maps)

public class GameInfo : MonoBehaviour {

	private int curLevel = 0;

	public float levelCompletionTime = 0f;

	public List<string> levelStrings = new List<string>()
	{
		"Main",
		"Jake2"
	};

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update() 
	{
		levelCompletionTime += Time.deltaTime;
	}

	public string nextLevel ()
	{
		levelCompletionTime = 0f;
		if (curLevel + 1 < levelStrings.Count) {
			return levelStrings[++curLevel];
		}

		return levelStrings [0];
	}
	
}
