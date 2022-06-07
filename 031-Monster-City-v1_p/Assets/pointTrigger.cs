using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointTrigger : MonoBehaviour
{
    public bool changeColor;
    public GameObject material;
    public Color Color;
    

    // Update is called once per frame
    void Update()
    {
        if (changeColor)
        {
            material.GetComponent<Renderer>().material.color = Color;
        }
    }
}
