using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePath : MonoBehaviour
{
    //private Mesh mesh;

    private CanvasRenderer _canvasRenderer;
    private RectTransform startPos;
    private RectTransform endPos;

    private void Awake()
    {
        _canvasRenderer = GetComponent<CanvasRenderer>();

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangels = new int[6];

        vertices[0] = new Vector3(-1,1);
        vertices[1] = new Vector3(-1,-1);
        vertices[2] = new Vector3(1, -1);
        vertices[3] = new Vector3(1, 1);

        triangels[0] = 0;
        triangels[1] = 3;
        triangels[2] = 1;

        triangels[3] = 1;
        triangels[4] = 3;
        triangels[5] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangels;


        mesh = Instantiate(mesh);
        Debug.Log(mesh.vertices[0]);
        _canvasRenderer.SetMesh(null);
        _canvasRenderer.SetMesh(mesh);
    }
}
