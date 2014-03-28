using UnityEngine;
using System.Collections;

public class JavelinControl : MonoBehaviour {

    public float speed;
    private GameObject thrower;
    private int aim;
    private bool launch = true;
    private bool flying = false;

    public void Create_Javelin(GameObject _thrower, int direction)
    {
        thrower = _thrower;
        aim = direction;
    }

    // for testing
    void Start() {
        thrower = GameObject.FindWithTag("Player");
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
                transform.right = Vector3.Slerp(transform.right, rigidbody2D.velocity.normalized, 1.3f * Time.deltaTime);
            }
            else if (aim == (int)GlobalConstant.direction.right)
            {
                transform.right = Vector3.Slerp(transform.right, rigidbody2D.velocity.normalized, 1.3f * Time.deltaTime);
            }
        }
	}


    // if javelin is attacking, damage player then drop down into not attacking mode
    // 
    // if javelin is not attacking, and player is colliding with it, then it is picked
    // up by player
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && flying)
        {
            // freeze the javelin in place
            rigidbody2D.fixedAngle = true;
			flying = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
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
                    // damage other player, if there is PvP
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
			if(flying){
				flying = false;
			}
			else {
				// deal damage
				//collision.transform.GetComponent<Health>().Change_Color();
			}
        }
    }
}
