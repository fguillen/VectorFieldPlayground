using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VectorFieldController))]
public class VectorFieldEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = target as VectorFieldController;

        // Coordinates 000
        Handles.color = Color.white;
        Handles.DrawLine(t.transform.position, t.coordinates000);
        Handles.color = Color.red;
        Handles.DrawLine(t.coordinates000, t.topCoordinates000);

        // Draw Cubes
        Handles.color = Color.yellow;
        for (int x = 0; x < t.cellsX; x++)
        {
            for (int y = 0; y < t.cellsY; y++)
            {
                for (int z = 0; z < t.cellsZ; z++)
                {
                    Vector3 cubeCoordinates =
                        new Vector3(
                            t.coordinates000.x + (x * t.cellSize),
                            t.coordinates000.y + (y * t.cellSize),
                            t.coordinates000.z + (z * t.cellSize)
                        );

                    Vector3 cubeDimensions =
                        new Vector3(
                            t.cellSize,
                            t.cellSize,
                            t.cellSize
                        );

                    Handles.DrawWireCube(cubeCoordinates, cubeDimensions);
                }
            }
        }

        // Draw Vectors
        if(t.initialized)
        {
            Handles.color = Color.magenta;

            for (int x = 0; x < t.cellsX; x++)
            {
                for (int y = 0; y < t.cellsY; y++)
                {
                    for (int z = 0; z < t.cellsZ; z++)
                    {
                        Vector3 vectorCoordinates =
                            new Vector3(
                                t.coordinates000.x + (x * t.cellSize),
                                t.coordinates000.y + (y * t.cellSize),
                                t.coordinates000.z + (z * t.cellSize)
                            );

                        Handles.ArrowHandleCap(
                            0,
                            vectorCoordinates,
                            Quaternion.LookRotation(t.vectors[x, y, z]),
                            t.cellSize,
                            EventType.Repaint
                        );
                    }
                }
            }
        }
    }
}
