using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[CreateAssetMenu(menuName = "SO/SkillTreeNode")]
public abstract class NodeSO : ScriptableObject
{
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;

    [HideInInspector] public int id;
    [HideInInspector] public List<NodeSO> nextNodes;
    public Sprite icon;
    public int requireCoin;
}
