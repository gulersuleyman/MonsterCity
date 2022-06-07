using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class dnaRotator : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    
    public bool working;

    public new Vector3 rotate;
    
    private float degrees;

    public bool scale;
    public bool scale2;
    // Update is called once per frame
    private void Start()
    {
        
    }

    void Update()
    {
        
            transform.Rotate(0, 100 * Time.deltaTime, 0);
      
            if (_player.transform.localScale.x>1.5&&!scale)
            {
            
                transform.DOScale(4, .1f);
                scale = true;
            }
            if (_player.transform.localScale.x>0.8f&&!scale2)
            {
            
                transform.DOScale(2, .1f);
                scale2 = true;
            }
        
        
      
    }
}
