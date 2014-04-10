using UnityEngine;
using System.Collections;

// Mingrui
// just attach this script to any enemy, the OnDestroy function does all the work

public class DeathBonus : MonoBehaviour {
    public int bonus_seconds;
	private float bonus_health = 10f;

    private BonusAnnouncer bonus_announcer;

    private Player poi;
    private TimerControl timer_control;
	private PlayerHealth player_health;

    void Start() {
        poi = (Player)GameObject.Find("Player").GetComponent<Player>();
        timer_control = poi.GetComponent<TimerControl>();
		player_health = poi.GetComponent<PlayerHealth> ();
        bonus_announcer = (BonusAnnouncer)GameObject.Find("Bonus Announcer").GetComponent<BonusAnnouncer>();
    }

    void OnDestroy() {
		HealthBonus (bonus_health);
        //Time_Bonus(bonus_seconds);
    }

	private void HealthBonus(float extra_health) {
		if (player_health.currentHealth >= player_health.maxHealth) {
			return;
		}
		player_health.increaseHealth(extra_health);
		bonus_announcer.Announce_Bonus(string.Format("{0} hp", (int) extra_health));
	}

    private void Time_Bonus(int extra_seconds) {
        timer_control.increase_time(extra_seconds);
        // tell bonus announcer to announce the time bonus for player to feel better
        // about themselves
        bonus_announcer.Announce_Bonus(string.Format("{0:00}:{1:00}", extra_seconds / 60, extra_seconds % 60));
    }
}
