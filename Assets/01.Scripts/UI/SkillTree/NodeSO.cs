using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/SkillTreeNode")]
public class NodeSO : ScriptableObject
{
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;

    public int id;
    public TemporaryEnum skillToOpen;        //�ϴ� �ӽ��Ӥ���

    public bool isStartNode;
    public List<NodeSO> nextNodes;
}

public enum TemporaryEnum
{
    SKIL = 0,
    SKILL = 1,
    SKILLL = 1,
    SKILLLL = 1,
    SKILLLLL = 1,
}