using UnityEngine;
using System.Collections;

public class JavelinControl : MonoBehaviour {

    public float speed;
	public JavelinAbility javelinAbility;

	private float start_time = 0f;
	private float alive_duration = 5f;

	private bool _friendly;
	public bool friendly {
		set {
			_friendly = value;
			if (_friendly) {
				gameObject.layer = 11;
			} else {
				gameObject.layer = 10;
			}
		}
		get {
			return _friendly;
		}
	}

    public GameObject thrower;
    private int aim;
    private bool launch = true;
    private bool flying = false;

    public void Create_Javelin(GameObject _thrower, int direction)
    {
		//Debug.Log ("Create Javelin " + _thrower);
        thrower = _thrower;
        aim = direction;
    }

	void Update () {
        if (launch) // initial throw
        {
			launch = false;
            flying = true;
            if (aim == (int)GlobalConstant.direction.up)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + 2,
                    0);
                transform.rigidbody2D.velocity = new Vector2(
                    thrower.transform.rigidbody2D.velocity.x,
                    thrower.transform.rigidbody2D.velocity.y + speed
                    );
            }
            else if (aim == (int)GlobalConstant.direction.down)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y - 2,
                    0);
                transform.rigidbody2D.velocity = new Vector2(
                    thrower.transform.rigidbody2D.velocity.x,
                    thrower.transform.rigidbody2D.velocity.y - speed
                    );
            }
            else if (aim == (int)GlobalConstant.direction.left)
            {
                transform.position = new Vector3(
                    transform.position.x - 1,
                    transform.position.y + 2,
                    0);
                transform.Rotate(0, 0, 45);
                transform.rigidbody2D.velocity = new Vector2(
                    thrower.transform.rigidbody2D.velocity.x - speed / 2,
                    thrower.transform.rigidbody2D.velocity.y + speed / 2
                    );
            }
            else if (aim == (int)GlobalConstant.direction.right)
            {
                transform.position = new Vector3(
                    transform.position.x + 1,
                    transform.position.y + 2,
                    0);
                transform.Rotate(0, 0, 495);
                transform.rigidbody2D.velocity = new Vector2(
                    thrower.transform.rigidbody2D.velocity.x + speed / 2,
                    thrower.transform.rigidbody2D.velocity.y + speed / 2
                    );
            }
        }
        else // flying
        {
            if (aim == (int)GlobalConstant.direction.up)
            {
                transform.up = Vector3.Slerp(transform.up, rigidbody2D.velocity.normalized, 10 * Time.deltaTime);
            }
            else if (aim == (int)GlobalConstant.direction.down)
            {
                transform.up = Vector3.Slerp(transform.up, rigidbody2D.velocity.normalized, 10 * Time.deltaTime);
            }
            else if (aim == (int)GlobalConstant.direction.left)
            {
                transform.right = Vector3.Slerp(transform.right, rigidbody2D.velocity.normalized, 0.5f * Time.deltaTime);
            }
            else if (aim == (int)GlobalConstant.direction.right)
            {
                transform.right = Vector3.Slerp(transform.right, rigidbody2D.velocity.normalized, 0.5f * Time.deltaTime);
            }
        }
	}


    // if javelin is attacking, damage player then drop down into not attacking mode
    // 
    // if javelin is not attacking, and player is colliding with it, then it is picked
    // up by player
    void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag == "ground" && flying)
        {
            // freeze the javelin in place
            rigidbody2D.fixedAngle = true;
			flying = false;
			friendly = true;
			gameObject.layer = 12;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
			if (!friendly) {
				Destroy(gameObject);
				PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
				player.decreaseHealth(50, javelinAbility);
				return;
			}

            if (flying)
            {
				flying = false;
                if (collision.transform == thrower.transform)
                {
                    // arrow cannot hurt thrower that launched it
                    return;
                }
                else
                {
					PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
					player.decreaseHealth(50, javelinAbility);
                }
            }
            else
            {
                // collect javelin if it is not moving
                //collision.transform.GetComponent<PlayerTitanAttack>().arrow_count++;
                Backpack pack = collision.transform.GetComponent<Backpack>();
                if (pack)
                {
                    pack.add_javelin(1);
                }
                Destroy(gameObject);
            }
        }

        // if collided with Enemy and it is not "frozen" on ground
        if (collision.transform.tag == "Enemy" && flying)
		{
			Destroy(gameObject);
			Destroy(collision.gameObject);
        }
    }

	// for testing
	public bool Check_If_Flying(){
		return flying;
	}
}
