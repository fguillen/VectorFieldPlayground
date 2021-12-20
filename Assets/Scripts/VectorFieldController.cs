using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFieldController : MonoBehaviour
{
    [SerializeField] public float cellSize = 1f;
    [SerializeField] public int cellsX = 5;
    [SerializeField] public int cellsY = 5;
    [SerializeField] public int cellsZ = 5;

    [SerializeField] float seed = 1;
    [SerializeField] float frequency = 0.1f;
    [SerializeField] float amplitude = 4f;
    [SerializeField] float persistence = 1f;
    [SerializeField] int octave = 1;

    public Vector3 coordinates000;
    public Vector3 topCoordinates000;
    public Vector3[,,] vectors;
    public bool initialized = false;

    void Awake()
    {
        if(!initialized)
        {
            InitializeCoordinates000();
            InitializeVectors();
            initialized = true;
        }
    }

    void OnValidate()
    {
        InitializeCoordinates000();
        InitializeVectors();
        initialized = true;
    }

    void InitializeVectors()
    {
        vectors = new Vector3[cellsX, cellsY, cellsZ];

        for (int x = 0; x < cellsX; x++)
        {
            for (int y = 0; y < cellsY; y++)
            {
                for (int z = 0; z < cellsZ; z++)
                {
                    float noise = PerlinNoise3D.value(x, y, z, seed, frequency, amplitude, persistence, octave);
                    float noisePI = noise * Mathf.PI;
                    Vector3 noiseDirection = new Vector3(Mathf.Cos(noisePI), Mathf.Sin(noisePI), Mathf.Cos(noisePI));
                    Debug.Log($"[{x}, {y}, {z}] : {noiseDirection.normalized}");
                    vectors[x, y, z] = noiseDirection.normalized;
                }
            }
        }
    }

    public void InitializeCoordinates000()
    {
        float x0 = (cellsX / 2f) * cellSize;
        float y0 = (cellsY / 2f) * cellSize;
        float z0 = (cellsZ / 2f) * cellSize;

        // On the top corner
        topCoordinates000 = transform.position - new Vector3(x0, y0, z0);

        // On center of the first cube
        coordinates000 = topCoordinates000 - new Vector3(cellSize / 2f, cellSize / 2f, cellSize / 2f);
    }

    public Vector3Int VectorIndexInWorldCoordinates(Vector3 worldCoordinates)
    {
        Vector3 distanceVector = worldCoordinates - topCoordinates000;
        Vector3Int vectorIndex = Vector3Int.FloorToInt(distanceVector / cellSize);

        if(
            vectorIndex.x < 0 ||
            vectorIndex.x >= cellsX ||
            vectorIndex.y < 0 ||
            vectorIndex.y >= cellsY ||
            vectorIndex.z < 0 ||
            vectorIndex.z >= cellsZ
        )
        {
            return new Vector3Int(-1, -1, -1);
        } else
        {
            return vectorIndex;
        }
    }

    public Vector3 VectorValueInWorldCoordinates(Vector3 worldCoordinates)
    {
        Vector3Int vectorIndex = VectorIndexInWorldCoordinates(worldCoordinates);
        Debug.Log($"VectorValueInWorldCoordinates.vectorIndex: {vectorIndex}");
        if(vectorIndex.x == -1)
        {
            return Vector3.zero;
        } else
        {
            return vectors[vectorIndex.x, vectorIndex.y, vectorIndex.z];
        }
    }
}
