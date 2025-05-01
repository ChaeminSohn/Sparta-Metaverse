using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControlStrategy
{
    void OnMove(Vector2 input, IControlHandler handler);
    void OnLook(Vector2 input, IControlHandler handler);
    void OnJump(IControlHandler handler);
}
