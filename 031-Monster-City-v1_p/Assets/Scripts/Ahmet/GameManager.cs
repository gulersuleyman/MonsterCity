using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool moveMode;
  public bool jumpMode;
  public bool leftRightClimbingMode;
  public bool surfaceClimbingMode;
  public bool demolitionMode;
  public bool lose;
  public bool success;

  
  #region Singleton

  public static GameManager Instance { get; private set; }

  private void Awake()
  {

    demolitionMode = false;
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
  
  
  
}
