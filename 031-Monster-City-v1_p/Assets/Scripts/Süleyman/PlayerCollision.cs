using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;


public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private float demolitionIndex;
    [SerializeField] private Transform _fightPosition;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _humanTransform;
    [SerializeField] private GameObject leftParticle;
    [SerializeField] private GameObject rightParticle;
    [SerializeField] private GameObject fightPanel;
    [SerializeField] private float cameraOffsetIncreaser = 0.8f;
    [SerializeField] SkinnedMeshRenderer[] _meshes;
    [SerializeField] SkinnedMeshRenderer[] _dragonMeshes;
    [SerializeField] ParticleSystem changeParticle;
    [SerializeField] GameObject dragonWings;
    [SerializeField] GameObject dragonTail;
    [SerializeField] Transform[] _avatars;

    public GameObject tapText;
    public bool front;
    public bool left;
    public bool right;
    public bool top;
    public int meshCount=0;
    public int activeMeshIndex=0;


   
    private bool cameraOffsetMove = false;
    private float runSpeed = 1f;


    public TrailRenderer[] trailRenderers;
    
    private CinemachineVirtualCamera _vcam;
    private AnimationController _animationController;
    private SPlayerMovement _playerMovement;
    private Rigidbody _rigidbody;
    private CinemachineTransposer transposer;
    private CinemachineComposer composer;
    private JoyStickRotator _rotator;
    Animator[] _animator;
    private void Awake()
    {
        _animationController=GetComponent<AnimationController>();
        _playerMovement = GetComponent<SPlayerMovement>();
        _rigidbody = GetComponent<Rigidbody>();
        _vcam = FindObjectOfType<CinemachineVirtualCamera>();
        _rotator = GetComponent<JoyStickRotator>();
        _animator = GetComponentsInChildren<Animator>();
        trailRenderers = GetComponentsInChildren<TrailRenderer>();

        leftParticle.gameObject.SetActive(false);
        rightParticle.gameObject.SetActive(false);

        foreach (var trails in trailRenderers)
        {
            trails.enabled = false;
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Building") )
        {
            _animationController.JumpAnimation(false);

            
            

            Vector3 playerPosition = this.gameObject.transform.position;
            Vector3 cornerTarget = collision.gameObject.GetComponent<CollisionDetection>()._cornerTarget.position;
            
            _animationController.OnGroundAnimation(false);
            
            if (playerPosition.z>cornerTarget.z && playerPosition.y<cornerTarget.y && playerPosition.x<cornerTarget.x )
            {
                Debug.Log("left");
                left = true;
                _animationController.ClimbingRightAnimation(left);
                _animationController.SprintAnimation(false);
                _animationController.JumpAnimation(false);
                rightParticle.gameObject.SetActive(true);
                
                GameManager.Instance.leftRightClimbingMode = true;
                GameManager.Instance.surfaceClimbingMode = true;
                Physics.gravity = new Vector3(0, -10f, 0);
            }

           else if (playerPosition.z>cornerTarget.z && playerPosition.y<cornerTarget.y && playerPosition.x>cornerTarget.x )
            {
                Debug.Log("right");
                right = true;
                _animationController.ClimbingLeftAnimation(right);
                _animationController.SprintAnimation(false);
                _animationController.JumpAnimation(false);
                leftParticle.gameObject.SetActive(true);
                _animationController.JumpEndAnimation(true);

                GameManager.Instance.leftRightClimbingMode = true;
                GameManager.Instance.surfaceClimbingMode = true;
                Physics.gravity = new Vector3(0, -10f, 0);
            }
            else if (playerPosition.y>cornerTarget.y)
            {

                _animationController.OnTopAnimation(true);
                _animationController.SprintAnimation(true);
                _animationController.JumpEndAnimation(true);
                Debug.Log("top");
                top = true;
                Physics.gravity = new Vector3(0, -10f, 0);
            }

            else//
                //(playerPosition.z<cornerTarget.z && playerPosition.y<cornerTarget.y && playerPosition.x>cornerTarget.x)
            {
                Debug.Log("front");
                front = true;
                _animationController.ClimbingFrontAnimation(front);
                _animationController.SprintAnimation(false);
                _animationController.JumpAnimation(false);
                _animationController.FallAnimation(false);
                _animationController.JumpEndAnimation(false);
                _animationController.JumpEndAnimation(true);

                GameManager.Instance.surfaceClimbingMode = true;
                Physics.gravity = new Vector3(0, -10f, 0);

            }

           
           
            if (GameManager.Instance.demolitionMode)
            {
                collision.transform.parent.GetChild(0).gameObject.SetActive(true);
                Debug.Log(collision.transform.parent.GetChild(0).gameObject.name);

                if(collision.transform.parent.gameObject.GetComponent<MeshRenderer>()!=null)
                collision.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled=false;

               
               if(collision.transform.parent.GetChild(2))
                {
                    collision.transform.parent.GetChild(2).gameObject.SetActive(false);
                }
                
                
                    

                collision.gameObject.SetActive(false);
                Physics.gravity = new Vector3(0, -300f, 0);
                
                _animationController.FallAnimation(false);
                _animationController.JumpAnimation(false);
                _animationController.RunningAnimation(true);
                _animationController.ClimbingFrontAnimation(false);
                _animationController.ClimbingLeftAnimation(false);
                _animationController.ClimbingRightAnimation(false);
                _animationController.SprintAnimation(false);
                _animationController.JumpEndAnimation(false);
                _animationController.OnTopAnimation(false);
                
                leftParticle.gameObject.SetActive(false);
                rightParticle.gameObject.SetActive(false);
                
                GameManager.Instance.leftRightClimbingMode = false;
                GameManager.Instance.surfaceClimbingMode = false;
            }
        }

        if (collision.gameObject.CompareTag("ground"))
        {
            
            _animationController.JumpAnimation(false);
            _animationController.FallAnimation(false);
            _animationController.OnGroundAnimation(true);
            _animationController.SprintAnimation(true);
            Physics.gravity = new Vector3(0, -10f, 0);
        }

        
        
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            
            /*_animationController.JumpAnimation(false);*/
            _animationController.FallAnimation(false);
           // _animationController.OnGroundAnimation(true);
            _animationController.SprintAnimation(true);
            _animationController.JumpEndAnimation(true);
            Physics.gravity = new Vector3(0, -10f, 0);
        }
        if (collision.gameObject.CompareTag("Building"))
        {
            _animationController.JumpAnimation(false);




            Vector3 playerPosition = this.gameObject.transform.position;
            Vector3 cornerTarget = collision.gameObject.GetComponent<CollisionDetection>()._cornerTarget.position;

            _animationController.OnGroundAnimation(false);

            if (playerPosition.z > cornerTarget.z && playerPosition.y < cornerTarget.y && playerPosition.x < cornerTarget.x)
            {
                Debug.Log("left");
                left = true;
                _animationController.ClimbingRightAnimation(left);
                _animationController.SprintAnimation(false);
                _animationController.JumpAnimation(false);
                rightParticle.gameObject.SetActive(true);

                GameManager.Instance.leftRightClimbingMode = true;
                GameManager.Instance.surfaceClimbingMode = true;
                Physics.gravity = new Vector3(0, -10f, 0);
            }

            else if (playerPosition.z > cornerTarget.z && playerPosition.y < cornerTarget.y && playerPosition.x > cornerTarget.x)
            {
                Debug.Log("right");
                right = true;
                _animationController.ClimbingLeftAnimation(right);
                _animationController.SprintAnimation(false);
                _animationController.JumpAnimation(false);
                leftParticle.gameObject.SetActive(true);

                GameManager.Instance.leftRightClimbingMode = true;
                GameManager.Instance.surfaceClimbingMode = true;
                Physics.gravity = new Vector3(0, -10f, 0);
            }
            else if (playerPosition.y > cornerTarget.y)
            {

                _animationController.OnTopAnimation(true);
                _animationController.SprintAnimation(true);
                _animationController.JumpEndAnimation(true);
                Debug.Log("top");
                top = true;
                Physics.gravity = new Vector3(0, -10f, 0);
            }

            else//
                //(playerPosition.z<cornerTarget.z && playerPosition.y<cornerTarget.y && playerPosition.x>cornerTarget.x)
            {
                Debug.Log("front");
                front = true;
                _animationController.ClimbingFrontAnimation(front);
                _animationController.SprintAnimation(false);
                _animationController.JumpAnimation(false);
                _animationController.FallAnimation(false);
                _animationController.JumpEndAnimation(false);
                _animationController.JumpEndAnimation(true);

                GameManager.Instance.surfaceClimbingMode = true;
                Physics.gravity = new Vector3(0, -10f, 0);

            }





        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            _animationController.FallAnimation(true);
            _animationController.JumpAnimation(true);
            _animationController.RunningAnimation(true);
            _animationController.ClimbingFrontAnimation(false);
            _animationController.ClimbingLeftAnimation(false);
            _animationController.ClimbingRightAnimation(false);
            _animationController.SprintAnimation(false);
            _animationController.JumpEndAnimation(false);
            _animationController.OnTopAnimation(false);
            
            leftParticle.gameObject.SetActive(false);
            rightParticle.gameObject.SetActive(false);
            
            GameManager.Instance.leftRightClimbingMode = false;
            GameManager.Instance.surfaceClimbingMode = false;
            front = false;
            left = false;
            right = false;
            top = false;
            _rigidbody.AddForce(new Vector3(0f,1000f,0f));
            Physics.gravity = new Vector3(0, -300f, 0);
            
        }
    }

    private void Update()
    {
        if (cameraOffsetMove && transform.localScale.x<4f )
            StartCoroutine(CameraOffset());


         foreach (var avatar in _avatars)
         {
             if(avatar.gameObject.activeSelf)
             {
                 avatar.transform.position = new Vector3(avatar.transform.position.x, avatar.transform.position.y, transform.position.z);
             }

         }
    }

    IEnumerator CameraOffset()
    {
        
        if (cameraOffsetMove)
        {
            transposer = _vcam.GetCinemachineComponent<CinemachineTransposer>();


            transposer.m_FollowOffset=Vector3.MoveTowards(transposer.m_FollowOffset,new Vector3(0f,
                transposer.m_FollowOffset.y+ cameraOffsetIncreaser*.5f,
                transposer.m_FollowOffset.z -(cameraOffsetIncreaser*.65f)),.05f) ;



            
        }
        if (!_playerMovement.hardEvolitionActivator)
            yield return new WaitForSeconds(0.7f);
        else
            yield return new WaitForSeconds(1f);



        cameraOffsetMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {


            foreach (var trails in trailRenderers)
            {
                trails.widthMultiplier += 0.1f;
                trails.time += 0.002f;
            }

            runSpeed -= 0.006f;

            foreach (var anim in _animator)
            {
                anim.SetFloat("runSpeed", runSpeed);
            }

            
            
            
            cameraOffsetMove = true;
            
            if (transform.localScale.x>demolitionIndex)
            {
                GameManager.Instance.demolitionMode = true;
            }

            Vector3 scaleIncreaser;
            float scaleLimit;
            if (_playerMovement.hardEvolitionActivator)
            {
               scaleIncreaser = this.gameObject.transform.localScale + new Vector3(0.4f, 0.4f, 0.4f);
                scaleLimit = 6f;
            }
            else
            {
                scaleLimit = 4f;
               scaleIncreaser = this.gameObject.transform.localScale + new Vector3(0.2f, 0.2f, 0.2f);
            }

            

            if (transform.localScale.x<=scaleLimit)
            {
                _playerMovement._moveSpeedIncreaser += 6.5f;
                
                this.gameObject.transform.DOScale(scaleIncreaser, 1f).OnComplete(() =>
                {
                    // Destroy(other.gameObject,1f);
                    // other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                });
                
              // StartCoroutine( CameraOffset());
            }
            
            
            
            //other.gameObject.transform.parent = _humanTransform;
            
            other.gameObject.transform.DOMove(_humanTransform.position, 0.1f)
                .OnComplete(() =>
                {
                    other.gameObject.transform.DOScale(Vector3.zero, .1f);

                });
            

        }

        if (other.gameObject.CompareTag("obstacle"))
        {

            meshCount++;
            bool growing = true;
            if (growing)
            {
                _animationController.RunningAnimation(true);
                
                other.gameObject.transform.DOMove(_humanTransform.position, 0.05f).OnComplete(() =>
                {
                    
                    other.gameObject.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                    {
                        
                        other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                    });

                    
                });
                _playerMovement.DNAControl();

                if(_playerMovement.hardEvolitionActivator && (meshCount%2)==0 && meshCount<=4)
                {
                    if(!_playerMovement.dragonActivator)

                    {
                        activeMeshIndex++;
                        foreach (var mesh in _meshes)
                        {
                            mesh.enabled = false;
                        }
                        if (activeMeshIndex > 3)
                            activeMeshIndex = 2;

                        _meshes[activeMeshIndex].enabled = true;
                    }
                    else
                    {
                        activeMeshIndex++;
                        foreach (var mesh in _dragonMeshes)
                        {
                            mesh.enabled = false;
                        }
                        if (activeMeshIndex > 3)
                            activeMeshIndex = 2;
                        dragonTail.gameObject.SetActive(true);
                        dragonWings.gameObject.SetActive(true);

                        _dragonMeshes[activeMeshIndex].enabled = true;
                    }
                    


                    changeParticle.Play();
                        
                }




                growing = false;
            }
           
        }

        if (other.gameObject.CompareTag("Fight"))
        {
            _playerMovement.stop = true;
            _playerMovement.fightTime = true;
            tapText.gameObject.SetActive(true);


            bossFight.Instance.Animator.SetBool("boxTime", true);

            _rigidbody.velocity=Vector3.zero;
            transform.eulerAngles=Vector3.zero;
            transform.Rotate(Vector3.zero);


            foreach (var anim in _animator)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isBoxing", true);
            }
            _animationController.RunningAnimation(true);
            _animationController.BoxingAnimation(true);

            transposer = _vcam.AddCinemachineComponent<CinemachineTransposer>();
            composer = _vcam.AddCinemachineComponent<CinemachineComposer>();
            transposer.m_FollowOffset = new Vector3(19f, 8f, -3f);
            composer.m_ScreenX = 0.41f;
            composer.m_ScreenY = 0.59f;
           // _vcam.Follow = bossTransform;
           // _vcam.LookAt = bossTransform;


            transform.LookAt(new Vector3(_enemy.transform.position.x,transform.position.y,_enemy.transform.position.z));
            transform.DOMove(new Vector3(_fightPosition.position.x, transform.position.y, _fightPosition.position.z),
                1f);

            fightPanel.gameObject.SetActive(true);
            
            
            GameManager.Instance.surfaceClimbingMode = false;
            GameManager.Instance.leftRightClimbingMode = false;
            GameManager.Instance.jumpMode = false;
            GameManager.Instance.moveMode = false;

            foreach (var trails in trailRenderers)
            {
                trails.enabled = false;
            }
        }
    }
}
