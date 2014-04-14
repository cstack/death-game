using UnityEngine;
using System.Collections;
using Parse;

public class Menu : MonoBehaviour {

    // mingrui
    // these will be even handlers
    // signals are sent from buttons using NGUI's native api
    public void OnPlayClicked() {
        //Debug.Log("Play button clicked");

        Application.LoadLevel("Main");
    }
}
