using UnityEngine;
using System.Collections;

public class BonusAnnouncer : MonoBehaviour {

    public UILabel bonus_label;
	public float default_messsage_duration = 1f;

	private float message_duration;
    private float timer = 0;

    void Start() {
        bonus_label.enabled = false;
    }

	// Update is called once per frame
	void Update () {
	    // period the bonus message is shown
        if(bonus_label.enabled){
            timer += Time.deltaTime;
            if(timer > message_duration){
                bonus_label.enabled = false;
                timer = 0;
            }
        }
	}

    public void Announce_Bonus(string msg) {
		Announce_Bonus (msg, default_messsage_duration);
    }

	public void Announce_Bonus(string msg, float seconds) {
		if (bonus_label != null) {
			bonus_label.text = msg;
			bonus_label.enabled = true;
			message_duration = seconds;
		}
	}
}
