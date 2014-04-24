using UnityEngine;
using System.Collections;

public class ProjectileBase : EntityBase {
	private bool _friendly;
	public bool friendly {
		set {
			_friendly = value;
			if (_friendly) {
				gameObject.layer = LayerMask.NameToLayer(GlobalConstant.Layer.PlayerProjectile);
			} else {
				gameObject.layer = LayerMask.NameToLayer(GlobalConstant.Layer.EnemyProjectile);
			}
		}
		get {
			return _friendly;
		}
	}
}


