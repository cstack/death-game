using UnityEngine;
using System.Collections;

public abstract class PowerBase : ActiveAbility {

	public string name;

	private bool m_setActive;
	public bool setActive {
		set {m_setActive = value;}
	}

	private bool m_canActivate;
	public bool canActivate {
		get {return m_canActivate;}
	}

	private bool m_isActive;
	public bool isActive {
		get {return m_isActive;}
	}

	private float m_durationTimer;
	public float durationTimer {
		get {return m_durationTimer;}
	}
	private float m_durationLength;
	public float durationLength {
		set {
			m_durationLength = value;
			m_durationTimer = value;
		}
	}

	private float m_cooldownTimer;
	public float cooldownTimer {
		get {return m_cooldownTimer;}
	}
	private float m_cooldownLength;
	public float cooldownLength {
		set {
			m_cooldownLength = value;
			m_cooldownTimer = value;
		}
	}
	
	public PowerBase() {
		m_cooldownTimer = 0;
		m_durationTimer = 0;
	}

	public abstract void Activate();

	public abstract void Deactivate();

	public override void use() {
		m_setActive = true;
		m_durationTimer = m_durationLength;
	}
	void Update() {
		/* Update Timers */
		if (m_cooldownTimer > 0)
			m_cooldownTimer -= Time.deltaTime;

		if (m_durationTimer > 0)
			m_durationTimer -= Time.deltaTime;

		/* Update method checks */
		if (m_cooldownTimer <= 0)
			m_canActivate = true;
		else
			m_canActivate = false;

		if (m_durationTimer <= 0) {
			m_isActive = false;
		}
		else
			m_isActive = true;

		if (m_canActivate && m_setActive)
			Activate();
	}
}
