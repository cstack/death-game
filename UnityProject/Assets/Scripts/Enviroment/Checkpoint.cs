﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public GameObject spawnpoint;

	void OnTriggerEnter2D () {
		spawnpoint.transform.position = transform.position;
	}
}