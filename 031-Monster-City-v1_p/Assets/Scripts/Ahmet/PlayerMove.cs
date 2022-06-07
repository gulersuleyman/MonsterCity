using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using DG.Tweening;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerMove : MonoBehaviour
{
    public GameObject Collider;
    public Joystick Joystick;
    public int lifeDuration=10;
    public Slider LSlider;
    public bool controllerCheck;
    public int Temp;
    private bool detector;
    public Transform enemy;
    public Transform fightPos;
    public ParticleSystem punchEffect;
    public Transform hand;
    public bossFight BossFight;
    public int count;
    public Slider Slider;
    public GameObject goSlider;
    public Animator AnimatorCam;
    public SkinnedMeshRenderer blandShapeScale;
    public GameObject DnaWing;
    public GameObject Dnahorn;
    public GameObject DnaTail;
    public GameObject warCanvas;
     int dnaValue=100;
     private int index;
    public CinemachineVirtualCamera VirtualCamera;
    public DynamicJoystick dynamicJoystick;
    public float Zspeed;
    public float Yspeed;
    public float scaleValue;
    public Rigidbody rb;
    public int tempShape=100;
    public int tempShapeZero=0;
    public Vector3 upPower;
    private Vector3 direction;
    private float horizontal;
    private bool camScale;
    private float vertical;
    public float DestroyScale;
    public Vector3 distance;
    public bool stop;
    public GameObject tutorial;
    public bool started = false;
    #region Singleton

    public static PlayerMove Instance { get; private set; }

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

    private void Start()
    {
        AnimatorCam.Play("cam1");
       VirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset=distance;
        camScale = true;
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AnimatorManager.Instance.IsRunning();
            started = true;
        }
    }

    public void FixedUpdate()
    {
        if (started)
        {
          if (!stop)
        {
            if (Input.GetMouseButton(0))
            {
               canvasDestroy();
               
            }
            if (scaleValue<DestroyScale)
            {
                GameManager.Instance.demolitionMode = false;
            }

            if (scaleValue>=DestroyScale)
            {
            
                GameManager.Instance.demolitionMode = true;
            } 
            if (scaleValue>=4)
            {
                camScale = false;
                // VirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset=new Vector3(0,7,-13f);
            }
            if (GameManager.Instance.moveMode && !GameManager.Instance.jumpMode &&!GameManager.Instance.surfaceClimbingMode) 
            {
                AnimatorManager.Instance.LeftClimbClosed();

                if (scaleValue>1.5f)
                {
                    Zspeed = 500; 
                }
                else
                {
                    Zspeed = 100; 
                }
                
                upPower = new Vector3(0, 0, 0);
                PlayerZMove();
            }

            if (GameManager.Instance.leftRightClimbingMode)
            {
                upPower = new Vector3(0, Yspeed, 0);
                PlayerZMove();
            } 
            if (GameManager.Instance.surfaceClimbingMode)
            {
                
                upPower = new Vector3(0, Yspeed, 0);
                PlayerZMove();
            }

            if (Zspeed == 0)
            {
                GameManager.Instance.moveMode = false;
            }
            else if ((Zspeed != 0 && GameManager.Instance.leftRightClimbingMode )|| (GameManager.Instance.surfaceClimbingMode &&Zspeed != 0))
            {
                GameManager.Instance.moveMode = false;
            }
            else if (Zspeed != 0 && !GameManager.Instance.leftRightClimbingMode  && !GameManager.Instance.surfaceClimbingMode && !GameManager.Instance.jumpMode)
            {
                GameManager.Instance.moveMode = true;
            }
        }
        else
        {
            BossFight.GoWay = true;
            if (Input.GetMouseButtonUp(0)||Input.GetMouseButtonUp(0)||Input.GetMouseButton(0)&&controllerCheck)
            {
            
              punchOrkick();
           

            }
            else
            {
                controllerCheck = true;
            }
            
            warCanvas.SetActive(true);
            transform.LookAt(enemy.transform.position);
            transform.DOMove(new Vector3(fightPos.transform.position.x,fightPos.transform.position.y,fightPos.transform.position.z), 5f).OnComplete(()=>
            {
                detector = true;

            });
            canvasDestroy();
          

        /*    if (count>5)
            {
                bossTime();
            }*/
        }  
        }
        
     

     
 
    }

    void punchOrkick( )
    {
        Temp = Random.Range(0, 2);
        controllerCheck = false;
        count++;
        BossFight.distance++;
        if (Temp==0)
        {
            AnimatorManager.Instance.punchTime();
        }if (Temp==1)
        {
            AnimatorManager.Instance.kickTime();
        }
        if (Temp==2)
        {
            AnimatorManager.Instance.punchTime();
        }
    }
    

    void canvasDestroy()
    {
        goSlider.SetActive(false);
        tutorial.SetActive(false);
      
    }
    public void lifeLose()
    {
        
        lifeDuration--;
        LSlider.value = lifeDuration;
    }

 
    void PlayerZMove()
    {
        /*horizontal = dynamicJoystick.Horizontal;
        vertical = dynamicJoystick.Vertical;
        direction = new Vector3(horizontal, 0, vertical).normalized;*/
        if (!GameManager.Instance.surfaceClimbingMode)
        {
            Vector3 temp= rb.transform.forward*Zspeed * Time.deltaTime + upPower*Time.deltaTime + new Vector3(0,rb.velocity.y,0);

            if (temp.y > upPower.y)
            {
                temp.y = upPower.y;
            }
            rb.velocity = temp;
        }
        else
        {

            rb.velocity = new Vector3(Joystick.Horizontal, 0, 0)+upPower*Time.deltaTime*13f;
        }
       

    }

    public void dnaControl()
    {
        dnaValue=dnaValue-10;
        Slider.value=Slider.value+1;
        Dnahorn.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,dnaValue);
        DnaWing.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,dnaValue);
        DnaTail.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0,dnaValue);
        
        
        

    }
    public void scaleTime()
    {
        index++;
        tempShape = tempShape - 10;
       
        tempShapeZero = tempShapeZero + 10;
        

  /*      if (index==1)
        {
            AnimatorCam.Play("cam2");
        }*/
        if (index==4)
        {
            AnimatorCam.Play("cam3");
        } 
      /*  if (index==3)
        {
            AnimatorCam.Play("cam4");
        } */ if (index==6)
        {
            AnimatorCam.Play("cam5");
        }
       /* if (index==5)
        {
            AnimatorCam.Play("cam6");
        } */
        if (index==9)
        {
            AnimatorCam.Play("cam7");
        }/* if (index==7)
        {
            AnimatorCam.Play("cam8");
        } */
        if (index==12)
        {
            AnimatorCam.Play("cam9");
        }
           
           /* distance=  new Vector3(distance.x,distance.y+4.55f,distance.z - 5f);
            VirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset=distance;*/
        

        blandShapeScale.SetBlendShapeWeight(0,tempShape);
      //  blandShapeScale.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1,tempShapeZero);
        transform.DOScale(scaleValue,1f);
        Collider.transform.localScale = Collider.transform.localScale / 1.2f;
    }

    public void zPowerAdd()
    {
        Zspeed=Mathf.MoveTowards(Zspeed, 200, Time.deltaTime);
        if (Zspeed>=200)
        {
            //zPowerFast = false;
        }
       
    }
    public void normalMode()
    {
        transform.DORotate(new Vector3(0, 0, 0),.5f);
    }


}
