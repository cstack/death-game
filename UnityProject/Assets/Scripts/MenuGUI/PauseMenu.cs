using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject pause_ui;
    private bool is_paused =false;

	void Start() {
		if (pause_ui != null) {
        	pause_ui.SetActive(false);
		}
    }

    void Update () {
        detect_pause();
	}

    void detect_pause() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(is_paused) {
                is_paused = false;
                Time.timeScale = 1;
                pause_ui.SetActive(false);
            }
            else {
                is_paused = true;
                Time.timeScale = 0;
                pause_ui.SetActive(true);
            }
        }
    }
}
