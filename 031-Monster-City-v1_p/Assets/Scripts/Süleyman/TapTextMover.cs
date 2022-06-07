using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TapTextMover : MonoBehaviour
{

    Vector3 firstScale;
    Vector3 firstRotation;
    // Start is called before the first frame update
    void OnEnable()
    {
        firstScale = transform.localScale;
        

        
        Move();
        Rotate();
    }

    // Update is called once per frame
    void Move()
    {
        transform.DOScale(transform.localScale * 1.3f, 0.45f).OnComplete(() =>
        {
            transform.DOScale(firstScale, 0.45f).OnComplete(() =>
            {
                Move();
            });
        });
    }

    void Rotate()
    {
        transform.DORotate(new Vector3(0, 0, 20f), 0.3f).OnComplete(() =>
           {
               transform.DORotate(new Vector3(0, 0, -20f), 0.3f).OnComplete(() =>
                  {
                      transform.DORotate(Vector3.zero, 0.3f).OnComplete(() =>
                       {
                           Rotate();
                       });
                  });
           });
    }
}
