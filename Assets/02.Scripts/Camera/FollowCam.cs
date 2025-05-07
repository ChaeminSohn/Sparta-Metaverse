using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    Transform target;

    Vector3 offset;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        if (target == null) return;

        offset = transform.position - target.position;
    }

 
    void LateUpdate()
    {
        if(target == null) return;
        Vector3 pos = target.position;
        pos += offset;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
