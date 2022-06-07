using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    [SerializeField] private GameObject _player;

    private float firstXPosition;
    private void Awake()
    {
        transform.position = _player.transform.position;
        firstXPosition = transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position =
            new Vector3((_player.transform.position.x-firstXPosition)/1.5f +firstXPosition ,
                _player.transform.position.y, _player.transform.position.z);
    }
}
