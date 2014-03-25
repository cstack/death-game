using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public string abilityName;
	public Texture2D abilityIcon;

	public CharacterBase character;
	
	protected virtual void Awake () {
		abilityName = "Unknown";
	}

	protected virtual void Start () {
		abilityName = "Unknown";
	}

	protected virtual void Update () {}

	protected virtual void OnActivate () {}

	public void Activate () {
		if (character == null) 
			Debug.LogWarning("Character is not set in ability " + abilityName);
		else 
			OnActivate();
	}			
}
