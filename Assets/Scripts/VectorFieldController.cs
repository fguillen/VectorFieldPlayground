using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFieldController : MonoBehaviour
{
    [SerializeField] public float cellSize = 1f;
    [SerializeField] public int cellsX = 10;
    [SerializeField] public int cellsY = 10;
    [SerializeField] public int cellsZ = 10;
    public Vector3[,,] vectors;

    void Awake()
    {
        vectors = new Vector3[cellsX, cellsY, cellsZ];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
