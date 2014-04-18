using UnityEngine;
using System.Collections;

public class HealthBarUpdate : MonoBehaviour {

	private UISlider slider;
	private EnemyHealth enemyHealth;
	
	// Use this for initialization
	void Start()
	{
		slider = GetComponent<UISlider>();
		enemyHealth = transform.parent.parent.parent.gameObject.GetComponent<EnemyHealth> ();
	}
	
	// Update is called once per frame
	void Update()
	{
		slider.value = enemyHealth.getHP () / enemyHealth.maxHP;
	}
}
