using UnityEngine;
using System.Collections;

// Mingrui
// just attach this script to any enemy, the OnDestroy function does all the work

public class DeathBonus : MonoBehaviour {
    public int bonus_seconds;
    
    private Player poi;
    private TimerControl timer_control;

    void Start() {
        poi = (Player)GameObject.Find("Player").GetComponent<Player>();
        timer_control = poi.GetComponent<TimerControl>();
    }

    void OnDestroy() {
        Time_Bonus(bonus_seconds);
    }

    private void Time_Bonus(int extra_seconds) {
        timer_control.increase_time(extra_seconds);
    }
}
