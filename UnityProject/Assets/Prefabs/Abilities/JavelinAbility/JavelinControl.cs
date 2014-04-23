using UnityEngine;
using System.Collections;

public class JavelinControl : ProjectileBase {

    public float speed;
	public JavelinAbility javelinAbility;
	public float javelinDamage = 30f;

	private float alive_timer = 0f;
	public float alive_duration;

    public GameObject thrower;
    private int aim;
    private bool launch = true;
    private bool flying = false;
	private bool frozen = false;

    public void Create_Javelin(GameObject _thrower, int direction)
    {
		//Debug.Log ("Create Javelin " + _thrower);
        thrower = _thrower;
        aim = direction;
    }

	void Update () {
		ReturnToBag();

		if (frozen) {
			return;
		}

        if (launch && thrower != null) // initial throw
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
                transform.Rotate(0, 0, 135);
                transform.rigidbody2D.velocity = new Vector2(
                    thrower.transform.rigidbody2D.velocity.x + speed / 2,
                    thrower.transform.rigidbody2D.velocity.y + speed / 2
                    );
				Transform sprite = transform.FindChild("JavelinSprite");
				sprite.transform.Rotate(0, 0, -180);
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
                transform.right = Vector3.Slerp(transform.right, rigidbody2D.velocity.normalized, 0.8f * Time.deltaTime);
            }
            else if (aim == (int)GlobalConstant.direction.right)
            {
                transform.right = Vector3.Slerp(transform.right, rigidbody2D.velocity.normalized, 0.8f * Time.deltaTime);
            }
        }
	}

	void ReturnToBag(){
		alive_timer += Time.deltaTime;
		if(alive_timer > alive_duration){
			Debug.Log("destroy because alive time > alive duration");
			Destroy(gameObject);
		}
	}	

    // if javelin is attacking, damage player then drop down into not attacking mode
    // 
    // if javelin is not attacking, and player is colliding with it, then it is picked
    // up by player
    void OnCollisionEnter2D(Collision2D collision){
		if (frozen) {
			return;
		}

        if (collision.gameObject.tag == GlobalConstant.Tag.Ground)
        {
            // freeze the javelin in place
			frozen = true;
            rigidbody2D.fixedAngle = true;
			flying = false;
			friendly = true;
			gameObject.layer = LayerMask.NameToLayer("NeutralProjectile");
        }

        if (collision.gameObject.tag == GlobalConstant.Tag.Player && !friendly)
        {
			Destroy(gameObject);
			PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
			player.decreaseHealth(javelinDamage, javelinAbility);
			return;
        }

        // if collided with Enemy and it is not "frozen" on ground
        if (collision.transform.tag == GlobalConstant.Tag.Enemy && friendly)
		{
			Destroy(gameObject);
			collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1f);
        }
    }

	// for testing
	public bool Check_If_Flying(){
		return flying;
	}

	void OnDestroy(){
		if(thrower && thrower.GetComponent<Backpack>()){
			thrower.GetComponent<Backpack>().add_javelin(1);
		}
	}
}
