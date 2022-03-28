using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    public bool IsUpMoveKey { get; private set; }

    public bool IsDownMoveKey { get; private set; }

    public bool IsRightMoveKey { get; private set; }

    public bool IsLeftMoveKey { get; private set; }

    public bool IsShieldKey { get; private set; }

    private void Update()
    {
        IsUpMoveKey = Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W);

        IsDownMoveKey = Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S);

        IsRightMoveKey = Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.D);

        IsLeftMoveKey = Input.GetKey(KeyCode.LeftArrow) | Input.GetKey(KeyCode.A);

        IsShieldKey = Input.GetKey(KeyCode.LeftShift);
    }
}