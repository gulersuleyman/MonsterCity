using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot : MonoBehaviour
{
    
    public bool mouseDown;
    public Joystick dynamicJoystick;
    public float turnSmoothTime = .1f;
    private float turnSmoothVelocity;
    
    private Vector3 direction;
    private float horizontal;
    private float vertical;
    private float angle;
    public Transform avatar;
    
    public float dynamicHorizontal;
    
    #region Singleton

    public static PlayerRot Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("EXTRA : " + this + "  SCRIPT DETECTED RELATED GAME OBJ DESTROYED");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion
    private void FixedUpdate()
    {
        dynamicHorizontal = dynamicJoystick.Horizontal;

        if (GameManager.Instance.moveMode)
        {
            if (Input.GetMouseButton(0))
            {
                mouseDown = true;
            }
            else
            {
                mouseDown = false;
            }  
        
        
            RotControl();
        }  
        if (GameManager.Instance.leftRightClimbingMode)
        {

          
            RotFirsChange();

        }  
        if (GameManager.Instance.surfaceClimbingMode)
        {
           
            RotFirsChange();
        
            
        } 
     
        
    }


   public void RotControl()
    {
        
        horizontal = dynamicJoystick.Horizontal;
        vertical = dynamicJoystick.Vertical;
        direction = new Vector3(horizontal, 0, vertical).normalized;
        
        if (mouseDown)
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                targetAngle = Mathf.Clamp(targetAngle, -50, 50);
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
        else if (!mouseDown)
        {
          RotFirsChange();
        }
    } 


   public void RotFirsChange()
    {

        transform.rotation=Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,0),0.2f );
       avatar.transform.rotation=Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,0),0.2f );
    }
    
}
