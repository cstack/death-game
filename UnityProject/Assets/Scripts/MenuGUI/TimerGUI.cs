using UnityEngine;
using System.Collections;

public class TimerGUI : MonoBehaviour {

    public UILabel timer_label;

    private Transform poi;
    private TimerControl timer_control;

    void Start() {
        poi = GameObject.Find("Player").transform;
        timer_control = poi.GetComponent<TimerControl>();
    }

    void Update() {
        int seconds = (int)timer_control.read_time();
        timer_label.text = string.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
    }
}
