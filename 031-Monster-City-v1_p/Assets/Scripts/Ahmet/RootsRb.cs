//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsRb : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> rbs;

    private bool oneTime;


    float explosionPower;

    /*private void OnEnable()
    {

        

        foreach (var VARIABLE in rbs)
        {
            VARIABLE.velocity = (Vector3.up * 2550f * Time.deltaTime);
            
            Destroy(VARIABLE.gameObject, 2.5f);
        }
    }
    */
    private void OnEnable()
    {
        explosionPower = Random.Range(0.1f, 0.3f);

        foreach (var VARIABLE in rbs)
        {
            // VARIABLE.velocity=(Vector3.up*300f *Time.deltaTime);
            // VARIABLE.AddForce(new Vector3(explosionPower,1f,explosionPower) * 18000f * Time.deltaTime);
            // VARIABLE.AddForce(Vector3.forward * 2000f * Time.deltaTime);
           // VARIABLE.AddForce(new Vector3(explosionPower, 1f, explosionPower) * 18000f * Time.deltaTime);
            VARIABLE.AddExplosionForce(600f, transform.position, 50f, 30f);

            Destroy(VARIABLE.gameObject, 2.5f);
        }
    }


    private void FixedUpdate()
    {
        /*if (this.gameObject.activeInHierarchy)
        {
            if (oneTime)
            {
                return;
            }
            foreach (var VARIABLE in rbs)
            {
                // VARIABLE.velocity=(Vector3.up*300f *Time.deltaTime);
                // VARIABLE.AddForce(new Vector3(explosionPower,1f,explosionPower) * 18000f * Time.deltaTime);
                // VARIABLE.AddForce(Vector3.forward * 2000f * Time.deltaTime);

                VARIABLE.AddExplosionForce(1000f, transform.position*9, 50f, 30f);

                Destroy(VARIABLE.gameObject, 2.5f);
            }

            oneTime = true;
        }*/
    }
}
