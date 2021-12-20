using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VectorFieldController))]
public class VectorFieldEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = target as VectorFieldController;
        float x0 = ((t.cellsX / 2f) * t.cellSize) - (t.cellSize / 2f);
        float y0 = ((t.cellsY / 2f) * t.cellSize) - (t.cellSize / 2f);
        float z0 = ((t.cellsZ / 2f) * t.cellSize) - (t.cellSize / 2f);

        // Coordinate 000
        Vector3 coordinate000 = t.transform.position - new Vector3(x0, y0, z0);
        Handles.color = Color.magenta;
        Handles.DrawLine(t.transform.position, coordinate000);

        for (int x = 0; x < t.cellsX; x++)
        {
            for (int y = 0; y < t.cellsY; y++)
            {
                for (int z = 0; z < t.cellsZ; z++)
                {
                    Vector3 cubeCoordinates =
                        new Vector3(
                            coordinate000.x + (x * t.cellSize),
                            coordinate000.y + (y * t.cellSize),
                            coordinate000.z + (z * t.cellSize)
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
    }
}
