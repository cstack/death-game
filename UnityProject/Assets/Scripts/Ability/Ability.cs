using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ability : MonoBehaviour {

	public string abilityName;
	public Texture2D abilityIcon;
	public AudioClip abilityClip;

	private Player _player;
	protected Animator am;
	public Player player {
		set {
			_player = value;
			if (_player != null) {
				am = _player.GetComponent<Animator>();
				onAttachedToCharacter();
			} else {
				am = null;
			}
		}
		get {
			return _player;
		}
	}

	protected AbilitySlot abilityGUI;
	public void setAbilityGUI (AbilitySlot gui) {
		abilityGUI = gui;
		onGUIAttached ();
	}

	protected virtual void onGUIAttached () {
		abilityGUI.showCount (false);
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
		if (player == null) 
			Debug.LogWarning("Player is not set in ability " + abilityName);
		else {
			OnActivate();
		}
	}

	public void Hit () {
		if (player == null) 
			Debug.LogWarning("Player is not set in ability " + abilityName);
		else 
			OnHit();
	}

	public void Finish () {
		if (player == null) 
			Debug.LogWarning("Player is not set in ability " + abilityName);
		else 
			OnFinish();
	}	
}
