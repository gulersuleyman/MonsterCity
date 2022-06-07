using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public LayerMask layer;
    public float distance;

    public bool hitToContact;
    public string Type;

    public bool test;

   
    
    void FixedUpdate()
    {
        if (!GameManager.Instance.demolitionMode)
        {


            if (Type == "left" && hitToContact)
            {

                if (PlayerRot.Instance.dynamicHorizontal < 0f)
                {
                    AnimatorManager.Instance.LeftClimbRun();
                    PlayerMove.Instance.Zspeed = 100;
                    GameManager.Instance.leftRightClimbingMode = true;
                }

                if (PlayerRot.Instance.dynamicHorizontal > 0f)
                {
                    
                    GameManager.Instance.leftRightClimbingMode = false;
                    GameManager.Instance.jumpMode = true;
                    JumpControl.Instance.RightJump();
                    AnimatorManager.Instance.LeftClimbClosed();
                    AnimatorManager.Instance.JumpRun();

                }
            }

            if (Type == "right" && hitToContact)
            {

                if (PlayerRot.Instance.dynamicHorizontal > 0f)
                {
                    AnimatorManager.Instance.LeftClimbRun();
                    PlayerMove.Instance.Zspeed = 100;
                    GameManager.Instance.leftRightClimbingMode = true;
                    GameManager.Instance.leftRightClimbingMode = true;
                }

                if (PlayerRot.Instance.dynamicHorizontal < 0f)
                {
                   
                    GameManager.Instance.leftRightClimbingMode = false;
                    GameManager.Instance.jumpMode = true;
                    JumpControl.Instance.LeftJump();
                    AnimatorManager.Instance.LeftClimbClosed();
                    AnimatorManager.Instance.JumpRun();
                }
            }


            if (!hitToContact && !GameManager.Instance.jumpMode && !GameManager.Instance.surfaceClimbingMode &&
                !GameManager.Instance.leftRightClimbingMode)
            {
                GameManager.Instance.moveMode = true;
                AnimatorManager.Instance.LeftClimbClosed();

            }








            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance,
                layer))
            {
                hitToContact = true;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                hitToContact = false;

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
}
