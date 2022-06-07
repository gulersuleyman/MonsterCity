using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;


public class SPlayerMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _avatar;
    [SerializeField] private GameObject beginningText;
    [SerializeField] private GameObject _wing;
    [SerializeField] private GameObject _tail;
    [SerializeField] private GameObject _horn;
    [SerializeField] GameObject _mutant;
    [SerializeField] private int _dnaScaleValue = 100;
    [SerializeField] float leftLimit=-11f;
    [SerializeField] float rightLimit=0;
    
    public bool hardEvolitionActivator;
    public bool dragonActivator;


    
    
    
    public bool _started = false;
    public float _moveSpeedIncreaser;
    public bool stop = false;
    public bool fightTime = false;
    public bool canPunch = true;
    public int touchCount = 0;
    
    
    
    private bool began=true;
    private float playerXLimit;
    
    
    private DynamicJoystick _joystick;
    private Rigidbody _rigidbody;
    private AnimationController _animationController;
    PlayerCollision _playerCollision;
    JoyStickRotator joyStickRotator;
    public Animator[] _animator;


   

    private void Awake()
    {
        _joystick = FindObjectOfType<DynamicJoystick>();
        _rigidbody = GetComponent<Rigidbody>();
        _animationController = GetComponent<AnimationController>();
        _animator = GetComponentsInChildren<Animator>();
        joyStickRotator = GetComponent<JoyStickRotator>();
        _playerCollision = GetComponent<PlayerCollision>();
        

    }


    private void Update()
    {
       Beginning();

        
        _avatar.transform.position = new Vector3(_avatar.transform.position.x, _avatar.transform.position.y, transform.position.z);



    }

    private void FixedUpdate()
    {
        if (_started)
        {
            
            PlayerLimit();
            Mover(); 
            Rotator(); 
        }
    }

    void Mover()
    {
        if (!stop)
        {
            if (!GameManager.Instance.surfaceClimbingMode)
            {
                Vector3 temp= _rigidbody.transform.forward*(_moveSpeed+_moveSpeedIncreaser) * Time.deltaTime;
                _rigidbody.velocity = temp;
            
            }
            else if(!GameManager.Instance.leftRightClimbingMode)
            {

                _rigidbody.velocity = new Vector3(_joystick.Horizontal, 1f, 0)*Time.deltaTime *(_moveSpeed+_moveSpeedIncreaser);
            }
            else
            {
                _rigidbody.velocity = new Vector3(0f, 1f, 0.5f)*Time.deltaTime *(_moveSpeed+_moveSpeedIncreaser);
            } 
        }
        else 
        {
            
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.2f);
                _avatar.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.2f);
            
            if(fightTime)
            {
                if(Input.GetMouseButtonDown(0) && canPunch)
                {
                    
                    if(touchCount >= 10 && touchCount != 0 && transform.localScale.x > 3f)
                    {
                        bossFight.Instance.extraDamage = 2;
                    }
                    else if(touchCount >= 6 && touchCount != 0 && transform.localScale.x > 2f)
                    {
                        bossFight.Instance.extraDamage = 1;
                    }

                    else
                    {
                        bossFight.Instance.extraDamage = 0;
                    }

                       foreach (var anim in _animator)
                        {
                            _animationController.PunchAnimation(anim, true);
                        }
                    

                    
                }
                if(Input.GetMouseButtonDown(0))
                {
                    touchCount++;
                }
                if(bossFight.Instance.playerLifeDuration==0)
                {
                    canPunch = false;
                }
            }
        }
        
        
    }

    void Rotator()
    {
        if (!stop)
        {
            float  horizontal = _joystick.Horizontal;
            float vertical = Mathf.Abs(_joystick.Vertical);
            Vector3 direction = new Vector3(horizontal, 0, vertical+0.2f).normalized;
        
            if (Input.GetMouseButton(0) && !GameManager.Instance.surfaceClimbingMode)
            {
                if (direction.magnitude >= .9f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    targetAngle = Mathf.Clamp(targetAngle, -50, 50);
                    //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                }
            }
            else
            {
                transform.rotation=Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,0),0.2f );
                _avatar.transform.rotation=Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,0),0.2f ); 
            }
        }
        
        
    }

    void PlayerLimit()
    {
        playerXLimit = transform.position.x;
        playerXLimit = Mathf.Clamp(playerXLimit, leftLimit, rightLimit);
        transform.position = new Vector3(playerXLimit, transform.position.y, transform.position.z);
    }

    void Beginning()
    {
        if (Input.GetMouseButtonDown(0) && began)
        {
            _started = true;
            beginningText.gameObject.SetActive(false);
        }

        if (_started && began)
        {

            foreach (var trails in _playerCollision.trailRenderers)
            {
                trails.enabled = true;
            }

            // _animationController.RunningAnimation(true);  //PROBLEM!!!!!!
            foreach (var anim in _animator)
            {
                anim.SetBool("isRunning", true);
            }
            began = false;

        }
    }

    public void DNAControl()
    {

        if(_wing.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0)>=0)
        {
            _dnaScaleValue -= 8;
            _horn.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, _dnaScaleValue);
            _wing.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, _dnaScaleValue);
            _tail.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, _dnaScaleValue);
            _mutant.GetComponent<SkinnedMeshRenderer>(). SetBlendShapeWeight(0, _dnaScaleValue);
        }

        
    }

    
}
