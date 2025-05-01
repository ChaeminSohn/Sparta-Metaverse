using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlatformControlStrategy : IControlStrategy
{
    public void OnJump(IControlHandler handler)
    {
        
    }

    public void OnLook(Vector2 input, IControlHandler handler)
    {   
        handler.Rotate(input);
    }

    public void OnMove(Vector2 input, IControlHandler handler)
    {
        handler.Move(input);
    }

}
