using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GUITexture pause_GUI;

    private bool is_paused =false;

    void Update () {
        detect_pause();
	}

    void detect_pause() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(is_paused) {
                is_paused = false;
            }
            else {
                is_paused = true;
            }
        }

        if(is_paused) {
            Time.timeScale = 0;
            pause_GUI.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            pause_GUI.enabled = false;
        }
    }
}
