using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlappyControlStrategy : IControlStrategy
{
    public void OnJump(IControlHandler handler)
    {
        handler.Jump();
    }

    public void OnLook(Vector2 input, IControlHandler handler)
    {
    }

    public void OnMove(Vector2 input, IControlHandler handler)
    {
    }
}
