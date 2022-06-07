using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class uiscript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int vibration;
    public float strength;
    // Update is called once per frame
    void Update()
    {
        transform.DOShakePosition(1f, strength, vibration, 80f);
      
        
    }
}
