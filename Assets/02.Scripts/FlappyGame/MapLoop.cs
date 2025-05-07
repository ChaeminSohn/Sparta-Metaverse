using UnityEngine;

public class MapLoop : MonoBehaviour
{
    private Obstacle obstacle;

    private float mapWidth = 8f;

    private void Start()
    {
        obstacle = GetComponentInChildren<Obstacle>();
    }

    private void Update()
    {
        if(transform.position.x < -mapWidth * 2)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        transform.position = new Vector2(mapWidth * 2, 0);
        obstacle.SetRandomPosition();
    }
}
