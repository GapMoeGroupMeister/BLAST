using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TechTree")]
public class TechTreeSO : ScriptableObject
{
    public NodeSO startTechTree;
    public NodeSO[] techTrees;
}
