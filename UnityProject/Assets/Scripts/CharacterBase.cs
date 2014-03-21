using UnityEngine;
using System.Collections;

public abstract class CharacterBase : StateMachineBase {
	public enum Direction {
		Left, Right
	}
	private Direction _dir;
	public Direction dir {
		get {
			return _dir;
		}
		set {
			Direction oldDirection = _dir;
			_dir = value;
			if ((_dir == Direction.Left && oldDirection == Direction.Right) ||
			    (_dir == Direction.Right && oldDirection == Direction.Left)) {
				FlipSprite();
			}
		}
	}
	public bool slowingDown;

	protected Transform sprite;

	public void Start() {
		sprite = transform.Find("Sprite");
	}

	protected void FlipSprite() {
		// Flip the sprite over the anchor point
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	protected void updateXVelocity(float x) {
		updateXVelocity(rigidbody2D, x);
	}

	protected void updateXVelocity(Rigidbody2D obj, float x) {
		Vector2 vel = obj.velocity;
		vel.x = x;
		obj.velocity = vel;
	}
	
	protected void updateYVelocity(float y) {
		Vector2 vel = rigidbody2D.velocity;
		vel.y = y;
		rigidbody2D.velocity = vel;
	}

	protected int forwardRaycast(RaycastHit2D[] hits, float range) {
		float delta = 0.1f;
		Vector3 origin = transform.position + new Vector3 (0, 0.5f, 0);
		if (dir == Direction.Right) {
			origin += new Vector3(1, 0, 0);
		}
		origin += delta * Vector3.right * (dir == Direction.Right ? 1 : -1);
		return Physics2D.RaycastNonAlloc (origin, rigidbody2D.velocity, hits, range);
	}

	protected IEnumerator SlowDown(float seconds) {
		yield return StartCoroutine(SlowDown(this.rigidbody2D, seconds));
	}

	protected IEnumerator SlowDown(Rigidbody2D obj, float seconds) {
		slowingDown = true;
		float velX = obj.velocity.x;
		int iterations = 20;
		float delay = seconds/iterations;
		for (int i = 0; i < iterations; i++) {
			if (obj == null) continue;
			velX *= 0.7f;
			updateXVelocity(obj, velX);
			yield return new WaitForSeconds(delay);
		}

		if (obj != null) {
			updateXVelocity(obj, 0f);
		}
		slowingDown = false;
	}
}
