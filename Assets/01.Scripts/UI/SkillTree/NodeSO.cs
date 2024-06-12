using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillTreeNode")]
public class NodeSO : ScriptableObject
{
    public int id;
    public int requireCost;
    public TemporaryEnum skillToUnlock;
    public NodeSO[] nextNodes;
}

public enum TemporaryEnum 
{
    SKIL = 0,
    SKILL = 1,
    SKILLL = 1,
    SKILLLL = 1,
    SKILLLLL = 1,
}