using UnityEngine;
using System.Collections;

public class BonusAnnouncer : MonoBehaviour {

    public UILabel bonus_label;

    private float msg_duration = 1;
    private float timer = 0;

    void Start() {
        bonus_label.enabled = false;
    }

	// Update is called once per frame
	void Update () {
	    // period the bonus message is shown
        if(bonus_label.enabled){
            timer += Time.deltaTime;
            if(timer > msg_duration){
                bonus_label.enabled = false;
                timer = 0;
            }
        }
	}

    public void Announce_Bonus(string msg) {
        bonus_label.text = "+ " + msg;
        bonus_label.enabled = true;
    }
}
