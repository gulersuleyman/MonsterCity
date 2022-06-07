using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

   
    
    
    public Transform _cornerTarget;

    private void Awake()
    {
        _cornerTarget = this.gameObject.transform.GetChild(0).transform;
    }

    

}
