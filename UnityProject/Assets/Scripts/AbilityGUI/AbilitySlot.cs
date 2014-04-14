using UnityEngine;
using System.Collections;

public class AbilitySlot : MonoBehaviour {
	private UILabel countLabel;
    private float cooldown_time;
    private UISprite cooldown_sprite;

    private float test_timer;

	public void Awake() {
		countLabel = transform.FindChild ("Count").GetComponent<UILabel>();
        cooldown_sprite = transform.FindChild("CooldownBlock").GetComponent<UISprite>();
	}

    void Update() {

    }

	public void setCount(int count) {
		countLabel.text = count.ToString ();
	}

	public void showCount() {
		showCount (true);
	}

	public void showCount(bool show) {
		countLabel.gameObject.SetActive (show);
	}

    public void initializeCooldownTime(float time) {
        cooldown_time = time;
        cooldown_sprite.fillAmount = 0;
    }

    public void updateCooldownSprite(float time) {
        cooldown_sprite.fillAmount = time / cooldown_time;
        //Debug.Log(cooldown_sprite.fillAmount);
    }

    private void testRun() {
        if (test_timer <= 0)
        {
            test_timer = cooldown_time;
        }
        else
        {
            test_timer -= Time.deltaTime;
        }

        updateCooldownSprite(test_timer);
    }
}
