using UnityEngine;
using System.Collections;

public class DaggerThrower : MonoBehaviour {

    public GameObject projectile;
    public GameObject head;
    public GameObject tail;
    public float throw_speed;

    private float dir;

    void Start() { 
        // remember initial facing direction
        dir = head.transform.position.x - tail.transform.position.x;
    }

    public void Throw_Projectile() {
        GameObject duplicate = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        if(Same_Direction()){
            duplicate.transform.RotateAround(Vector3.up, Mathf.PI);
        }
        duplicate.SetActive(true);
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
