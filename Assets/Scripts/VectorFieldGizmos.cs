using UnityEditor;
using UnityEngine;

public static class VectorFieldGizmos
{
    public static void DrawGizmos(VectorFieldController target)
    {
        // Coordinates 000
        Handles.color = Color.white;
        Handles.DrawLine(target.transform.position, target.coordinates000);
        Handles.color = Color.red;
        Handles.DrawLine(target.coordinates000, target.topCoordinates000);

        // Draw Cubes
        Handles.color = Color.yellow;
        for (int x = 0; x < target.cellsX; x++)
        {
            for (int y = 0; y < target.cellsY; y++)
            {
                for (int z = 0; z < target.cellsZ; z++)
                {
                    Vector3 cubeCoordinates =
                        new Vector3(
                            target.coordinates000.x + (x * target.cellSize),
                            target.coordinates000.y + (y * target.cellSize),
                            target.coordinates000.z + (z * target.cellSize)
                        );

                    Vector3 cubeDimensions =
                        new Vector3(
                            target.cellSize,
                            target.cellSize,
                            target.cellSize
                        );

                    Handles.DrawWireCube(cubeCoordinates, cubeDimensions);
                }
            }
        }

        // Draw Vectors
        if(target.initialized)
        {
            Handles.color = Color.magenta;

            for (int x = 0; x < target.cellsX; x++)
            {
                for (int y = 0; y < target.cellsY; y++)
                {
                    for (int z = 0; z < target.cellsZ; z++)
                    {
                        Vector3 vectorCoordinates =
                            new Vector3(
                                target.coordinates000.x + (x * target.cellSize),
                                target.coordinates000.y + (y * target.cellSize),
                                target.coordinates000.z + (z * target.cellSize)
                            );

                        Handles.ArrowHandleCap(
                            0,
                            vectorCoordinates,
                            Quaternion.LookRotation(target.vectors[x, y, z]),
                            target.cellSize,
                            EventType.Repaint
                        );
                    }
                }
            }
        }
    }
}
