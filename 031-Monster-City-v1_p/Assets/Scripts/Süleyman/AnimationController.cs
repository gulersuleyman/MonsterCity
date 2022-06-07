using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator[] _animator;

    private void Awake()
    {
        _animator = GetComponentsInChildren<Animator>();
    }
    
    public void RunningAnimation(bool isRunning)
    {
        foreach (var _animator in _animator)
        {

            if (isRunning == _animator.GetBool("isRunning")) return;

            _animator.SetBool("isRunning", isRunning);
        }

        
    }

    public void ClimbingFrontAnimation(bool isClimbingFront)
    {
        foreach (var _animator in _animator)
        {
            if (isClimbingFront == _animator.GetBool("isClimbingFront")) return;

            _animator.SetBool("isClimbingFront", isClimbingFront);

        }
        
        
    }
    public void ClimbingRightAnimation(bool isClimbingRight)
    {
        foreach (var _animator in _animator)
        {
            if (isClimbingRight == _animator.GetBool("isClimbingRight")) return;

            _animator.SetBool("isClimbingRight", isClimbingRight);
        }
          
    }
    public void ClimbingLeftAnimation(bool isClimbingLeft)
    {
        foreach (var _animator in _animator)
        {
            if (isClimbingLeft == _animator.GetBool("isClimbingLeft")) return;

            _animator.SetBool("isClimbingLeft", isClimbingLeft);
        }
         
    }
    public void FallAnimation(bool isFalling)
    {
        foreach (var _animator in _animator)
        {
            if (isFalling == _animator.GetBool("isFalling")) return;

            _animator.SetBool("isFalling", isFalling);
        }
          
    }
    public void JumpAnimation(bool isJump)
    {
        foreach (var _animator in _animator)
        {
            if (isJump == _animator.GetBool("isJump")) return;

            _animator.SetBool("isJump", isJump);
        }

           
    }

    public void SprintAnimation(bool isSprinting)
    {
        foreach (var _animator in _animator)
        {
            if (isSprinting == _animator.GetBool("isSprinting")) return;

            _animator.SetBool("isSprinting", isSprinting);
        }
          
    }

    public void BoxingAnimation(bool isBoxing)
    {
        foreach (var _animator in _animator)
        {
            if (isBoxing == _animator.GetBool("isBoxing")) return;

            _animator.SetBool("isBoxing", isBoxing);
        }
         
    }

    public void JumpEndAnimation(bool isJumpEnd)
    {
        foreach (var _animator in _animator)
        {
            if (isJumpEnd == _animator.GetBool("isJumpEnd")) return;

            _animator.SetBool("isJumpEnd", isJumpEnd);
        }
          
    }
    public void OnGroundAnimation(bool isOnGround)
    {
        foreach (var _animator in _animator)
        {
            if (isOnGround == _animator.GetBool("isOnGround")) return;

            _animator.SetBool("isOnGround", isOnGround);
        }
          
    }
    public void OnTopAnimation(bool isOnTop)
    {
        foreach (var _animator in _animator)
        {
            if (isOnTop == _animator.GetBool("isOnTop")) return;

            _animator.SetBool("isOnTop", isOnTop);
        }
            
    }
    public void PunchAnimation(Animator animator,bool isPunch)
    {
        if (isPunch == animator.GetBool("isPunch")) return;

        animator.SetBool("isPunch", isPunch);
    }
    public void HitAnimation(Animator animator,bool didHit)
    {
        if (didHit == animator.GetBool("didHit")) return;

        animator.SetBool("didHit", didHit);
    }
    public void KickAnimation(Animator animator,bool isKicking)
    {
        if (isKicking == animator.GetBool("isKicking")) return;

        animator.SetBool("isKicking", isKicking);
    }
    public void DeathAnimation(bool isDeath)
    {
        foreach (var _animator in _animator)
        {
            if (isDeath == _animator.GetBool("isDeath")) return;

            _animator.SetBool("isDeath", isDeath);
        }
          
    }
    public void SelfPunchAnimation(Animator animator, bool selfPunch)
    {
        if (selfPunch == animator.GetBool("selfPunch")) return;

        animator.SetBool("selfPunch", selfPunch);
    }
}
