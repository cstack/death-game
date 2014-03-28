using UnityEngine;
using System.Collections;

public class GlobalConstant : MonoBehaviour {
    [HideInInspector]
    public KeyCode keycode_up = KeyCode.W;
    [HideInInspector]
    public KeyCode keycode_down = KeyCode.S;
    [HideInInspector]
    public KeyCode keycode_left = KeyCode.A;
    [HideInInspector]
    public KeyCode keycode_right = KeyCode.D;

    [HideInInspector]
    public KeyCode keycode_arrow = KeyCode.K;
    [HideInInspector]
    public KeyCode keycode_jump = KeyCode.J;
    [HideInInspector]
    public KeyCode keycode_duplicate = KeyCode.I;

    [HideInInspector]
    public enum direction
    {
        up, down, left, right
    }
}
