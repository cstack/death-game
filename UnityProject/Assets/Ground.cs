using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.SetTextureScale ("_MainTex", new Vector2(transform.localScale.x, 1f));

	}
}
