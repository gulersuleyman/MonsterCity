using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{



   
    
    
    public Joystick dynamicJoystick;
    public float Zspeed;
    public float Xspeed;

    public Rigidbody rb;

    public Vector3 upPower;
    private Vector3 direction;
    private float horizontal;
    private float vertical;

  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(dynamicJoystick.Horizontal * Xspeed * Time.deltaTime, 0, Zspeed * Time.deltaTime); 
       

      
        rb.velocity = temp;
    }

   
}
