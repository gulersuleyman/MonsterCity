using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObstacleControl : MonoBehaviour
{
   
    public ParticleSystem scaleEffect;
    [SerializeField] private Vector3 scaleVec;
    [SerializeField] private float MaxScaleX;
    [SerializeField] private Transform humanPos;
    public PlayerMove PlayerMove;
    [SerializeField] private Ease ease;
    [SerializeField] private float speed;
    public ParticleSystem dnaEffect;
    public Transform posDna;
    public Animator Animator;
    public Animator enemyAnimate;
    public GameObject gameManager;
    public GameObject ui;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fall"))
        {
            if (PlayerMove.scaleValue<PlayerMove.DestroyScale)
            {
                AnimatorManager.Instance.falling();
            }
            else
            {
                AnimatorManager.Instance.fallClosed();
            }
          
        }

        if (other.gameObject.CompareTag("speedFresh"))
        {
            PlayerMove.Instance.Zspeed = 300;
        }
        if (other.gameObject.tag == "buildFinish")
        {
            //Physics.gravity = new Vector3(0,-600,0);
            AnimatorManager.Instance.ClimbClosed();
            AnimatorManager.Instance.LeftClimbClosed();
            AnimatorManager.Instance.rightSideFlaser();
            PlayerMove.rb.AddForce(new Vector3(0,1f,0)*100f*Time.deltaTime);
        //    Debug.Log("finishbuild");
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            PlayerMove.Instance.upPower = Vector3.zero;
            PlayerMove.Instance.Zspeed = 30;
            
       //    PlayerMove.Instance.zPowerFast = true;
        }

        if (other.gameObject.tag == "bridge")
        {
            if (!GameManager.Instance.surfaceClimbingMode && !GameManager.Instance.leftRightClimbingMode)
            {
              
                AnimatorManager.Instance.rightSideFlaser();
                AnimatorManager.Instance.ClimbClosed();
                PlayerMove.rb.AddForce(new Vector3(0,1f,0)*100f*Time.deltaTime);
                Debug.Log("finishbuild");
                GameManager.Instance.surfaceClimbingMode = false;
                GameManager.Instance.leftRightClimbingMode = false;
                PlayerMove.Instance.upPower = Vector3.zero;
                PlayerMove.Instance.Zspeed = 30;
            }
           
        }


        if (other.gameObject.tag == "obstacle"&&other.GetComponent<ObstacleType>().working)
        {
            ObstacleType tempobst = other.GetComponent<ObstacleType>();
      
            tempobst.working = false;
            if (tempobst.type == ObstacleType.obstacleType.dna)
            {
                
                ParticleSystem temp = Instantiate(dnaEffect, posDna.position, other.transform.rotation);
                temp.transform.parent = posDna.transform;
                Destroy(temp,1f);
                
                other.gameObject.transform.DOMove(humanPos.position, 0.1f).SetEase(ease).OnComplete(() =>
                {
                    PlayerMove.dnaControl();
                //    DOTween.Kill(other.gameObject, false);
                    /*   ParticleSystem particleSystem= Instantiate(scaleEffect);
                       particleSystem.transform.position = other.transform.position;
                       particleSystem.transform.parent = this.transform;*/
                    Destroy(other.gameObject);
                    
                });
               
            }
            if (tempobst.type == ObstacleType.obstacleType.halfHuman)
            {
                
               
                PlayerMove.scaleValue=PlayerMove.scaleValue+0.1f;
                PlayerMove.scaleTime();
                other.gameObject.transform.DOMove(humanPos.position, 0.1f).SetEase(ease).OnComplete(() =>
                {
                    /*   ParticleSystem particleSystem= Instantiate(scaleEffect);
                       particleSystem.transform.position = other.transform.position;
                       particleSystem.transform.parent = this.transform;*/
                    Destroy(other.gameObject);
                    
                });
               
            }
            if (tempobst.type == ObstacleType.obstacleType.human)
            {
             /*   if (this.gameObject.transform.localScale.x >= MaxScaleX)
                {
                    GameManager.Instance.demolitionMode = true;
                    return;
                    
                }*/
 
             PlayerMove.scaleValue=PlayerMove.scaleValue+0.2f;
             PlayerMove.scaleTime();
                other.gameObject.transform.DOMove(humanPos.position, 0.1f).SetEase(ease).OnComplete(() =>
                {
                  /*   ParticleSystem particleSystem= Instantiate(scaleEffect);
                     particleSystem.transform.position = other.transform.position;
                     particleSystem.transform.parent = this.transform;*/
                    Destroy(other.gameObject);
                    
                });
                // other.gameObject.transform.DOLocalMove(this.gameObject.transform.position, .2f);
                // Destroy(other.gameObject,.3f);

            }
        } if (other.gameObject.tag == "side"&& !GameManager.Instance.demolitionMode)
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
        
        if (other.gameObject.tag == "build")
        {
            if (GameManager.Instance.demolitionMode)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
           
        } 
        if (other.gameObject.tag == "FİGHTMODE")
        {
            GameManager.Instance.moveMode = false;
            GameManager.Instance.surfaceClimbingMode=false;

            PlayerMove.Instance.Zspeed = 0;
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            GameManager.Instance.jumpMode = false;

            PlayerMove.stop = true;
            Animator.Play("fight");
            AnimatorManager.Instance.actionTime();
            enemyAnimate.SetBool("boxTime",true);
            ui.SetActive(true);
           
          

        }
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            AnimatorManager.Instance.rightSideFlaser();
            AnimatorManager.Instance.fallClosed();
            GameManager.Instance.jumpMode = false;
            GameManager.Instance.moveMode = true;
            JumpControl.Instance.gravity = -9.81f;
            Physics.gravity = new Vector3(0, JumpControl.Instance.gravity, 0);
        } 
        if (other.gameObject.tag == "groundRoof"&&!GameManager.Instance.demolitionMode)
        {
            other.gameObject.GetComponent<leftSideClose>().falser();
            AnimatorManager.Instance.fallClosed();
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            GameManager.Instance.jumpMode = false;
            GameManager.Instance.moveMode = true;
            JumpControl.Instance.gravity = -9.81f;
            Physics.gravity = new Vector3(0, JumpControl.Instance.gravity, 0);
        }
        
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            
            AnimatorManager.Instance.falling();
          
        } 
        if (other.gameObject.tag == "groundRoof")
        {
            AnimatorManager.Instance.falling();
        }
        
    }

   /* private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "side"&& !GameManager.Instance.demolitionMode)
        {
        
       
       
            if (PlayerMove.scaleValue<PlayerMove.DestroyScale)
            {
                GameManager.Instance.surfaceClimbingMode=false;

                PlayerMove.Instance.Zspeed = 0;
        
                GameManager.Instance.leftRightClimbingMode = false;
                GameManager.Instance.jumpMode = true;

                AnimatorManager.Instance.JumpRun();


             
            }
         
        }
    }*/
}
