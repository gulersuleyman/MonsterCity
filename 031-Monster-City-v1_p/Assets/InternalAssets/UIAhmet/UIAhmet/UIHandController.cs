using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIHandController : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private bool run = true;
    
    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            run = false;
            transform.DOMove(gameObject.transform.position + Vector3.right*500, .5f).SetEase(ease).OnComplete((() =>
            {
                transform.DOMove(gameObject.transform.position +Vector3.left*500, .5f).SetEase(ease).OnComplete((() =>
                {
                    run = true;
                }));
            }));
        }
    }
}
