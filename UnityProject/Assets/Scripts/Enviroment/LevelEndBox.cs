﻿using UnityEngine;
using System.Collections;

public class LevelEndBox : MonoBehaviour {
	public GameInfo ginfo;
	public GameObject gobject;

	void Start () {
		GameObject tmp = GameObject.Find ("GameInfo");

		if (tmp == null) {
			tmp = GameObject.Instantiate(gobject) as GameObject;
			tmp.name = "GameInfo";
		} 

		ginfo = tmp.GetComponent<GameInfo> ();
	}

	private IEnumerator OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Player> ().crouching = true;
			yield return new WaitForSeconds(5f);
			GameObject.Find("EndOfLevelSound").GetComponent<EndOfLevelSound>().FadeOut(5f);
			yield return new WaitForSeconds(5f);
			if (ginfo != null) {
				DataLogging.TrackLevelCompleted(ginfo.levelCompletionTime);
				Application.LoadLevel (ginfo.nextLevel());
			} else {
				Debug.Log ("No GameInfo GameObject Found, Restarting Current Level");
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
