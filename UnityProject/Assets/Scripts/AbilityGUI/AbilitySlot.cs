using UnityEngine;
using System.Collections;

public class AbilitySlot : MonoBehaviour {
	private UILabel countLabel;

	public void Awake() {
		countLabel = transform.FindChild ("Count").GetComponent<UILabel>();
	}

	public void setCount(int count) {
		countLabel.text = count.ToString ();
	}

	public void showCount() {
		showCount (true);
	}

	public void showCount(bool show) {
		countLabel.gameObject.SetActive (show);
	}
}
