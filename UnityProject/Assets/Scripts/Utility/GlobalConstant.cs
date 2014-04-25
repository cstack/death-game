using UnityEngine;
using System.Collections;

public class GlobalConstant : MonoBehaviour
{
    [HideInInspector]
    public static KeyCode keycode_up = KeyCode.UpArrow;
    [HideInInspector]
    public static KeyCode keycode_down = KeyCode.DownArrow;
    [HideInInspector]
    public static KeyCode keycode_left = KeyCode.LeftArrow;
    [HideInInspector]
    public static KeyCode keycode_right = KeyCode.RightArrow;


    [HideInInspector]
    public static KeyCode keycode_jump = KeyCode.Space;
    [HideInInspector]
    public static KeyCode controller_jump = KeyCode.Joystick1Button0;

    [HideInInspector]
    public static KeyCode controller_down= KeyCode.;

    [HideInInspector]
    public static KeyCode keycode_ability_1 = KeyCode.Q;
    [HideInInspector]
    public static KeyCode controller_ability_1 = KeyCode.Joystick1Button2;
    [HideInInspector]
    public static KeyCode keycode_ability_2 = KeyCode.W;
    [HideInInspector]
    public static KeyCode controller_ability_2 = KeyCode.Joystick1Button3;
    [HideInInspector]
    public static KeyCode keycode_ability_3 = KeyCode.E;
    [HideInInspector]
    public static KeyCode controller_ability_3 = KeyCode.Joystick1Button1;
    [HideInInspector]
    public static KeyCode keycode_ability_4 = KeyCode.R;
    [HideInInspector]
    public static KeyCode controller_ability_4 = KeyCode.Joystick1Button5;

    [HideInInspector]
    public enum direction
    {
        up, down, left, right
    }

    public enum AbilityAnimation
    {
        NotUsingAbility, MeleeAttack, DashAttack, RockAttack
    }

    public class Tag
    {
        public static string Player = "Player";
        public static string Enemy = "Enemy";
        public static string PlayerHead = "PlayerHead";
        public static string Ground = "ground";
    }

    public class Layer
    {
        public static string PlayerProjectile = "PlayerProjectile";
        public static string EnemyProjectile = "EnemyProjectile";
    }
}
