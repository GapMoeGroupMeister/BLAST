using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGaugeUI : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] _triangles;


    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

    }

    public void RefreshGauge(int value, int maxValue){
        RefreshGauge(value / (float)maxValue);
    }

    public void RefreshGauge(float fillAmount)
    {
        vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.05f, 0), // Bottom-left
            new Vector3(-0.5f + fillAmount, -0.05f, 0), // Bottom-right (fill에 비례)
            new Vector3(-0.5f + fillAmount, 0.05f, 0),  // Top-right
            new Vector3(-0.5f, 0.05f, 0)   // Top-left
        };

        _triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3
        };

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = _triangles;
        mesh.RecalculateNormals();
    }

}
