using UnityEngine;
using System.Collections;

// Mingrui
// just attach this script to any enemy, the OnDestroy function does all the work

public class DeathBonus : MonoBehaviour {
    public int bonus_seconds;

    private BonusAnnouncer bonus_announcer;

    private Player poi;
    private TimerControl timer_control;

    void Start() {
        poi = (Player)GameObject.Find("Player").GetComponent<Player>();
        timer_control = poi.GetComponent<TimerControl>();
        bonus_announcer = (BonusAnnouncer)GameObject.Find("Bonus Announcer").GetComponent<BonusAnnouncer>();
    }

    void OnDestroy() {
        Time_Bonus(bonus_seconds);
    }

    private void Time_Bonus(int extra_seconds) {
        timer_control.increase_time(extra_seconds);
        // tell bonus announcer to announce the time bonus for player to feel better
        // about themselves
        bonus_announcer.Announce_Bonus(string.Format("{0:00}:{1:00}", extra_seconds / 60, extra_seconds % 60));
    }
}
