using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    public StairCtrl[] stairs;
    private int stairIndex = 0;

    private Vector2 topPosition = new Vector2 (0, -0.5f);

    private void Start()
    {
        foreach(var stair in stairs)
        {
            topPosition = stair.Respawn(topPosition);
        }
    }

    public void RespawnStair()
    {
        topPosition = stairs[stairIndex++].Respawn(topPosition);
        if(stairIndex > stairs.Length - 1)
        {
            stairIndex = 0;
        }
    }
}
