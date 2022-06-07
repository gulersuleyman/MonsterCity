using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upController : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("buildFinish"))
        {
            AnimatorManager.Instance.ClimbClosed();
            AnimatorManager.Instance.LeftClimbClosed();
            AnimatorManager.Instance.rightSideFlaser();
            Debug.Log("finishbuild");
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            PlayerMove.Instance.upPower = Vector3.zero;
            PlayerMove.Instance.Zspeed = 150;
            PlayerMove.Instance.rb.AddForce(new Vector3(0,1f,0)*100f*Time.deltaTime);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("buildFinish"))
        {
            AnimatorManager.Instance.ClimbClosed();
            AnimatorManager.Instance.LeftClimbClosed();
            AnimatorManager.Instance.rightSideFlaser();
          //  Debug.Log("finishbuild");
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            PlayerMove.Instance.upPower = Vector3.zero;
            PlayerMove.Instance.Zspeed = 150;
            PlayerMove.Instance.rb.AddForce(new Vector3(0,1f,0)*100f*Time.deltaTime);
        }
    } 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("buildFinish"))
        {
            AnimatorManager.Instance.ClimbClosed();
            AnimatorManager.Instance.LeftClimbClosed();
            AnimatorManager.Instance.rightSideFlaser();
      //      Debug.Log("finishbuild");
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            PlayerMove.Instance.upPower = Vector3.zero;
            PlayerMove.Instance.Zspeed = 150;
            PlayerMove.Instance.rb.AddForce(new Vector3(0,1f,0)*100f*Time.deltaTime);
        }
    }
}
