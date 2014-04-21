using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ability : MonoBehaviour {

	public string abilityName;
	public Texture2D abilityIcon;
	public AudioClip abilityClip;

	// cooldown timer
	private float lastuse;
	public float cooldown;

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

	protected virtual void onAttachedToCharacter() {
		lastuse = 0;
	}

	protected virtual void Update () {}

	protected virtual void OnActivate () {}

	protected virtual void OnHit () {}

	protected virtual void OnFinish () {}

	public void Activate () {
		if (player == null) 
			Debug.LogWarning("Player is not set in ability " + abilityName);
		else {
			if(OffCooldown()){
				// refresh lastuse
				UpdateLastuse();
				OnActivate();
				SetAudioClip();
			}
			else {
				Debug.Log("Abilit still on cooldown!");
			}
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

	private bool OffCooldown(){
		if(Time.time - lastuse > cooldown){
			return true;
		}
		else {
			return false;
		}
	}

	private void UpdateLastuse(){
		lastuse = Time.time;
	}

	private void SetAudioClip() {
		if (abilityClip != null && player.audio != null) {
			if (!player.audio.enabled) {
				player.audio.enabled = true;
			}
			player.audio.loop = false;
			if (player.audio.clip != abilityClip) {
				player.audio.clip = abilityClip;
			}
			player.audio.Play();
		}
	}
}