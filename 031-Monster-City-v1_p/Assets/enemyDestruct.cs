using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDestruct : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("build"))
        {
           
           
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;

          

            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
           
        }

        if (other.gameObject.CompareTag("point"))
        {
            other.gameObject.GetComponent<pointTrigger>().changeColor = true;
        }
       
    }
}
