using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class TechTree : MonoBehaviour, IWindowPanel
{
    public TechTreeSO treeSO;
    public Dictionary<NodeSO, Node> nodeDic;

    public Transform edgeParent;
    private string _path;

    [SerializeField] private RectTransform _treeRect;

    private void Awake()
    {
        nodeDic = new Dictionary<NodeSO, Node>();
        _path = Path.Combine(Application.dataPath, "TechTree.json");
    }

    private void Start()
    {
        int childCnt = transform.childCount;

        for (int i = 0; i < treeSO.nodes.Count; i++)
        {
            for (int j = 0; j < childCnt; j++)
            {
                if (transform.GetChild(j).TryGetComponent(out Node node))
                {
                    if (treeSO.nodes[i].id != node.node.id) continue;

                    nodeDic.Add(node.node, node);
                }
            }
        }

        for(int i = 0; i < treeSO.nodes.Count; i++)
        {
            NodeSO nodeSO = treeSO.nodes[i];

            if(nodeDic.TryGetValue(nodeSO, out Node node))
                node.Init(this, node.node.id == 0);
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

        return nodeDic.TryGetValue(nodeSO, out node);
    }

    public Node GetNode(int id)
    {
        //일단 기본으로 첫번째 놈 반환

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
                    nodeDic[n].Init(this, p.enabled);
                }
            }
            else if (n is WeaponNodeSO weapon)
            {
                if (GameDataManager.Instance.TryGetWeapon(weapon.weapon, out WeaponSave w))
                {
                    nodeDic[n].Init(this, w.enabled);
                }
            }
        });
    }

    #region UI

    public void Open()
    {
        _treeRect.DOAnchorPosY(0, 0.5f).SetEase(Ease.Linear);
    }

    public void Close()
    {
        _treeRect.DOAnchorPosY(-1080, 0.5f).SetEase(Ease.Linear);
    }

    #endregion
}


[Serializable]
public class TechTreeSave
{
    public List<bool> nodeEnable = new List<bool>();
}
