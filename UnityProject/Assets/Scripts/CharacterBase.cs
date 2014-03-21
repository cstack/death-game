using UnityEngine;
using System.Collections;

public abstract class CharacterBase : MonoBehaviour {
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
}
