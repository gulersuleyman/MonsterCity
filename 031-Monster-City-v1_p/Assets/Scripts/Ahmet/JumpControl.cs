using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float jumpForce;
    public float gravity = -100f;
    public float verticalVelocity;
    public bool oneTimeRihtForce;
    public bool oneTimeLeftForce;
    public bool oneTimeForwardForce;


    
    
    #region Singleton

    public static JumpControl Instance { get; private set; }

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

    void FixedUpdate()
    {
       // Debug.Log("mode" + GameManager.Instance.jumpMode);
       // Debug.Log("gravity" + Physics.gravity);
        if (GameManager.Instance.moveMode &&  !GameManager.Instance.jumpMode )
        {
           
            
            oneTimeLeftForce = false;
            oneTimeRihtForce = false;
            oneTimeForwardForce = false;
         /*   AnimatorManager.Instance.JumpClosed();
            AnimatorManager.Instance.BridgejumpClosed();*/
        }
        
    }


    public void RightJump()
    {
        if (!oneTimeRihtForce)
        {   
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 1 );
            rb.AddForce(new Vector3(.4f, 1f, 1f) * jumpForce * Time.deltaTime,ForceMode.VelocityChange);
            Invoke("GravityChange",.4f);

            oneTimeRihtForce = true;
        }
    } 
    public void LeftJump()
    {
        
        if (!oneTimeLeftForce)
        {
            
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 1 );
            rb.AddForce(new Vector3(-.4f, 1f, 1f) * jumpForce * Time.deltaTime,ForceMode.VelocityChange);
           Invoke("GravityChange",.4f);
            oneTimeLeftForce = true;
        }
    }

    public void ForwardJump()
    {
        if (!oneTimeForwardForce)
        {
            PlayerMove.Instance.Zspeed = 0;
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 1 );
            rb.AddForce(new Vector3(0f, .9f, 1f) * jumpForce * Time.deltaTime,ForceMode.VelocityChange);
            Invoke("GravityChangeForward",.4f);
            oneTimeForwardForce = true;
        }
    }


    void GravityChange()
    {
        if (GameManager.Instance.jumpMode)
        {
            gravity = -50;
            gravity = Mathf.Lerp(gravity, -50, 1f);
            Physics.gravity = new Vector3(0, gravity, 0);
        }
       
    }
    void GravityChangeForward()
    {
        if (GameManager.Instance.jumpMode)
        {
            gravity = -30;
            gravity = Mathf.Lerp(gravity, -40, 1f);
            Physics.gravity = new Vector3(0, gravity, 0);
        }
       
    }
}
