using UnityEngine;
using System.Collections;

public class BreathBarControl : MonoBehaviour {
    private GameObject bar_right;
    private GameObject bar_left; 
    private Player player;

    void Awake() {
        player = GameObject.Find("Player").GetComponent<Player>();
        bar_right = GameObject.Find("GUI Breath Bar Facing Right");
        bar_left = GameObject.Find("GUI Breath Bar Facing Left");
    }

    void Update() {
        if (player.headUnderwater)
        {
            if (player.getFacingDir() == -1)
            {
                bar_right.SetActive(false);
                bar_left.SetActive(true);
            }
            else {
                bar_right.SetActive(true);
                bar_left.SetActive(false);
            }
        }
        else {
            bar_right.SetActive(false);
            bar_left.SetActive(false);
        }
    }
    
}
