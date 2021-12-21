using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGarbageCollectorController : MonoBehaviour
{
    void Update()
    {
        if(transform.position.magnitude > 100f)
            Destroy(gameObject);
    }
}
