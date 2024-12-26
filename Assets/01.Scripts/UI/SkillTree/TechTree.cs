using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TechTree : MonoBehaviour, IWindowPanel
{
    public TechTreeSO treeSO;
    public Dictionary<NodeSO, Node> nodeDic = new Dictionary<NodeSO, Node>();
    public WarningPanel warningPanel;
    public TechTreeTooltipPanel tooltipPanel;
    [SerializeField] private Node nodePf;

    private string _path;

    [SerializeField] private RectTransform _treeRect;
    public UnityEvent<int, int> selectNodeEvent;

    private void Start()
    {
        _path = Path.Combine(Application.dataPath, "TechTree.json");
        Load();
    }



    private void OnValidate()
    {
        Debug.Log("코코다요~");
        int childCnt = transform.childCount;
        nodeDic.Clear();

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            for (int j = 0; j < childCnt; j++)
            {
                if (transform.GetChild(j).TryGetComponent(out Node node))
                {
                    if (treeSO.nodes[i].id != node.NodeType.id) continue;
                    nodeDic.Add(node.NodeType, node);
                }
            }
        }

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            NodeSO nodeSO = treeSO.nodes[i];
            nodeDic[nodeSO].SetEdge();
        }
    }

#if UNITY_EDITOR
    [ContextMenu("CreateNodes")]
    private void CreateNodes()
    {
        treeSO.nodes.ForEach(node =>
        {
            Node nodeInstance = PrefabUtility.InstantiatePrefab(nodePf, transform) as Node;
            nodeInstance.SetNode(node);

            if (node is PartNodeSO part)
            {
                nodeInstance.name = part.openPart.ToString();
            }
            else if (node is WeaponNodeSO weapon)
            {
                nodeInstance.name = weapon.weapon.ToString();
            }
            else if (node is StartNodeSO)
            {
                nodeInstance.name = "StartNode";
                //nodeInstance.DestroyEdge();
            }
        });
    }

    [ContextMenu("RefreshNodes")]
    private void RefreshNode()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Node node))
            {
                Node nodeInstance = PrefabUtility.InstantiatePrefab(nodePf, transform) as Node;
                //nodeInstance.SetEdgePosition(node.GetEdgePosition());
                //nodeInstance.SetPosition(node.GetPosition());
                //nodeInstance.transform.SetSiblingIndex(i);

                //nodeInstance.node = node.NodeType;

                if (node.NodeType is PartNodeSO part)
                {
                    nodeInstance.name = part.openPart.ToString();
                }
                else if (node.NodeType is WeaponNodeSO weapon)
                {
                    nodeInstance.name = weapon.weapon.ToString();
                }
                else if (node.NodeType is StartNodeSO)
                {
                    nodeInstance.name = "StartNode";
                }

                DestroyImmediate(node.gameObject);
                //Destroy(NodeType.gameObject);
            }
        }
    }
    #endif

    public bool TryGetNode(NodeSO nodeSO, out Node node)
    {
        if (nodeSO == null)
        {
            node = null;
            return false;
        }

        return nodeDic.TryGetValue(nodeSO, out node);
    }

    public Node GetNode(int id)
    {
        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            if (treeSO.nodes[i].id == id)
            {
                NodeSO nodeSO = treeSO.nodes[i];
                return nodeDic[nodeSO];
            }
        }
        return null;
    }

    public void Save()
    {
        List<Node> parts = new List<Node>();
        List<Node> weapons = new List<Node>();
        treeSO.nodes.ForEach(n =>
        {
            if (n is PartNodeSO)
            {
                if (TryGetNode(n, out Node node))
                {
                    parts.Add(node);
                }
            }

            if (n is WeaponNodeSO)
            {
                if (TryGetNode(n, out Node node))
                {
                    weapons.Add(node);
                }
            }
        });

        GameDataManager.Instance.SetParts(parts);
        GameDataManager.Instance.SetWeapons(weapons);

        GameDataManager.Instance.Save();
    }

    public void Load()
    {
        treeSO.nodes.ForEach(n =>
        {
            if (n is PartNodeSO part)
            {
                if (GameDataManager.Instance.TryGetPart(part.openPart, out PartSave p))
                {
                    Debug.Log(p.enabled);
                    nodeDic[n].Init(p.enabled);
                }
            }
            else if (n is WeaponNodeSO weapon)
            {
                if (GameDataManager.Instance.TryGetWeapon(weapon.weapon, out WeaponSave w))
                {
                    Debug.Log(w.enabled);
                    nodeDic[n].Init(w.enabled);
                }
            }
        });
    }

    #region UI

    public void Open()
    {
        _treeRect.DOAnchorPosY(0, 0.5f).SetEase(Ease.Linear);
        UIControlManager.Instance.overUIAmount++;
    }

    public void Close()
    {
        _treeRect.DOAnchorPosY(-1080, 0.5f).SetEase(Ease.Linear);
        UIControlManager.Instance.overUIAmount--;
    }

    #endregion
}


[Serializable]
public class TechTreeSave
{
    public List<bool> nodeEnable = new List<bool>();
}
