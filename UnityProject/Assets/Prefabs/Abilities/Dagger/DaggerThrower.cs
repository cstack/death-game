using UnityEngine;
using System.Collections;

public class DaggerThrower : MonoBehaviour {

    public ProjectileBase projectile;
    public GameObject head;
    public GameObject tail;
    public float throw_speed;
	public bool friendly;

    private float dir;

    void Start() { 
        // remember initial facing direction
        dir = head.transform.position.x - tail.transform.position.x;
    }

    public void Throw_Projectile() {
        ProjectileBase duplicate = (ProjectileBase) Instantiate(projectile, transform.position, transform.rotation);
        if(Same_Direction()){
            duplicate.transform.RotateAround(Vector3.up, Mathf.PI);
        }
		duplicate.friendly = friendly;
        duplicate.gameObject.SetActive(true);
        duplicate.rigidbody2D.velocity = (head.transform.position - tail.transform.position) * throw_speed;
    }

    private bool Same_Direction() {
        if (dir > 0 && head.transform.position.x - tail.transform.position.x > 0)
        {
            return true;
        }
        else if (dir < 0 && head.transform.position.x - tail.transform.position.x < 0)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
