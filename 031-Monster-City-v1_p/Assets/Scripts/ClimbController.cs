using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbController : MonoBehaviour
{
    public Transform footPoint;
    public PlayerMove PlayerMove;
    public new Vector3 pos;
    public new Vector3 def;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("frontsurface")&&PlayerMove.scaleValue<PlayerMove.DestroyScale)
        {
           // footPoint.position = pos;
            AnimatorManager.Instance.fallClosed();
            
            GameManager.Instance.surfaceClimbingMode=true;

            PlayerMove.Instance.Zspeed = 0;
            GameManager.Instance.surfaceClimbingMode = true;
            GameManager.Instance.leftRightClimbingMode = false;
            GameManager.Instance.jumpMode = false;

            AnimatorManager.Instance.ClimbRun();
        } 
        if (other.gameObject.tag == "side"&& !GameManager.Instance.demolitionMode)
        {
        
        
       
            if (PlayerMove.scaleValue<PlayerMove.DestroyScale)
            {
                AnimatorManager.Instance.fallClosed();
                GameManager.Instance.surfaceClimbingMode=false;

                PlayerMove.Instance.Zspeed = 0;
        
                GameManager.Instance.leftRightClimbingMode = true;
                GameManager.Instance.jumpMode = false;

                AnimatorManager.Instance.LeftClimbRun();


             
            }
         
        }
        if (other.gameObject.tag == "Lside"&& !GameManager.Instance.demolitionMode)
        {
        
        
       
            if (PlayerMove.scaleValue<PlayerMove.DestroyScale)
            {
                AnimatorManager.Instance.fallClosed();
                GameManager.Instance.surfaceClimbingMode=false;

                PlayerMove.Instance.Zspeed = 0;
        
                GameManager.Instance.leftRightClimbingMode = true;
                GameManager.Instance.jumpMode = false;

                AnimatorManager.Instance.rightSide();

             
            }
         
        }
   
      
    }


  /*  private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "side"&& !GameManager.Instance.demolitionMode)
        {
        
            AnimatorManager.Instance.ClimbClosed();
            AnimatorManager.Instance.LeftClimbClosed();
            AnimatorManager.Instance.rightSideFlaser();
            Debug.Log("finishbuild");
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            PlayerMove.Instance.upPower = Vector3.zero;
            PlayerMove.Instance.Zspeed = 150;
       
          
         
        }
        if (other.gameObject.tag == "Lside"&& !GameManager.Instance.demolitionMode)
        {
        
            AnimatorManager.Instance.ClimbClosed();
            AnimatorManager.Instance.LeftClimbClosed();
            AnimatorManager.Instance.rightSideFlaser();
            Debug.Log("finishbuild");
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            PlayerMove.Instance.upPower = Vector3.zero;
            PlayerMove.Instance.Zspeed = 150;
       
         
        }
    }*/

   
}
