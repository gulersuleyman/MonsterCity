using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axisFixated : MonoBehaviour
{
    public float globalX;
    void Start()
    {
        globalX=transform.position.x;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        currentPos.x = globalX;
        transform.position =   currentPos ;
    }
}
