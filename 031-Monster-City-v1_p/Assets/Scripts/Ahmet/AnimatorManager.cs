using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator anim;
    public string climbAnim; 
    public string leftclimbAnim; 
    public string jumpAnim;
    public string bridgejumpAnim;
    public string fall;
    public string reverseClimb;
    public string action;
    public string punch;
    public string kick;
    public string isRunning;
    
    #region Singleton

    public static AnimatorManager Instance { get; private set; }

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
    public void IsRunning()
    {
        anim.SetBool(isRunning,true);
        
    } 
    public void ClimbRun()
    {
        anim.SetBool(climbAnim,true);
        
    } 
    public void punchTime()
    {
        anim.SetBool(punch,true);
        
    }    
    public void punchFalse()
    {
        anim.SetBool(punch,false);
        
    }    
    public void rightSide()
    {
        anim.SetBool(reverseClimb,true);
        
    }    
    public void rightSideFlaser()
    {
        anim.SetBool(reverseClimb,false);
        
    }   
    public void kickTime()
    {
        anim.SetBool(kick,true);
        
    }    
    public void kickFalse()
    {
        anim.SetBool(kick,false);
        
    }  
    public void ClimbClosed()
    {
        anim.SetBool(climbAnim,false);

    }
    public void falling()
    {
        anim.SetBool(fall,true);
        
    }  
    public void fallClosed()
    {
        anim.SetBool(fall,false);

    }
    public void JumpRun()
    {
        anim.SetBool(jumpAnim,true);

    }  
    public void BridgejumpRun()
    {
        anim.SetBool(bridgejumpAnim,true);

    } 
    public void BridgejumpClosed()
    {
        anim.SetBool(bridgejumpAnim,false);

    }   
    public void JumpClosed()
    {
        anim.SetBool(jumpAnim,false);

    }  
    public void LeftClimbRun()
    {
        anim.SetBool(leftclimbAnim,true);

    }
    public void LeftClimbClosed()
    {
        anim.SetBool(leftclimbAnim,false);

    } 
    public void actionTime()
    {
        anim.SetBool(action,true);

    }
    
    
    
    
}
