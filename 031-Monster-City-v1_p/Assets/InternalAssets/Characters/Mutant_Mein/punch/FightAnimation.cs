using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightAnimation : MonoBehaviour
{
    [SerializeField] Animator EnemyAnimController;
    [SerializeField] Animator[] PlayerAnimController;
    [SerializeField] ParticleSystem enemyPuncParticle;
    [SerializeField] ParticleSystem playerPuncParticle;
    [SerializeField] ParticleSystem playerKickParticle;




    AnimationController _animationController;
    SPlayerMovement _playerMovement;
    PlayerCollision _playerCollision;

    float punchTime;
    private void Awake()
    {
        _playerMovement = FindObjectOfType<SPlayerMovement>();
        _animationController = FindObjectOfType<AnimationController>();
        _playerCollision = FindObjectOfType<PlayerCollision>();
    }
    private void Start()
    {
        PlayerAnimController = _playerMovement._animator;

        
       // PlayerAnimController = GetComponent<Animator>();
    }

    private void Update()
    {


        if(_playerMovement.stop && _playerMovement.fightTime && !bossFight.Instance.didWin && !bossFight.Instance.didLose)
        {
            punchTime += Time.deltaTime;

            if(punchTime>=3.5f)
            {
                _animationController.SelfPunchAnimation(EnemyAnimController, true);
                

                
                punchTime = 0;
                
            }

        }
        else
        {
            _animationController.SelfPunchAnimation(EnemyAnimController, false);
        }
    }



    public void punchFonction()  //for enemy
    {
        // AnimatorManager.Instance.punchFalse();
        // bossFight.Instance.lifeLose();

        punchTime = 0;
        _animationController.SelfPunchAnimation(EnemyAnimController, false);



        foreach (var anim in PlayerAnimController)
        {
            _animationController.HitAnimation(anim, true);
        }

        
        _animationController.PunchAnimation(EnemyAnimController, true);
        bossFight.Instance.PlayerLifeLose();
        enemyPuncParticle.Play();
        if(bossFight.Instance.lifeDuration<=1 && bossFight.Instance.playerLifeDuration!=0)
        {
            foreach (var anim in PlayerAnimController)
            {
                _animationController.KickAnimation(anim, true);
            }
            
        }
        if(bossFight.Instance.playerLifeDuration==0)
        {
            _playerMovement.canPunch = false;
            
            _animationController.DeathAnimation(true);
            

            foreach (var anim in PlayerAnimController)
            {
                _animationController.HitAnimation(anim, true);
                _animationController.PunchAnimation(anim, false);
            }
        }
      
    } 

    public void KickBegan()
    {
        _playerCollision.tapText.SetActive(false);
        Time.timeScale = 0.4f;
    }


    public void kick()
    {
        //AnimatorManager.Instance.kickFalse();
        // bossFight.Instance.lifeLose();
        bossFight.Instance.lifeDuration--;
        bossFight.Instance.lifeDuration = 0;

        foreach (var anim in PlayerAnimController)
        {
            _animationController.KickAnimation(anim, false);
        }
        

        playerKickParticle.Play();
        Time.timeScale = 1f;
    }
    public void PlayerPunch()
    {
        if(bossFight.Instance.lifeDuration !=1)
        {
            
            
            bossFight.Instance.lifeLose();

            //playerCanPunch = false;
            _playerMovement.canPunch = false;

            if(bossFight.Instance.lifeDuration==0)
            {
                bossFight.Instance.lifeDuration = 1;
                bossFight.Instance.slider.value = bossFight.Instance.lifeDuration;
            }
        }
        _animationController.HitAnimation(EnemyAnimController, true);
       
            playerPuncParticle.Play();
        
        foreach (var anim in PlayerAnimController)
        {
            _animationController.PunchAnimation(anim, false);
        }
        
        

    }

    public void PlayerHit()
    {

        foreach (var anim in PlayerAnimController)
        {
            _animationController.HitAnimation(anim, false);
        }
        

        // playerCanPunch = true;
        _playerMovement.canPunch = true;
    }
    public void EnemyHit()
    {
        _animationController.PunchAnimation(EnemyAnimController, false);
        _animationController.HitAnimation(EnemyAnimController, false);
    }


    
}
