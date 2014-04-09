using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour {
	public enum Direction {
		Right, Left, Up, Down
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
	private float riverSpeed = 0.0f;
	
	protected void FlipSprite() {
		// Flip the sprite over the anchor point
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	
	public void updateXVelocity(float x) {
		updateXVelocity(rigidbody2D, x);
	}
	
	public void updateXVelocity(Rigidbody2D obj, float x) {
		Vector2 vel = obj.velocity;
		vel.x = x + riverSpeed;
		obj.velocity = vel;
	}
	
	public void updateYVelocity(float y) {
		Vector2 vel = rigidbody2D.velocity;
		vel.y = y;
		rigidbody2D.velocity = vel;
	}

	//right is if river flows right or not
	public void enterRiver(float rvSpeed) {
		riverSpeed = rvSpeed;
	}

	public void exitRiver() {
		riverSpeed = 0f;
	}
}

