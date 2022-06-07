using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Dreamteck.Splines.Primitives;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    /*private void OnTriggerEnter(Collider other)
    {
        Explode();
    }*/

    public float cubeSize = .2f;
    public int cubesInRow = 5;

    private float cubesPivotDistance;
    private Vector3 cubesPivot;

    
    //rb
    public float explosionForce;
    public float explosionRadius;
    public float explosionUpward;

    
    //Materials
    public Material cubeMaterial;
    private void Start()
    {
        //calculates pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        
        //use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    public void Explode()
    {
        gameObject.SetActive(false);

        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    CreatePiece(x,y,z);
                }
            }
        }
        
        //get explosion position
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit  in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce,transform.position,explosionRadius,explosionUpward);
            }
        }
    }

    void CreatePiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        //materia
        piece.GetComponent<MeshRenderer>().material = cubeMaterial;
        
        
        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize*x,cubeSize*y,cubeSize*z)-cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize,cubeSize,cubeSize);
        
        // rigibody count
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

    }
}
