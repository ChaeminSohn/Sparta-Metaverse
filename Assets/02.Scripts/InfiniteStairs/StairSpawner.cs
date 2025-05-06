using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    public StairCtrl[] stairs;

    private Vector2 topPosition = new Vector2 (0, -0.5f);

    private void Start()
    {
        foreach(var stair in stairs)
        {
            topPosition = stair.Respawn(topPosition);
        }
    }

    public void RespawnStair(int index)
    {
        topPosition = stairs[index].Respawn(topPosition);
    }
}
