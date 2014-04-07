using UnityEngine;
using System.Collections;

public class LivesGUI : MonoBehaviour {
    public UILabel lives_label;

    private Transform poi;
    private PlayerHealth player_health;

    void Start()
    {
        poi = GameObject.Find("Player").transform;
        player_health = poi.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        lives_label.text = "X " + player_health.current_life_count;
    }
}
