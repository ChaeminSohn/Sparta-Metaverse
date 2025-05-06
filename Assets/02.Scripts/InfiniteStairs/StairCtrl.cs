using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StairState
{
    Start,
    Right,
    Left
}
public class StairCtrl : MonoBehaviour
{
    public StairState state;
    private float offsetX = 1.0f;
    private float offsetY = 0.75f;

    public Vector2 Respawn(Vector2 topPosition)
    {
        if (state == StairState.Start)
        {
            state = StairState.Right;
        }
        else
        {
            float ran = Random.Range(0, 2);

            state = (ran < 1) ? StairState.Left : StairState.Right;
        }

        Vector2 offset;
        if(state == StairState.Left)
        {
            offset = new Vector2(-offsetX, offsetY);
        }   
        else
        {
            offset = new Vector2(offsetX, offsetY);
        }
        transform.position = topPosition + offset;
        return transform.position;
    }
}
