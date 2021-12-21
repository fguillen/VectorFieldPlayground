using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VectorFieldEffector : MonoBehaviour
{
    [SerializeField] public VectorFieldController vectorFieldController;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Vector3 forceEffect;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if(rb == null) {
            throw new Exception("rb is required");
        }
    }

    void Start()
    {
        if(vectorFieldController == null) {
            throw new Exception("vectorField is required");
        }
    }

    void LateUpdate()
    {
        forceEffect = vectorFieldController.VectorValueInWorldCoordinates(transform.position);
        // Debug.Log($"forceEffect: {forceEffect}");
        if(forceEffect != Vector3.zero)
        {
            ApplyRotation(forceEffect);
            rb.velocity = transform.forward * moveSpeed;
        }
    }

    void ApplyRotation(Vector3 rotation)
    {
        Quaternion targetRotation = Quaternion.LookRotation(rotation.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        if(forceEffect != Vector3.zero)
        {
            Handles.color = Color.magenta;
            Handles.ArrowHandleCap(
                0,
                transform.position,
                Quaternion.LookRotation(forceEffect),
                1f,
                EventType.Repaint
            );
        }
    }

}
