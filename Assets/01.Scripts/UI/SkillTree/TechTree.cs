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
    public Dictionary<int, Node> nodeDic = new Dictionary<int, Node>();
    public WarningPanel warningPanel;
    public TechTreeTooltipPanel tooltipPanel;
    public UnityEvent<int, int> selectNodeEvent;

    [SerializeField] private Node nodePf;
    [SerializeField] private RectTransform _treeRect;
    [SerializeField] private RectTransform _edgeParent;
    [SerializeField] private RectTransform _edgeFillParent;

    public RectTransform EdgeParent => _edgeParent;
    public RectTransform EdgeFillParent => _edgeFillParent;

    private void Awake()
    {
        int childCnt = transform.childCount;
        nodeDic.Clear();

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            for (int j = 0; j < childCnt; j++)
            {
                if (transform.GetChild(j).TryGetComponent(out Node node))
                {
                    if (treeSO.nodes[i].id != node.NodeType.id) continue;
                    nodeDic.Add(node.NodeType.id, node);
                }
            }
        }

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            NodeSO nodeSO = treeSO.nodes[i];
            nodeDic[nodeSO.id].SetEdge();
        }

        Load();
    }

    public bool TryGetNode(NodeSO nodeSO, out Node node)
    {
        if (nodeSO == null)
        {
            node = null;
            return false;
        }

        return nodeDic.TryGetValue(nodeSO.id, out node);
    }

    public Node GetNode(int id)
    {
        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            if (treeSO.nodes[i].id == id)
            {
                NodeSO nodeSO = treeSO.nodes[i];
                return nodeDic[nodeSO.id];
            }
        }
        return null;
    }

    public void Save()
    {
        GameDataManager.Instance.Save();
    }

    public void Load()
    {
        GameDataManager.Instance.Load();

        treeSO.nodes.ForEach(n =>
        {
            if (n is PartNodeSO part)
            {
                if (GameDataManager.Instance.TryGetPart(part.openPart, out PartSave p))
                {
                    Debug.Log(p.enabled);
                    nodeDic[n.id].Init(p.enabled);
                }
            }
            else if (n is WeaponNodeSO weapon)
            {
                if (GameDataManager.Instance.TryGetWeapon(weapon.weapon, out WeaponSave w))
                {
                    Debug.Log(w.enabled);
                    nodeDic[n.id].Init(w.enabled);
                }
            }
            else if (n is StartNodeSO start)
            {
                nodeDic[n.id].Init(true);
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

    private void OnValidate()
    {
        int childCnt = transform.childCount;
        nodeDic.Clear();

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            for (int j = 0; j < childCnt; j++)
            {
                if (transform.GetChild(j).TryGetComponent(out Node node))
                {
                    if (treeSO.nodes[i].id != node.NodeType.id) continue;
                    nodeDic.Add(node.NodeType.id, node);
                }
            }
        }

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            NodeSO nodeSO = treeSO.nodes[i];
            nodeDic[nodeSO.id].SetEdge();
        }
    }
#endif

}


[Serializable]
public class TechTreeSave
{
    public List<bool> nodeEnable = new List<bool>();
}
