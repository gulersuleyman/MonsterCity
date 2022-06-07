using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;

public class bossFight : MonoBehaviour
{
    private CinemachineVirtualCamera _vcam;
    


    public bool GoWay;
    public int distance;
   
    public ragdoll1 Ragdoll1;
    public GameObject _avatar;

    

    public Animator Animator;
    public Animator CAnimator;
    public Slider slider;
    public Slider playerSlider;
    public GameObject collider;
    public defaultTracker5 DefaultTracker5;
    // Update is called once per frame
    public int lifeDuration = 4;
    public int playerLifeDuration=4;
    public int extraDamage = 0;
    public bool finalBlow;
    public bool didWin=false;
    public bool didLose=false;
    public static bossFight Instance { get; private set; }

    private void Awake()
    {
       // LevelSystem.Instance.DidYouReturnPanel = false;
       // LevelSystem.Instance.DidYouNextLevelPanel = false;

        Animator = GetComponentInChildren<Animator>();
        Ragdoll1 = GetComponentInChildren<ragdoll1>();
        _vcam = FindObjectOfType<CinemachineVirtualCamera>();
       

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

    public void lifeLose()
    {
        if(lifeDuration <0)
        {
            lifeDuration = 0;
        }
        lifeDuration-=(1+extraDamage);
        slider.value = lifeDuration;
    }
    public void PlayerLifeLose()
    {

        playerLifeDuration--;
        playerSlider.value = playerLifeDuration;
    }

    void Update()
    {
        if (lifeDuration==0 && !finalBlow && !didWin && !didLose)
        {


            CinemachineTransposer transposer = _vcam.GetCinemachineComponent<CinemachineTransposer>();
            _vcam.transform.eulerAngles = new Vector3(_vcam.transform.eulerAngles.x, 0, 0);
             transposer.m_FollowOffset = new Vector3(0f, 20f, 25f);
           


            _vcam.Follow = this.transform; 
            _vcam.LookAt = this.transform;

            _avatar.transform.DOMoveY(_avatar.transform.position.y+2f , 2f);   




            Animator.enabled = false;
            Ragdoll1.RagdollActivator = true;
            collider.transform.DOMoveZ(collider.transform.position.z+35f,2f).OnComplete(()=>
            {

             //  LevelSystem.Instance.DidYouNextLevelPanel = true;

            });    //    ucma suresi ve uzaklıgı belırlı bır sabıte gore olmalı
            DefaultTracker5.enabled = true;
            slider.value = lifeDuration;
            finalBlow = true;
            didWin = true;
        }  
        if(playerLifeDuration<=0 && !didWin && !didLose)
        {
            playerLifeDuration = 0;
          //  LevelSystem.Instance.DidYouReturnPanel = true;
            didLose = true;
        }
        
        
        
            
    }
}
