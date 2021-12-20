using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFieldEffector : MonoBehaviour
{
    [SerializeField] VectorFieldController vectorFieldController;
    [SerializeField] Vector3 appliedForce;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if(rb == null || vectorFieldController == null) {
            throw new Exception("rb and vectorField are required");
        }
    }

    void Update()
    {
        appliedForce = vectorFieldController.VectorValueInWorldCoordinates(transform.position);
        rb.velocity = appliedForce;
    }

}
