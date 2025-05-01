using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControlHandler 
{
    void Move(Vector2 direction);
    void Rotate(Vector2 direction);
    void Jump();
}
