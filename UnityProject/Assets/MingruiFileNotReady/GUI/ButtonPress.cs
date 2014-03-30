using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour {

    public KeyCode key;
    private UIButton button;

	// Use this for initialization
	void Start () {
        button = GetComponent<UIButton>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(key)){
            SendMessage("OnClick");
            Debug.Log(key + " pressed");
        }
	}
}
