using UnityEngine;
using System.Collections;

public class GlobalConstant : MonoBehaviour {
    [HideInInspector]
    public KeyCode keycode_up = KeyCode.UpArrow;
    [HideInInspector]
    public KeyCode keycode_down = KeyCode.DownArrow;
    [HideInInspector]
    public KeyCode keycode_left = KeyCode.LeftArrow;
    [HideInInspector]
    public KeyCode keycode_right = KeyCode.RightArrow;

    [HideInInspector]
    public KeyCode keycode_ability_1 = KeyCode.Q;
    [HideInInspector]
    public KeyCode keycode_ability_2 = KeyCode.W;
    [HideInInspector]
    public KeyCode keycode_ability_3 = KeyCode.E;
	[HideInInspector]
	public KeyCode keycode_ability_4 = KeyCode.R;

    [HideInInspector]
    public enum direction
    {
        up, down, left, right
    }
}