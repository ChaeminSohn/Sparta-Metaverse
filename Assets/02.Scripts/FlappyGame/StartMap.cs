using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMap : MonoBehaviour
{
    private float mapWidth = 8f;


    private void Update()
    {
        if (transform.position.x < -mapWidth * 2)
        {
           Destroy(gameObject);
        }
    }
}
