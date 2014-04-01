using UnityEngine;
using System.Collections;

public class AbilitySlot : MonoBehaviour {
	private UILabel countLabel;

	public void Start() {
		countLabel = transform.FindChild ("Count").GetComponent<UILabel>();
	}

	public void setCount(int count) {
		countLabel.text = count.ToString ();
	}

	public void showCount() {
		countLabel.gameObject.SetActive (true);
	}
}
