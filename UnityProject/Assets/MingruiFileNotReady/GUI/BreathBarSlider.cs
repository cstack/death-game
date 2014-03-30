using UnityEngine;
using System.Collections;

public class BreathBarSlider : MonoBehaviour
{

    public UISlider slider;
    private GameObject player;
    private PlayerHealth player_health;

    // Use this for initialization
    void Start()
    {
        slider = GetComponent<UISlider>();
        player = GameObject.Find("Player");
        player_health = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)player_health.currentBreath / (float)player_health.maxBreath;
    }
}