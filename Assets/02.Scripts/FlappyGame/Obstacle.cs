using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform topObject;
    public Transform bottomObject;

    private float highPosY = 1f;
    private float lowPosY = -1f;

    private float maxHoleSize = 16f;
    private float minHoleSize = 12f;

    private void Start()
    {
        SetRandomPosition();
    }
    public void SetRandomPosition()
    {
        float holeSize = Random.Range(minHoleSize, maxHoleSize);

        topObject.localPosition = new Vector2(0, holeSize/2);
        bottomObject.localPosition = new Vector2(0, -holeSize/2);

        Vector3 placePositon = Vector2.zero;
        placePositon.y = Random.Range(lowPosY, highPosY);

        transform.localPosition = placePositon;
    }
}
