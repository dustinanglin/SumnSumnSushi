using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSquare : MonoBehaviour
{
    public GameObject mesh_object;
    public Vector3 dimensions;
    private Mesh mesh, CubeMesh;
    public bool use_mesh;

    // Start is called before the first frame update
    void Start()
    {

        mesh = new Mesh();

        /*Vector3[] verts = new Vector3[5];

        verts[0] = new Vector3(-1, -1);
        verts[1] = new Vector3(-1, 1);
        verts[2] = new Vector3(1, 1);
        verts[3] = new Vector3(1, -1);
        verts[4] = new Vector3(-1, -1);*/

        mesh.name = "Custom Mesh";

    }

    // Update is called once per frame
    void Update()
    {
        if (use_mesh)
            UseMesh();
        else
            SquareFromDimensions();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void SquareFromDimensions()
    {
        Vector3[] verts = new Vector3[13];

        verts[0] = new Vector3(0, 0);
        verts[1] = new Vector3(0, dimensions.y);
        verts[2] = new Vector3(dimensions.x, dimensions.y);
        verts[3] = new Vector3(dimensions.x, 0);

        verts[4] = new Vector3(0, 0);
        verts[5] = new Vector3(0, 0, dimensions.z);
        verts[6] = new Vector3(dimensions.x, 0, dimensions.z);
        verts[7] = new Vector3(dimensions.x, 0);

        verts[8] = new Vector3(0, 0);
        verts[9] = new Vector3(0, 0, dimensions.z);
        verts[10] = new Vector3(0, dimensions.y, dimensions.z);
        verts[11] = new Vector3(0, dimensions.y, 0);

        verts[12] = new Vector3(0, 0);

        mesh.vertices = verts;
    }

    private void UseMesh()
    {
        CubeMesh = mesh_object.GetComponent<MeshFilter>().mesh;

        Vector3[] verts = new Vector3[CubeMesh.vertices.Length + 1];
        CubeMesh.vertices.CopyTo(verts, 0);
        verts[CubeMesh.vertices.Length] = verts[0];

        mesh.vertices = verts;
    }
}
