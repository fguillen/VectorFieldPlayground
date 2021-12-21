using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;
    [SerializeField] VectorFieldController vectorFieldController;
    [SerializeField] int particlesPerSecond;
    float nextParticleAt;

    // Start is called before the first frame update
    void Start()
    {
        SpawnParticle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextParticleAt)
            SpawnParticle();
    }

    void SpawnParticle()
    {
        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity, transform);
        particle.GetComponent<Rigidbody>().AddForce(Vector3.down * 10f, ForceMode.Impulse);
        particle.GetComponent<VectorFieldEffector>().vectorFieldController = vectorFieldController;
        nextParticleAt = Time.time + (1f / particlesPerSecond);
    }
}
