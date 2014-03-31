using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public string abilityName;
	public Texture2D abilityIcon;

	private CharacterBase _character;
	protected Animator am;
	public CharacterBase character {
		set {
			_character = value;
			if (_character != null) {
				am = _character.GetComponent<Animator>();
				onAttachedToCharacter();
			} else {
				am = null;
			}
		}
		get {
			return _character;
		}
	}

	protected virtual void Awake () {
		abilityName = "Unknown";
	}

	protected virtual void Start () {
		abilityName = "Unknown";
	}

	protected virtual void onAttachedToCharacter() {}

	protected virtual void Update () {}

	protected virtual void OnActivate () {}

	protected virtual void OnHit () {}

	protected virtual void OnFinish () {}

	public void Activate () {
		if (character == null) 
			Debug.LogWarning("Character is not set in ability " + abilityName);
		else {
			OnActivate();
		}
	}

	public void Hit () {
		if (character == null) 
			Debug.LogWarning("Character is not set in ability " + abilityName);
		else 
			OnHit();
	}

	public void Finish () {
		if (character == null) 
			Debug.LogWarning("Character is not set in ability " + abilityName);
		else 
			OnFinish();
	}	
}
