using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public string abilityName;
	public Texture2D abilityIcon;

	public CharacterBase character;

	public Ability() { character = null; }
	public Ability(CharacterBase newCharacter) {
		character = newCharacter;
	}

	private bool isCharacterSet() {
		if (character == null) {
			Debug.LogWarning("Character is not set in ability " + abilityName);
			return false;
		}
		else
			return true;
	}
	
	protected virtual void Awake () {
		abilityName = "Unknown";
	}

	protected virtual void Start () {
		abilityName = "Unknown";
	}

	protected virtual void Update () {}

	protected virtual void OnActivate () {}

	public void Activate () {
		if (isCharacterSet())
			OnActivate();
	}


	
}
