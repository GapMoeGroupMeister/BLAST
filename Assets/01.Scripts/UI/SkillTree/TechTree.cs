using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    public TechTreeSO techTreeSO;
    [SerializeField] private GameObject treeNodePrefab;

    private List<(int, int)> nodes;

    private void Awake()
    {
        nodes = new List<(int, int)>();

        for (int i = 0; i < techTreeSO.techTrees.Length; i++)
        {
            int begin = techTreeSO.techTrees[i].id;
            for (int j = 0; j < techTreeSO.techTrees[i].nextNodes.Length; j++)
            {
                int end = techTreeSO.techTrees[i].nextNodes[j].id;
                nodes.Add((begin, end));
            }
        }
    }

    //public TechTree[] GetTreesInPath(int idx)
    //{

    //}
}
